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
    /// Jwt��֤�м�� �Զ�����֤
    /// ���������WebApi�����ƺ�������ӵ������HTTP����ܵ�
    /// </summary>
    public static class JwtTokenMiddleware
    {
        public static IApplicationBuilder UseJwtTokenMiddleware(this IApplicationBuilder app, string schema = JwtBearerDefaults.AuthenticationScheme)
        {
            return app.Use(async (ctx, next) =>
            {
                #region 
                /*
                 * //α���� ʾ��
                 ��ȡͷ��Ϣ�����token
                 if (ctx.Request.Headers.ContainsKey("Authorization"))
                 {
                  var tokenBearer = ctx.Request.Headers["Authorization"];
                  string token = tokenBearer.ToString().Split(' ')[1];
                  var jwtHandler = new JwtSecurityTokenHandler();
                  //��ȡtoken��Ϣ����
                  jwtHandler.ReadJwtToken();
                  //��֤token
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
