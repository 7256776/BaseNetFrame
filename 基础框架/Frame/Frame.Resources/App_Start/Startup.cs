using System;
using System.Threading.Tasks;
using Abp.Owin;
using Jurassic.Library.Resources;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Jurassic.Library.Resources
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseAbp();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                //AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //LoginPath = new PathString("/Home/Index")
            });
            //app.UseStaticFiles();
            //app.UseEmbeddedFiles(); //允许暴露嵌入式文件到web中！
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
