using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using NetCoreWorkFlow.Core;

namespace NetCoreWorkFlow.Application
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(NetCoreWorkFlowCoreModule)
        )]
    public class NetCoreWorkFlowApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(NetCoreWorkFlowApplicationModule).GetAssembly();
            IocManager.RegisterAssemblyByConvention(thisAssembly);
        }

        public override void PostInitialize()
        {
        }



    }
}
