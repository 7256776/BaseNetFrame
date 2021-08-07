using Abp.AspNetCore;
using Abp.AspNetCore.SignalR;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Resources.Embedded;
using AppFrame.Core;
using NetCoreFrame.Core;
using NetCoreFrame.Web;
using System.Reflection;

namespace AppFrame.Models
{
    [DependsOn(
        typeof(AbpAspNetCoreModule),
        typeof(AbpAspNetCoreSignalRModule),
        typeof(NetCoreFrameWebModule)
        )]
    public class AppFrameModule : AbpModule
    {
        /// <summary>
        /// 初始化前
        /// </summary>
        public override void PreInitialize()
        {
            Configuration.EmbeddedResources.Sources.Add(new EmbeddedResourceSet("/Views/", Assembly.GetExecutingAssembly(), "AppFrame.Views"));

            //注册EfDBContext,或者在Startup启动类注册
            Configuration.Modules.AbpEfCore().AddDbContext<AppFrameDbContext>(options =>
            {
                NetCoreFrameDbContextConfigurer<AppFrameDbContext>.Configure(options);
            });
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// 请求初始化
        /// </summary>
        public override void PostInitialize()
        {

        }

    }
}
