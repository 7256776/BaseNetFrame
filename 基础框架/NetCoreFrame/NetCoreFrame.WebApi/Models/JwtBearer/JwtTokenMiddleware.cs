using Abp.Runtime.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace NetCoreFrame.WebApi
{
    /// <summary>
    /// Jwt验证中间件 自定义验证
    /// 如果仅仅是WebApi服务似乎不用添加到服务的HTTP请求管道
    /// </summary>
    public static class JwtTokenMiddleware
    {
        public static IApplicationBuilder UseJwtTokenMiddleware(this IApplicationBuilder app, string schema = JwtBearerDefaults.AuthenticationScheme)
        {
            return app.Use(async (ctx, next) =>
            {
                #region 
                /*
                 * //伪代码 示例
                 获取头信息里面的token
                 if (ctx.Request.Headers.ContainsKey("Authorization"))
                 {
                  var tokenBearer = ctx.Request.Headers["Authorization"];
                  string token = tokenBearer.ToString().Split(' ')[1];
                  var jwtHandler = new JwtSecurityTokenHandler();
                  //获取token信息对象
                  jwtHandler.ReadJwtToken();
                  //验证token
                  jwtHandler.ValidateToken();
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
