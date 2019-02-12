using Abp.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 实现系统用户管理对象
    /// </summary>
    public class UserInfoManager : SysUserInfoManager<UserInfo>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfoStore"></param>
        public UserInfoManager(
            UserInfoStore userInfoStore,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<UserInfo> passwordHasher,
            IEnumerable<IUserValidator<UserInfo>> userValidators,
            IEnumerable<IPasswordValidator<UserInfo>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<UserInfo>> logger,
            //UserClaimsPrincipalFactory claimsPrincipalFactory,
            ICacheManagerExtens cacheManagerExtens,
            IPermissionManager permissionManager,
            IAbpSessionExtens abpSessionExtens
            ) :
            base(
                userInfoStore,
                optionsAccessor,
                passwordHasher,
                userValidators,
                passwordValidators,
                keyNormalizer,
                errors,
                services,
                logger,
                //claimsPrincipalFactory,
                cacheManagerExtens,
                permissionManager,
                abpSessionExtens
                )
        {

        }

    }
}
