﻿using Abp;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Localization;
using Abp.Runtime.Session;
using System.Threading.Tasks;

namespace Frame.Core
{
    /// <summary>
    /// 构造授权验证
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public abstract class PermissionCheckerCore<TUser> : IPermissionChecker, ITransientDependency //, IIocManagerAccessor
            where TUser : SysUserInfo<TUser>
    {

        //public IIocManager IocManager { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private readonly UserInfoManager _userInfoManager;

        private readonly ILocalizationManager _localizationManager;

        public IAbpSession AbpSession { get; set; }
     
        /// <summary>
        /// Constructor.
        /// </summary>
        protected PermissionCheckerCore(UserInfoManager userInfoManager, ILocalizationManager localizationManager)
        {
            //
            _userInfoManager = userInfoManager;
            _localizationManager = localizationManager;
            AbpSession = NullAbpSession.Instance;

        }

        /// <summary>
        /// 验证授权(当前登录用户)
        /// </summary>
        /// <param name="permissionName">授权名称</param>
        /// <returns></returns>
        public async Task<bool> IsGrantedAsync(string permissionName)
        {
            var isGranted = await _userInfoManager.IsGrantedAsync(permissionName);
            if (!isGranted)
            {
                //AuthorizationException(permissionName);
            }
            return isGranted;
        }

        /// <summary>
        /// 验证授权(指定用户)
        /// </summary>
        /// <param name="user">指定用户对象</param>
        /// <param name="permissionName">授权名称</param>
        /// <returns></returns>
        public async Task<bool> IsGrantedAsync(UserIdentifier user, string permissionName)
        {
            bool isGranted = await _userInfoManager.IsGrantedAsync(user, permissionName);
            if (!isGranted)
            {
                //AuthorizationException(permissionName);
            }
            return isGranted;
        }

        /// <summary>
        /// 获取abp系统默认本地化信息(待解决)
        /// </summary>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        public string AuthorizationException(string permissionName)
        {
            throw new AbpAuthorizationException(
                string.Format(
                        _localizationManager.GetString(AbpConsts.LocalizationSourceName, "AllOfThesePermissionsMustBeGranted"),
                        string.Join(", ", permissionName)
                    )
                );
        }

    }
}
