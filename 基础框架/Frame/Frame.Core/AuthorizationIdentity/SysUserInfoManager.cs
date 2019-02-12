using Abp;
using Abp.Authorization;
using Abp.Extensions;
using Abp.UI;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Frame.Core
{
    /// <summary>
    /// 重构Microsoft.AspNet.Identity方案
    /// 系统用户管理对象
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public abstract class SysUserInfoManager<TUser> :
        UserManager<TUser, long>,
        Abp.Domain.Services.IDomainService
        where TUser : SysUserInfo<TUser>
    {

        /// <summary>
        /// 注入用户信息仓储
        /// </summary>
        public SysUserInfoStore<TUser> _sysInUserInfoStore;

        /// <summary>
        /// 缓存扩展
        /// </summary>
        private readonly ICacheManagerExtens _cacheManagerExtens;

        /// <summary>
        /// 
        /// </summary>
        private readonly IPermissionManager _permissionManager;

         /// <summary>
         /// 
         /// </summary>
        public readonly IAbpSessionExtens AbpSessionExtens;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysInUserInfoStore"></param>
        /// <param name="cacheManager"></param>
        protected SysUserInfoManager(
            SysUserInfoStore<TUser> sysInUserInfoStore,
             ICacheManagerExtens cacheManagerExtens,
             IPermissionManager permissionManager,
             IAbpSessionExtens abpSessionExtens 
            ) : base(
            sysInUserInfoStore
            )
        {
            _cacheManagerExtens = cacheManagerExtens;
            _sysInUserInfoStore = sysInUserInfoStore;

            _permissionManager = permissionManager;
            AbpSessionExtens = abpSessionExtens;
        }

        /// <summary>
        /// 验证登录用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<SysLoginResult<TUser>> LoginAuth(TUser model)
        {
            if (model.UserCode.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(model.UserCode));
            }

            if (model.Password.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(model.Password));
            }
             
            var userModel = await _sysInUserInfoStore.FindByNameAsync(model.UserCode);
            //验证用户信息 
            if (userModel == null)
            {
                return new SysLoginResult<TUser>(LoginResultType.InvalidUserNameOrEmailAddress, userModel);
            }
            if (!userModel.IsActive)
            {
                //未激活的用户不做登录
                throw new UserFriendlyException(nameof(LoginResultType.UserIsNotActive), "用户未激活!");
                //return new SysLoginResult<TUser>(LoginResultType.UserIsNotActive, userModel);
            }

            //验证登录的密码
            var verificationResult = base.PasswordHasher.VerifyHashedPassword(userModel.Password, model.Password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new UserFriendlyException(nameof(LoginResultType.InvalidPassword), "用户密码无效!");
                //return new SysLoginResult<TUser>(LoginResultType.InvalidPassword);
            }
            #region 扩展验证 暂未使用
            //成功但应更新密码并重新经过哈希处理
            //verificationResult == PasswordVerificationResult.SuccessRehashNeeded

            //锁定用户
            //if (await base.IsLockedOutAsync(userModel.Id))
            //{
            //    return new SysLoginResult<TUser>(LoginResultType.LockedOut);
            //}
            //如果实现了登陆锁定功能必须执行如下
            //await base.ResetAccessFailedCountAsync(model.Id);
            #endregion
            //设置授权信息
            return await CreateIdentityAsync(userModel);
        }

        /// <summary>
        /// 设置授权信息
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<SysLoginResult<TUser>> CreateIdentityAsync(TUser userModel)
        {
            //创建登录凭证
            var identity = await base.CreateIdentityAsync(userModel, DefaultAuthenticationTypes.ApplicationCookie);

            #region 授权凭证添加自定义的属性
            //用户名称
            identity.AddClaim(new Claim("UserNameCn", userModel.UserNameCn));
            //用户账号
            identity.AddClaim(new Claim("UserCode", userModel.UserCode));
            //
            identity.AddClaim(new Claim("IsAdmin", userModel.IsAdmin.ToString()));
            //用户包含的角色ID
            List<long> roleList = new List<long>();
            foreach (var item in userModel.SysRoleToUserList)
                roleList.Add(item.RoleID);
            string strList = string.Join(",", roleList.ToArray());
            identity.AddClaim(new Claim("UserRoleList", strList));
            #endregion

            //成功后返回用户对象
            return new SysLoginResult<TUser>(userModel, identity);
        }

        /// <summary>
        /// 验证加密的密码
        /// </summary>
        /// <param name="hashedPass"></param>
        /// <param name="currentPass"></param>
        /// <returns></returns>
        public bool VerifyPassword(string hashedPass, string currentPass)
        {
            var verificationResult = base.PasswordHasher.VerifyHashedPassword(hashedPass, currentPass);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取指定用户的授权
        /// </summary>
        /// <param name="user"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        //[UnitOfWork]
        public async Task<bool> IsGrantedAsync(UserIdentifier user, string permissionName)
        {
            UserInfo userInfo = _cacheManagerExtens.GetUserInfoCache(user.UserId);
            return await IsValidationGrantedAsync(userInfo,permissionName);
        }

        /// <summary>
        /// 获取当前用户的授权
        /// </summary>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        //[UnitOfWork]
        public async Task<bool> IsGrantedAsync(string permissionName)
        {
            UserInfo userInfo = _cacheManagerExtens.GetUserInfoCache(AbpSessionExtens.UserId.Value);
            return await IsValidationGrantedAsync(userInfo, permissionName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        //[UnitOfWork]
        public async Task<bool> IsValidationGrantedAsync(UserInfo userInfo,string permissionName)
        {
            permissionName = permissionName.ToLower();

            //
            if (userInfo.IsAdmin)
            {
                return true;
            }

            //旧的方案:在Abp授权字典集合查询(启动状态下新增的授权将无法获取到)
            //Permission currentPermission = _permissionManager.GetPermissionOrNull(permissionName);
            //在所有授权对象里面查询(可以及时获取到新增的授权对象)
            Permission currentPermission = GetPermissionsOrNull(permissionName);

            if (currentPermission == null)
            {
                return false;
            }
          
            //判断角色
            if (userInfo.SysRoleToUserList == null || !userInfo.SysRoleToUserList.Any())
            {
                return false;
            }
            //获取当前授权名称的请求对象
            List<MenuActionPermissionCache> menuActionPermissionList = _cacheManagerExtens.GetMenuActionPermissionCache();
            var resData = menuActionPermissionList
                   .Where(w => w.IsActive == true
                   && !string.IsNullOrEmpty(w.PermissionName)
                   && w.PermissionName.Equals(permissionName, StringComparison.CurrentCultureIgnoreCase));
            if (resData == null || !resData.Any())
            {
                return false;
            }
            //
            string requiresAuthModel = resData.ToList()[0].RequiresAuthModel;
            //开放模式(所有用户可以访问)
            if (requiresAuthModel == "1")
            {
                return true;
            }
            //登录模式(仅限登录验证通过的用户)
            else if (requiresAuthModel == "2" && AbpSessionExtens != null && AbpSessionExtens.UserId != null)
            {
                return true;
            }
            //授权模式(不仅要登录还需要有授权的用户)
            else if (requiresAuthModel == "3")
            {
                foreach (var item in userInfo.SysRoleToUserList)
                {
                    var permissionList = _cacheManagerExtens.GetRoleToPermissionCache(Convert.ToInt64(item.RoleID))
                        .Where(w => !string.IsNullOrEmpty(w.PermissionName) && w.PermissionName.ToLower() == permissionName);
                    if (permissionList.Any())
                    {
                        return true;
                    }
                }
            }
            //
            return await Task.FromResult(false);
        }

        /// <summary>
        /// 获取授权
        /// </summary>
        /// <param name="list">所有授权对象</param>
        /// <param name="permissionName">授权名称</param>
        /// <returns></returns>
        public Permission GetPermissionsOrNull(string permissionName)
        {
            IReadOnlyList<Permission> allPermissionList = _permissionManager.GetAllPermissions();

            foreach (Permission item in allPermissionList)
            {
                Permission p = GetPermissionsOrNull(item, permissionName);
                if (p != null)
                {
                    return p;
                }
            }
            return null;
        }

        /// <summary>
        /// 递归获取授权
        /// </summary>
        /// <param name="permission">Permission to be added</param>
        private Permission GetPermissionsOrNull(Permission permission, string permissionName)
        {
            if (permission.Name.Equals(permissionName, StringComparison.CurrentCultureIgnoreCase))
            {
                return permission;
            }
            //
            foreach (var childPermission in permission.Children)
            {
                Permission res = GetPermissionsOrNull(childPermission, permissionName);
                if (res != null)
                {
                    return permission;
                }
            }
            return null;
        }




    }
}
