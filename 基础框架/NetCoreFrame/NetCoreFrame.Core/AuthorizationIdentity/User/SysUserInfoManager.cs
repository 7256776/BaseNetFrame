using Abp;
using Abp.Authorization;
using Abp.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 重构Microsoft.AspNet.Identity方案
    /// 系统用户管理对象
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public abstract class SysUserInfoManager<TUser> :
        UserManager<TUser>,
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

        private readonly IOptions<IdentityOptions> _optionsAccessor;

        #region 构造
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysInUserInfoStore"></param>
        /// <param name="cacheManager"></param>
        protected SysUserInfoManager(
            SysUserInfoStore<TUser> sysInUserInfoStore,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<TUser> passwordHasher,
            IEnumerable<IUserValidator<TUser>> userValidators,
            IEnumerable<IPasswordValidator<TUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<TUser>> logger,
             ICacheManagerExtens cacheManagerExtens,
             IPermissionManager permissionManager,
             IAbpSessionExtens abpSessionExtens
            ) : base(
            sysInUserInfoStore,
            optionsAccessor,
            passwordHasher,
            userValidators,
            passwordValidators,
            keyNormalizer,
            errors, 
            services,
            logger 
            )
        {
            _sysInUserInfoStore = sysInUserInfoStore;
            _optionsAccessor = optionsAccessor;

            _cacheManagerExtens = cacheManagerExtens;
            _permissionManager = permissionManager;
            AbpSessionExtens = abpSessionExtens;
           
        }
        #endregion

        /// <summary>
        /// 验证加密的密码
        /// </summary>
        /// <param name="hashedPass"></param>
        /// <param name="currentPass"></param>
        /// <returns></returns>
        public bool VerifyPassword(string hashedPass, string currentPass)
        {
            var verificationResult = base.PasswordHasher.VerifyHashedPassword(null, hashedPass, currentPass);
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
                //判断角色
                if (userInfo.SysRoleToUserList == null || !userInfo.SysRoleToUserList.Any())
                    return false;

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
