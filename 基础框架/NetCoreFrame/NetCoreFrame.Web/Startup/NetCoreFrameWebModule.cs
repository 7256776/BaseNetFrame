using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.SignalR;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Resources.Embedded;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using System.Reflection;

namespace NetCoreFrame.Web
{
    [DependsOn(
        typeof(AbpAspNetCoreModule),
        typeof(AbpAspNetCoreSignalRModule),
        typeof(NetCoreFrameApplicationModule)
        )]
    public class NetCoreFrameWebModule : AbpModule
    {

        public override void PreInitialize()
        {

            //注册嵌入资源 目录View下的所有视图文件
            Configuration.EmbeddedResources.Sources.Add(new EmbeddedResourceSet("/Views/", Assembly.GetExecutingAssembly(), "NetCoreFrame.Web.Views"));

            //加载本地化资源文件(由于资源文件在该程序集) 目前采用的是根据目录加载因此此处未使用
            //NetCoreFrameLocalizationConfigurer.LocalEmbedded(Configuration.Localization, Assembly.GetExecutingAssembly(), "NetCoreFrame.Web.Localization.JsonSource");

            /*
             * 注册所有服务层请求动态生成webapi服务(所有请求均为POST)
             * 可以通过ConfigureControllerModel对动态创建api的服务进行配置
             */
            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(NetCoreFrameApplicationModule).GetAssembly(),
                     moduleName: "frame",
                     useConventionalHttpVerbs: false
                 );

        }


        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(NetCoreFrameWebModule).GetAssembly());

        }
    }
}
