using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace NetCoreFrame.WebApi
{
    /// <summary>
    /// Jwt验证中间件 自定义验证
    /// 如果仅仅是WebApi服务似乎不用添加到服务的HTTP请求管道
    /// </summary>
    public static class JwtTokenMiddleware
    {
        public static IApplicationBuilder UseJwtTokenMiddleware(this IApplicationBuilder app, IConfigurationRoot configuration, string schema = JwtBearerDefaults.AuthenticationScheme)
        {
            return app.Use(async (ctx, next) =>
            {
                #region 自定义验证方式 
                //注: 通常需要对验证进行扩展或重写情况下可以采用如下方式,通常直接采用如下方式既可以完成验证
                /*  
                if (ctx.Request.Headers.ContainsKey("Authorization") &&
                    ctx.Request.Headers["Authorization"].Count > 0 &&
                    ctx.Request.Headers["Authorization"][0] != "null")
                {
                    //通过头信息获取到授权tokey
                    var tokenBearer = ctx.Request.Headers["Authorization"];
                    string token = tokenBearer.ToString().Split(' ')[1];

                    #region 解析token信息对象
                    var jwtHandler = new JwtSecurityTokenHandler();
                    var jwtSecurityToken = jwtHandler.ReadJwtToken(token);
                    #endregion

                    #region 通过请求tokey验证
                    //获取Jwt配置信息
                    var tokenValidationParameters = new TokenValidationParameters
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
                    SecurityToken securityToken = new JwtSecurityToken();
                    var tokenHandler = new JwtSecurityTokenHandler();
                    //验证完成后返回授权对象信息,此处需要处理验证失败的情况, 目前默认是直接返回500的错误
                    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                    #endregion
                }
                */
                #endregion
                if (ctx.User.Identity?.IsAuthenticated != true)
                {
                    var result = await ctx.AuthenticateAsync(schema);
                    if (result.Succeeded && result.Principal != null)
                    {
                        ctx.User = result.Principal;
                    }
                }
                await next();
            });
        }
    }

}
