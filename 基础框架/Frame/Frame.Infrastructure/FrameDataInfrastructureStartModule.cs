using Abp.EntityFramework;
using Abp.Modules;
using Frame.Core;
using System.Reflection;

namespace Frame.Infrastructure
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(StartModuleCore))]
    public class FrameDataInfrastructureStartModule : AbpModule
    {
        public override void PreInitialize()
        {
           
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

    }
}