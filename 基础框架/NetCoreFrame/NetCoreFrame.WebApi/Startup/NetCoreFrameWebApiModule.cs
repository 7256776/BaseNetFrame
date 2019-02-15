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
