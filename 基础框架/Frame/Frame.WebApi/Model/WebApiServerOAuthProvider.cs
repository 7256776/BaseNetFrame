using Abp.Dependency;
using Abp.UI;
using Frame.Core;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Frame.WebApi
{
    public class WebApiServerOAuthProvider : OAuthAuthorizationServerProvider, ITransientDependency
    {
        private readonly UserInfoManager _userInfoManager;
        private readonly IUserInfoRepository _userInfoRepository;
        //private readonly IUserInfoAppService _userInfoAppService;

        public WebApiServerOAuthProvider(
            UserInfoManager userInfoManager,
            IUserInfoRepository userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
            _userInfoManager = userInfoManager;
        }

        /// <summary>
        /// 验证Client的身份（ClientId以及ClientSecret）
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            //获取Headers包含的验证信息,是指Client可以按照Basic身份验证的规则提交ClientId和ClientSecret
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                //TryGetFormCredentials：是指Client可以把ClientId和ClientSecret放在Post请求的form表单中提交
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            context.Validated(clientId);
            context.OwinContext.Set<string>("clientId", clientId);
            context.OwinContext.Set<string>("clientSecret", clientSecret);

            return base.ValidateClientAuthentication(context);
            //return Task.FromResult<object>(null);

        }

        /// <summary>
        /// 对客户端进行授权
        /// 客户端访问方式必须是"grant_type", "client_credentials"
        /// 暂未具体实现()
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var clientId = context.OwinContext.Get<string>("clientId");
            var clientSecret = context.OwinContext.Get<string>("clientSecret");
            //验证是授权信息是否正确,并发放授权信息,添加自定的验证方式默认是true
            bool isAuth = true;
            /*

           添加自定义的验证逻辑

           */
            if (isAuth)
            {
                var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                //创建授权对象,并记录到HttpContext.Current.User.Identity
                oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, context.ClientId));
                //授权安全令牌后,记录当前授权用户id 到会话对象中
                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { "as:clientId", context.ClientId },
                    { "as:clientSecret", clientSecret }
                });
                //发布授权
                var ticket = new AuthenticationTicket(oAuthIdentity, props);
                context.Validated(ticket);
            }
            //return Task.FromResult<object>(null);
            return base.GrantClientCredentials(context);
        }

        /// <summary>
        /// 对具体用户账号与密码进行授权
        /// 客户端访问方式必须是"grant_type" = "password"
        /// 通过客户端的context.UserName与context.Password获取客户端传递的用户验证信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //var tenantId = context.Request.Query["tenantId"];

            //此处只做简单判断验证，可以添加自定义的验证逻辑
            if (string.IsNullOrEmpty(context.UserName))
            {
                context.SetError("未提供用户账号信息.");
                context.Rejected();
                return;
            }
            //password验证需要通过 UserName 以及 Password获取,因此需要重新添加到到clientId与clientSecret便于生成restoken
            context.OwinContext.Set<string>("clientId", context.UserName);
            context.OwinContext.Set<string>("clientSecret", context.Password);

            #region 验证登录信息
            //获取登录用户信息
            UserInfo userModel = _userInfoRepository.GetUserInfoByUserCode(context.UserName);
            //验证用户信息 
            if (userModel == null)
            {
                throw new UserFriendlyException(nameof(LoginResultType.InvalidUserNameOrEmailAddress), "用户账号无效!");
            }
            //未激活的用户不做登录
            if (!userModel.IsActive)
            {
                throw new UserFriendlyException(nameof(LoginResultType.UserIsNotActive), "用户未激活!");
            }
            //验证登录的密码
            var verificationResult = _userInfoManager.VerifyPassword(userModel.Password, context.Password);
            if (!verificationResult)
            {
                throw new UserFriendlyException(nameof(LoginResultType.InvalidPassword), "用户密码无效!");
            }
            SysLoginResult<UserInfo> sysLoginResult = await _userInfoManager.CreateIdentityAsync(userModel);
            #endregion

            //设置登录验证成功后获取到的授权信息
            var oAuthIdentity = new ClaimsIdentity(sysLoginResult.Identity);
            //var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            //授权安全令牌后,记录当前授权用户id 到安全令牌对象中
            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { "as:clientId", context.UserName }
                });
            //
            var ticket = new AuthenticationTicket(oAuthIdentity, props);
            context.Validated(ticket);
            //return Task.FromResult<object>(null);
            //return await base.GrantResourceOwnerCredentials(context);
        }

        /// <summary>
        /// 添加安全令牌相关信息到客户响应请求中
        /// (返回信息到客户端)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            if (context.Properties.IssuedUtc != null)
            {
                //安全令牌发布日期.
                DateTimeOffset Issued = new DateTimeOffset();
                bool flag = DateTimeOffset.TryParse(context.Properties.IssuedUtc.ToString(), out Issued);
                if (flag)
                    context.AdditionalResponseParameters.Add("issued", Issued.LocalDateTime);
            }

            if (context.Properties.ExpiresUtc != null)
            {
                //安全令牌有效期截止日期
                DateTimeOffset expires = new DateTimeOffset();
                bool flag = DateTimeOffset.TryParse(context.Properties.ExpiresUtc.ToString(), out expires);
                if (flag)
                    context.AdditionalResponseParameters.Add("expires", expires.LocalDateTime);
            }
            return Task.FromResult<object>(null);
        }


    }
}