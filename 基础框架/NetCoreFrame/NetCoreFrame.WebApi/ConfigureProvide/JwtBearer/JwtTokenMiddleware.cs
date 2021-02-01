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
    /// Jwt��֤�м�� �Զ�����֤
    /// ���������WebApi�����ƺ�������ӵ������HTTP����ܵ�
    /// </summary>
    public static class JwtTokenMiddleware
    {
        public static IApplicationBuilder UseJwtTokenMiddleware(this IApplicationBuilder app, IConfigurationRoot configuration, string schema = JwtBearerDefaults.AuthenticationScheme)
        {
            return app.Use(async (ctx, next) =>
            {
                #region �Զ�����֤��ʽ 
                //ע: ͨ����Ҫ����֤������չ����д����¿��Բ������·�ʽ,ͨ��ֱ�Ӳ������·�ʽ�ȿ��������֤
                /*  
                if (ctx.Request.Headers.ContainsKey("Authorization") &&
                    ctx.Request.Headers["Authorization"].Count > 0 &&
                    ctx.Request.Headers["Authorization"][0] != "null")
                {
                    //ͨ��ͷ��Ϣ��ȡ����Ȩtokey
                    var tokenBearer = ctx.Request.Headers["Authorization"];
                    string token = tokenBearer.ToString().Split(' ')[1];

                    #region ����token��Ϣ����
                    var jwtHandler = new JwtSecurityTokenHandler();
                    var jwtSecurityToken = jwtHandler.ReadJwtToken(token);
                    #endregion

                    #region ͨ������tokey��֤
                    //��ȡJwt������Ϣ
                    var tokenValidationParameters = new TokenValidationParameters
                    {
                        // �Ƿ���֤ǩ����Կ����ƥ��!
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:JwtBearer:SecurityKey"])),
                        // ��֤JWT������(iss)������
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Authentication:JwtBearer:Issuer"],
                        // ��֤JWT������(aud)������
                        ValidateAudience = true,
                        ValidAudience = configuration["Authentication:JwtBearer:Audience"],
                        // ��֤���ƹ���
                        ValidateLifetime = true,
                        // ���������ʱ��ƫ����
                        ClockSkew = TimeSpan.Zero
                    };
                    SecurityToken securityToken = new JwtSecurityToken();
                    var tokenHandler = new JwtSecurityTokenHandler();
                    //��֤��ɺ󷵻���Ȩ������Ϣ,�˴���Ҫ������֤ʧ�ܵ����, ĿǰĬ����ֱ�ӷ���500�Ĵ���
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
