using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Identity;
using NetCoreFrame.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Application
{
    [Audited]
    public class UserInfoAppService :
        NetCoreFrameApplicationBase, IUserInfoAppService
    {
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly UserInfoManager _userInfoManager;
        private readonly SignInManager _sysSignInManager;

        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ICacheManagerExtens _cacheManagerExtens;
        private readonly ISysNotificationAppService _sysNotificationAppService;
        private readonly IUserInfoExtens _userInfoExtens;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfoRepository"></param>
        /// <param name="userInfoManager"></param>
        /// <param name="authenticationManager"></param>
        /// <param name="unitOfWorkManager"></param>
        public UserInfoAppService(
            IUserInfoRepository userInfoRepository,
            UserInfoManager userInfoManager,
            SignInManager sysSignInManager,
            IUnitOfWorkManager unitOfWorkManager,
            ICacheManagerExtens cacheManagerExtens,
            ISysNotificationAppService sysNotificationAppService,
            IUserInfoExtens userInfoExtens
            )
        {
            _userInfoRepository = userInfoRepository;
            _sysSignInManager = sysSignInManager;
            _userInfoManager = userInfoManager;
            _unitOfWorkManager = unitOfWorkManager;
            _cacheManagerExtens = cacheManagerExtens;
            _sysNotificationAppService = sysNotificationAppService;
            _userInfoExtens = userInfoExtens;
        }

        #region SysSignInManager

        /// <summary>
        /// 验证登录用户信息
        /// 注册登录凭证
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [DisableAuditing]
        public async Task<SysLoginResult<UserInfo>> VerifyAuthAndSignIn(LoginUser model)
        {
            //验证并返回登录用户对象
            SysLoginResult<UserInfo> resLoginUser = await _sysSignInManager.LoginAuth(model.MapTo<UserInfo>());
            if (resLoginUser.Identity != null)
            {
                //注册登录凭证
                await SetAuthenticationProperties(model, resLoginUser);
            }
            return resLoginUser;
        }

        /// <summary>
        /// 登录授权 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [DisableAuditing]
        public async Task<SysLoginResult<UserInfo>> LoginAuth(LoginUser model)
        {
            //验证并返回登录用户对象
            SysLoginResult<UserInfo> resLoginUser = await _sysSignInManager.LoginAuth(model.MapTo<UserInfo>());
            return resLoginUser;
        }

        /// <summary>
        /// 注册身份验证的属性
        /// </summary>
        /// <param name="model"></param>
        /// <param name="resLoginUser"></param>
        [RemoteService(false)]
        [DisableAuditing]
        public async Task SetAuthenticationProperties(LoginUser model, SysLoginResult<UserInfo> resLoginUser)
        {
            if (resLoginUser.Identity != null)
            {
               await _sysSignInManager.SignOutAndSignInAsync(resLoginUser.Identity, model.IsPersistent);
            }
            else
            {
                throw new UserFriendlyException(nameof(LoginResultType.AuthenticationRegistrationFailure));
            }
        }

        /// <summary>
        /// 注销用户
        /// </summary>
        /// <returns></returns>
        [RemoteService(false)]
        [DisableAuditing]
        public AjaxResponse SignOut()
        {
            _sysSignInManager.SignOutAsync();
            return new AjaxResponse { Success = true };
        }

        #endregion

        /// <summary>
        /// 查询用户集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize("UserInfoManager")]
        public PagedResultDto<UserOut> GetUserList(RequestParam<UserOut> requestParam)
        {
            //反序列化参数对象
         
            var model = requestParam.Params;

            var queue = _userInfoRepository.GetAll();
            if (model != null && model.UserCode != null)
            {
                queue = _userInfoRepository.GetAll().Where(w =>
                            w.UserCode.Contains(model.UserCode) ||
                            w.UserNameCn.Contains(model.UserCode)
                ).OrderBy(o => o.UserNameCn);
            }
            else
            {
                queue = _userInfoRepository.GetAll().OrderBy(o => o.UserNameCn);
            }
            //此处添加超级管理员判断
            if (AbpSession.IsAdmin)
            {
                //禁用过滤器
                using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
                {
                    return queue.GetPagingData<UserInfo, UserOut>(requestParam.PagingDto);
                }
            }
            else
            {
                return queue.GetPagingData<UserInfo, UserOut>(requestParam.PagingDto);
            }
        }

        /// <summary>
        /// 获取用户对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public UserOut GetUserModel(long id)
        {
            //禁用过滤器
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                //获取用户数据
                UserOut data = _userInfoRepository.Get(id).MapTo<UserOut>();
                //获取用户扩展的数据
                data.UserInfoEx = _userInfoExtens.GetUserModel(id);

                //此处添加超级管理员判断,删除的用户仅超级管理员可以查看
                if (!AbpSession.IsAdmin && data.IsDeleted)
                {
                    return new UserOut();
                }
                else
                {
                    return data;
                }
            }
        }

        /// <summary>
        /// 保存用户信息(新增,修改)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize("UserInfoManager.SaveUser")]
        public async Task<AjaxResponse> SaveUserModel(UserInput model)
        {
          

            if (model.ID == null)
            {
                UserInfo modelInput = model.MapTo<UserInfo>();
                //查询范围包含已经逻辑删除的数据
                using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
                {
                    //唯一键冲突必须要先进行验证
                    var res = await _userInfoManager.FindByNameAsync(modelInput.UserCode);
                    if (res != null)
                    {
                        throw new UserFriendlyException("账号重复", "您输入的账号:" + model.UserCode + "重复!");
                    }
                }
                //新增用户设置默认图标
                modelInput.ImageUrl = "m";
                if (modelInput.Sex == "0")
                    modelInput.ImageUrl = "w";
                //新增用户对象(新增前默认验证UserCode的账号是否存在如果存在不做新增)
                await _userInfoManager.CreateAsync(modelInput);
                _unitOfWorkManager.Current.SaveChanges();
                //获取新增后的主键id
                model.ID = modelInput.Id;
                // 新增用户默认订阅所有通知
                await _sysNotificationAppService.UserSubscriptionNotificationInfoAll(modelInput.Id);
            }
            else
            {
                long id = Convert.ToInt64(model.ID);
                //更新数据时不对数据是否逻辑删除做判断
                using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
                {
                    //获取需要更新的数据
                    UserInfo data = _userInfoRepository.Get(id);
                    //映射需要修改的数据对象
                    UserInfo modelInput = ObjectMapper.Map(model, data);

                    //提交修改
                    await _userInfoManager.UpdateAsync(modelInput);
                }
                //清除缓存
                _cacheManagerExtens.RemoveUserInfoCache(id);
            }

            bool isFlag = await _userInfoExtens.UpdateUserInfo(model);
            if (!isFlag)
            {
                throw new UserFriendlyException("用户扩展信息", "用户扩展信息保存失败!");

            }
            return new AjaxResponse { Success = true };
        }

        /// <summary>
        /// 删除用户对象(逻辑删除)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [RemoteService(false)]
        [AbpAuthorize("UserInfoManager.DelUser")]
        public async Task DelUserModel(List<UserInput> model)
        {
            foreach (var item in model)
            {
                long id = Convert.ToInt64(item.ID);
                //删除用户时跳过当前登录用户
                if (id != AbpSession.UserId)
                {
                    await _userInfoRepository.DeleteAsync(id);
                    //清除缓存
                    _cacheManagerExtens.RemoveUserInfoCache(id);
                }

            }
        }

        /// <summary>
        /// 获取登录用户所有授权模块以及动作
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        [DisableAuditing]
        [RemoteService(false)]
        public object GetUserPermission()
        {
            List<RoleToPermissionCache> resData = new List<RoleToPermissionCache>();
            List<string> roleList = AbpSession.UserRoleList;
            foreach (var item in roleList)
            {
                //获取的是动作按钮授权
                var permissionList = _cacheManagerExtens.GetRoleToPermissionCache(Convert.ToInt64(item));
                    //.Where(w => !string.IsNullOrEmpty(w.PermissionName));
                foreach (var pitem in permissionList.ToList())
                {
                    var data = resData.Where(w =>
                      w.MenuId == pitem.MenuId
                     && w.HandleName == pitem.HandleName
                      && w.PermissionName == pitem.PermissionName
                      );
                    if (!data.Any())
                    {
                        resData.Add(pitem);
                    }
                }
            }

            List<MenuActionPermissionCache> menuActionPermissionList = _cacheManagerExtens.GetMenuActionPermissionCache();
            var permissionData = menuActionPermissionList  .Where(w => w.IsActive == true  && w.RequiresAuthModel!="3");
            foreach (var item in permissionData)
            {
                var data = resData.Where(w =>
                      (w.MenuId == item.MenuId && w.HandleName == item.MenuName && item.IsMenu && w.PermissionName == item.PermissionName) || 
                      (w.MenuId == item.MenuId && w.HandleName == item.ActionName && !item.IsMenu && w.PermissionName == item.PermissionName)
                      );
                if (!data.Any())
                {
                    resData.Add(new RoleToPermissionCache() {
                        MenuName= item.MenuName,
                        HandleName= item.IsMenu? item.MenuName: item.ActionName 
                    });
                }
            }

            var userData = _cacheManagerExtens.GetUserInfoCache(AbpSession.UserId.Value);

            return new
            {
                User = new
                {
                    UserNameCn = AbpSession.UserNameCn,
                    UserCode = AbpSession.UserCode,
                    IsAdmin = AbpSession.IsAdmin,
                    ImageUrl = userData.ImageUrl,
                    OrgCode = userData.OrgCode
                },
                Permission = resData
            };
        }

        /// <summary>
        /// 保存用户自己维护的个人信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AjaxResponse> SeetingUserInfo(UserInfoInput model)
        {
            long id = Convert.ToInt64(model.ID);
            //获取需要更新的数据
            UserInfo data = _userInfoRepository.Get(id);
            //映射需要修改的数据对象
            UserInfo m = ObjectMapper.Map(model, data);
            //提交修改
            await _userInfoManager.UpdateAsync(m);
            //清除缓存
            _cacheManagerExtens.RemoveUserInfoCache(id);

            return new AjaxResponse { Success = true };
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [RemoteService(false)]
        [DisableAuditing]
        public async Task<AjaxResponse> UpdateUserPass(UserPassInput model)
        {
            if (model.NewPass != model.CheckPass)
            {
                throw new UserFriendlyException("密码修改", "您修改的两次密码输入不一致");
            }
            long id = Convert.ToInt64(model.ID);
            //获取需要更新的数据
            UserInfo data = _userInfoRepository.Get(id);
            //验证密码
            var verificationResult = _userInfoManager.VerifyPassword(data.Password, model.OldPass);

            if (!verificationResult )
            {
                throw new UserFriendlyException("密码修改", "您输入的旧密码有误请重新输入");
            }
            //更新原密码
            data.Password = new PasswordHasher<UserInfo>().HashPassword(data, model.NewPass);
            //提交修改
            await _userInfoManager.UpdateAsync(data);
            return new AjaxResponse { Success = true };
        }

        /// <summary>
        /// 设置用户图像
        /// </summary>
        /// <param name="imgId"></param>
        /// <returns></returns>
        [DisableAuditing]
        public async Task<AjaxResponse> SaveAvatars(string imgId)
        {
            if (string.IsNullOrEmpty(imgId))
            {
                throw new UserFriendlyException("设置用户图像", "请选择用户图像");
            }
            //获取需要更新的数据
            UserInfo data = _userInfoRepository.Get(AbpSession.UserId.Value);
            if (data.ImageUrl == imgId)
            {
                return new AjaxResponse { Success = true };
            }
            data.ImageUrl = imgId;
            //提交修改
            await _userInfoManager.UpdateAsync(data);
            //设置完成头像后清空
            _cacheManagerExtens.RemoveUserInfoCache(AbpSession.UserId.Value);
            return new AjaxResponse { Success = true };
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回重置后的密码</returns>
        [RemoteService(false)]
        [AbpAuthorize("UserInfoManager.ResetPass")]
        public async Task<AjaxResponse> ResetUserPass(long id)
        {
            //获取需要更新的数据
            UserInfo data = _userInfoRepository.Get(id);
            //更新原密码
            data.Password = new PasswordHasher<UserInfo>().HashPassword(data, ConstantConfig.DefaultPassword);
            //提交修改
            await _userInfoManager.UpdateAsync(data);
            return new AjaxResponse { Success = true, Result = ConstantConfig.DefaultPassword };
        }

    }
}
