using Abp.Owin;
using Frame.Web;
using Frame.WebApi;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Owin;


/*
 设置启动类命名 通过webconfig配置
  <appSettings>
    <add key = "owin:appStartup" value="Frame.Entrance.Startup" />
 </appSettings>
 */
[assembly: OwinStartup("Frame.Web.Startup", typeof(Startup))]
namespace Frame.Web
{
    public class Startup
    {
        public virtual void Configuration(IAppBuilder app)
        {
            app.UseAbp();

            //配置跨域访问
            app.UseCors(CorsOptions.AllowAll);
            //使用OAuth 2.0 密码认证模式
           app.UseOAuthAuthorizationServer(OAuthOptions.CreateServerOptions());
             //app.UseOAuthBearerTokens(OAuthOptions.CreateServerOptions());

            //设置abp基于token的验证方式
            app.UseOAuthBearerAuthentication(AccountController.OAuthBearerOptions);

            //注册cookie属性
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //登录入口
                LoginPath = new PathString("/J_Account/Login")  
            });
            ////app.UseStaticFiles();
            ////app.UseEmbeddedFiles(); //允许暴露嵌入式文件到web中！

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.MapSignalR();

            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
