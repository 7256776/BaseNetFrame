using Abp.EntityFramework;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Microsoft.EntityFrameworkCore;
using NetCoreFrame.Core;
using System;
using System.Data.Common;
using System.Reflection;

namespace NetCoreFrame.Infrastructure
{
    [DependsOn(
        typeof(AbpEntityFrameworkCoreModule), 
        typeof(NetCoreFrameCoreModule))]
    public class NetCoreFrameInfrastructureModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpEfCore().AddDbContext<InfrastructureDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    NetCoreFrameInfrastructureConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    NetCoreFrameInfrastructureConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                }
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
           
        }




    }
}