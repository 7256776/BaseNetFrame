using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Runtime.Security;
using Abp.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetCoreFrame.Core
{
    public class SysSignInManager<TUser> : SignInManager<TUser>, ITransientDependency
        where TUser : SysUserInfo<TUser>
    {
        private readonly IUserInfoRepository _userInfoRepository;
        public SysUserInfoStore<TUser> _sysInUserInfoStore;

        private readonly SysUserClaimsPrincipalFactory<TUser> _sysUserClaimsPrincipalFactory;
        private readonly SysUserInfoManager<TUser> _sysUserInfoManager;

        public SysSignInManager(
            IUserInfoRepository userInfoRepository,
            SysUserInfoManager<TUser> sysUserInfoManager,
            SysUserInfoStore<TUser> sysInUserInfoStore,
            SysUserClaimsPrincipalFactory<TUser> claimsFactory,
            IHttpContextAccessor contextAccessor,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<TUser>> logger,
            IAuthenticationSchemeProvider schemes)
            : base(
                sysUserInfoManager,
                contextAccessor,
                claimsFactory,
                optionsAccessor,
                logger,
                schemes)
        {
            _userInfoRepository = userInfoRepository;
            _sysInUserInfoStore = sysInUserInfoStore;
            _sysUserInfoManager = sysUserInfoManager;
            _sysUserClaimsPrincipalFactory = claimsFactory;
        }
    

        public virtual async Task SignOutAndSignInAsync(ClaimsIdentity identity, bool isPersistent)
        {
            await Context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //await SignOutAsync();

            await SignInAsync(identity, isPersistent);
        }

        /// <summary>
        /// 注册登录用户信息
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="isPersistent"></param>
        /// <returns></returns>
        public virtual async Task SignInAsync(ClaimsIdentity identity, bool isPersistent)
        {
            await Context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            await Context.SignInAsync(
               CookieAuthenticationDefaults.AuthenticationScheme,
               new ClaimsPrincipal(identity),
               new AuthenticationProperties { IsPersistent = isPersistent });
        }

        /// <summary>
        /// 注销登录账号
        /// </summary>
        /// <returns></returns>
        public override Task SignOutAsync()
        {
            //Context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //return base.SignOutAsync();
            return Context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// 注册登录账号
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isPersistent"></param>
        /// <param name="authenticationMethod"></param>
        /// <returns></returns>
        public override Task SignInAsync(TUser user, bool isPersistent, string authenticationMethod = null)
        {
            return base.SignInAsync(user, isPersistent, authenticationMethod);
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
            //获取用户对象
            var userModel = await _sysInUserInfoStore.GetLoginUserAsync(model.UserCode);
            //验证用户信息 
            if (userModel == null)
            {
                throw new UserFriendlyException(nameof(LoginResultType.InvalidUserName), " 未获取到该" + model.UserCode + "的用户信息。");
                //return new SysLoginResult<TUser>(LoginResultType.InvalidUserNameOrEmailAddress, userModel);
            }
            if (!userModel.IsActive)
            {
                //未激活的用户不做登录
                throw new UserFriendlyException(nameof(LoginResultType.UserIsNotActive));
                //return new SysLoginResult<TUser>(LoginResultType.UserIsNotActive, userModel);
            }

            //验证用户密码(该方案验证流程是通过用户id从数据库获取信息后与提供的明文未加密的密码匹配)
            //var isOk = await _sysUserInfoManager.CheckPasswordAsync(userModel, model.Password);
            var verificationResult = _sysUserInfoManager.PasswordHasher.VerifyHashedPassword(model, userModel.Password, model.Password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new UserFriendlyException(nameof(LoginResultType.InvalidPassword));
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
            //记录最后一次登录系统的日期
            userModel.LastLoginTime = DateTime.Now;
            await _userInfoRepository.SetLastLoginTime(userModel.Id);
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
            var identity = await _sysUserClaimsPrincipalFactory.CreateAsync(userModel);
            //成功后返回用户对象
            return new SysLoginResult<TUser>(userModel, identity.Identity as ClaimsIdentity);
        }



    }
}
