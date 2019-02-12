using System;
using System.Threading.Tasks;
using Abp.Owin;
using Abp.Resources.Embedded;
using Frame.Sample;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

/*
 设置启动类命名 通过webconfig配置
  <appSettings>
    <add key = "owin:appStartup" value="Frame.Entrance.Startup" />
 </appSettings>
 */
[assembly: OwinStartup("Frame.Sample.Startup", typeof(Startup))]
namespace Frame.Sample
{
    public class Startup : Frame.Web.Startup
    {
        /// <summary>
        ///  继承基础框架的启动类
        /// </summary>
        /// <param name="app"></param>
        public override void Configuration(IAppBuilder app)
        {
            base.Configuration(app);
           
        }
    }
}
