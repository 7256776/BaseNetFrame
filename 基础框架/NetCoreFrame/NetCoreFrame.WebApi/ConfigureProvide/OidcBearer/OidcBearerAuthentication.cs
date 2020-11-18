using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace NetCoreFrame.WebApi
{
     

    public static class OidcBearerAuthentication
    {
        /// <summary>
        /// 配置 IdentityServer4 授权验证
        /// 主要适用于资源服务器端的配置信息
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddOidcBearerAuthentication(this IServiceCollection services, IConfiguration configuration, string schema = JwtBearerDefaults.AuthenticationScheme)
        {
            //配置基本的资源服务器授权对象
            var authenticationBuilder = services.AddAuthentication(schema);
            //
            bool isEnabled = bool.Parse(configuration["Authentication:JwtBearer:IsEnabled"]);
            if (isEnabled)
            {
                //配置资源服务器授权信息
                authenticationBuilder.AddJwtBearer(options =>
                {
                    options.Authority = "http://localhost:18377/";
                    options.RequireHttpsMetadata = false;
                    options.Audience = "ResourceApi";
                    //options.MetadataAddress = "http://localhost:18377/.well-known/openid-configuration";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.FromSeconds(0)         //授权token有效时间偏移值(单位秒)
                        //ValidateAudience = true                               //是否验证授权 接受方
                    };

                    //options.Events=new JwtBearerEvents
                    //{
                    //    OnAuthenticationFailed= OnAuthenticationFailed,
                    //    OnChallenge =  OnChallenge,
                    //    OnTokenValidated = OnTokenValidated,
                    //    OnMessageReceived = QueryStringTokenResolver
                    //};

                });
            }
        }

        public static Task OnTokenValidated(TokenValidatedContext context)
        {
            var token = (context.SecurityToken as JwtSecurityToken).RawData;

            return Task.CompletedTask;
        }

        public static Task OnChallenge(JwtBearerChallengeContext context)
        {

            return Task.CompletedTask;
        }

        public static Task OnAuthenticationFailed(AuthenticationFailedContext context)
        {

            return Task.CompletedTask;
        }

        /// <summary>
        ///  用于获取授权tokey不在头信息的请求，目前主要考虑signalr
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static Task QueryStringTokenResolver(MessageReceivedContext context)
        {
            return Task.CompletedTask;
        }


        #region 扩展待验证

        public static void AddOAuthBearerAuthentication(this IServiceCollection services, IConfiguration configuration, string schema = JwtBearerDefaults.AuthenticationScheme)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = schema;
                options.DefaultSignInScheme = schema;
                options.DefaultChallengeScheme = OAuthDefaults.DisplayName;
            })
            .AddCookie()
            .AddOAuth(OAuthDefaults.DisplayName, options =>
            {
                options.ClientId = "oauth.code";
                options.ClientSecret = "secret";
                options.AuthorizationEndpoint = "https://oidc.faasx.com/connect/authorize";
                options.TokenEndpoint = "https://oidc.faasx.com/connect/token";
                options.CallbackPath = "/signin-oauth";
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                options.SaveTokens = true;
                // 事件执行顺序 ：
                // 1.创建Ticket之前触发
                options.Events.OnCreatingTicket = context => Task.CompletedTask;
                // 2.创建Ticket失败时触发
                options.Events.OnRemoteFailure = context => Task.CompletedTask;
                // 3.Ticket接收完成之后触发
                options.Events.OnTicketReceived = context => Task.CompletedTask;
                // 4.Challenge时触发，默认跳转到OAuth服务器
                // options.Events.OnRedirectToAuthorizationEndpoint = context => context.Response.Redirect(context.RedirectUri);
            });
        }

        #endregion




    }
}
