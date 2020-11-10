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
    public static class JwtBearerAuthentication
    {
        /// <summary>
        /// 配置 JwtBearer 授权验证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddJwtBearerAuthentication(this IServiceCollection services, IConfiguration configuration, string schema = JwtBearerDefaults.AuthenticationScheme)
        { 
            bool isEnabled = bool.Parse(configuration["Authentication:JwtBearer:IsEnabled"]);

            //两种配置
            //配置详细的授权对象
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  //授权方案名称, 用于验证环节对应方案名称确定配置对象 例如方案名称:JwtBearer"
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            #region 参数配置列表
            /*
              Authentication": {
                  "JwtBearer": {
                      "IsEnabled": "true",
                      "SecurityKey": "密钥",
                      "Issuer": "颁发者名称",
                      "Audience": "颁发给目标名称"
                  }
              }
            */
            #endregion
            //配置基本的授权对象 JwtBearerDefaults.AuthenticationScheme 是授权方案名称与验证函数 UseJwtTokenMiddleware 环节保持一致
            services.AddAuthentication(schema).AddJwtBearer(options =>
            {
                options.Audience = configuration["Authentication:JwtBearer:Audience"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    
                    // 是否验证签名密钥必须匹配!
                    ValidateIssuerSigningKey = isEnabled,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:JwtBearer:SecurityKey"])),

                    // 验证JWT发行者(iss)的声明
                    ValidateIssuer = isEnabled,
                    ValidIssuer = configuration["Authentication:JwtBearer:Issuer"],

                    // 验证JWT接受者(aud)的声明
                    ValidateAudience = isEnabled,
                    ValidAudience = configuration["Authentication:JwtBearer:Audience"],

                    // 验证令牌过期
                    ValidateLifetime = isEnabled,

                    // 允许服务器时间偏移量
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    //添加服务访问请求事件
                    OnMessageReceived = QueryStringTokenResolver
                };
            });
        }
    
        /// <summary>
        ///  用于获取授权tokey不在头信息的请求，目前主要考虑signalr
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static Task QueryStringTokenResolver(MessageReceivedContext context)
        {
            //此处用于授权SignalR javascript客户端.SignalR无法发送授权头。我们从查询字符串中获取加密文本
            if (!context.HttpContext.Request.Path.HasValue ||
                !context.HttpContext.Request.Path.Value.StartsWith("/signalr"))
            {
                // 获取signalr客户
                return Task.CompletedTask;
            }

            //url参数获取tokeny
            var qsAuthToken = context.HttpContext.Request.Query["enc_auth_token"].FirstOrDefault();
            if (qsAuthToken == null)
            {
                return Task.CompletedTask;
            }
            // 
            context.Token =qsAuthToken;
            return Task.CompletedTask;
        }
    }
}
