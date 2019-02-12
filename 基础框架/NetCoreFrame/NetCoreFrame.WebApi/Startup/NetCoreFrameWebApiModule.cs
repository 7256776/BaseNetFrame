using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using NetCoreFrame.Application;

namespace NetCoreFrame.WebApi
{
    [DependsOn(
        typeof(AbpAspNetCoreModule), 
        typeof(NetCoreFrameApplicationModule)
        )]
    public class NetCoreFrameWebApiModule : AbpModule
    {

        public override void PreInitialize()
        {

            //注册嵌入资源 目录View下的所有视图文件
            //Configuration.EmbeddedResources.Sources.Add(new EmbeddedResourceSet("/Views/", Assembly.GetExecutingAssembly(), "NetCoreFrame.Web.Views"));

            //加载本地化资源文件(由于资源文件在该程序集)
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
            IocManager.RegisterAssemblyByConvention(typeof(NetCoreFrameWebApiModule).GetAssembly());

        }
    }
}
