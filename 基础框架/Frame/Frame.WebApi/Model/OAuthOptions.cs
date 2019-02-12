using Abp.Dependency;
using Frame.Core;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using System;

namespace Frame.WebApi
{
    public class OAuthOptions
    {/// <summary>
     /// Gets or sets the server options.
     /// </summary>
     /// <value>The server options.</value>
        private static OAuthAuthorizationServerOptions _serverOptions;

        /// <summary>
        /// Creates the server options.
        /// </summary>
        /// <returns>OAuthAuthorizationServerOptions.</returns>
        public static OAuthAuthorizationServerOptions CreateServerOptions()
        {
            if (_serverOptions == null)
            {
                //注册授权验证业务类实现 
                var provider = IocManager.Instance.Resolve<WebApiServerOAuthProvider>();
                //注册安全令牌创建于刷新业务类实现
                var refreshTokenProvider = IocManager.Instance.Resolve<ApiRefreshTokenProvider>();
                _serverOptions = new OAuthAuthorizationServerOptions
                {
                    TokenEndpointPath = new PathString("/oauth/token"),
                    Provider = provider,
                    RefreshTokenProvider = refreshTokenProvider,
                    //token有效期时间
                    AccessTokenExpireTimeSpan =TimeSpan.FromMinutes(ConstantConfig.WebApiExpires),
                    AllowInsecureHttp = true
                };
            }
            return _serverOptions;
        }

    }
}