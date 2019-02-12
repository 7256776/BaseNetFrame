using Frame.Main.Entrance;
using Microsoft.Owin;
using Owin;

/*
 设置启动类命名 通过webconfig配置
  <appSettings>
    <add key = "owin:appStartup" value="Frame.Entrance.Startup" />
 </appSettings>
 */
[assembly: OwinStartup("Frame.Entrance.Startup", typeof(Startup))]
namespace Frame.Main.Entrance
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
