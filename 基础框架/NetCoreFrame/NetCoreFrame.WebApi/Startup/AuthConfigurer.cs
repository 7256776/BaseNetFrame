using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Abp.Runtime.Security;
using NetCoreFrame.Core;

namespace NetCoreFrame.WebApi
{

    public static class AuthConfigurer
    {
        /*
         参数配置列表
        "Authentication": {
            "JwtBearer": {
                "IsEnabled": "true",
                "SecurityKey": "密钥",
                "Issuer": "颁发者名称",
                "Audience": "颁发给目标名称"
            }
        },
        */

        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<AuthConfigurerModel>(configuration.GetSection("Authentication"));

            if (bool.Parse(configuration["Authentication:JwtBearer:IsEnabled"]))
            {
                //services.AddAuthentication(options => {
                //    options.DefaultAuthenticateScheme = "JwtBearer";
                //    options.DefaultChallengeScheme = "JwtBearer";
                //}).AddJwtBearer("JwtBearer", options =>
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.Audience = configuration["Authentication:JwtBearer:Audience"];

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // 是否验证签名密钥必须匹配!
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:JwtBearer:SecurityKey"])),

                        // 验证JWT发行者(iss)的声明
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Authentication:JwtBearer:Issuer"],

                        // 验证JWT接受者(aud)的声明
                        ValidateAudience = true,
                        ValidAudience = configuration["Authentication:JwtBearer:Audience"],

                        // 验证令牌过期
                        ValidateLifetime = true,

                        // 允许服务器时间偏移量
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = QueryStringTokenResolver
                    };
                });
            }
        }

        /// <summary>
        /// 此方法用于授权SignalR javascript客户端.SignalR无法发送授权头。我们从查询字符串中获取加密文本
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static Task QueryStringTokenResolver(MessageReceivedContext context)
        {
            if (!context.HttpContext.Request.Path.HasValue ||
                !context.HttpContext.Request.Path.Value.StartsWith("/signalr"))
            {
                // 获取signalr客户
                return Task.CompletedTask;
            }

            var qsAuthToken = context.HttpContext.Request.Query["enc_auth_token"].FirstOrDefault();
            if (qsAuthToken == null)
            {
                // Cookie value does not matches to querystring value
                return Task.CompletedTask;
            }

            // 从cookie设置身份令牌 AppConsts.DefaultPassPhrase
            context.Token = SimpleStringCipher.Instance.Decrypt(qsAuthToken, ConstantConfig.DefaultPassPhrase);
            return Task.CompletedTask;
        }
    }
}
