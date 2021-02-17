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
        /// <summary>
        /// 初始化注册数据库连接
        /// </summary>
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


            Configuration.Modules.AbpEfCore().AddDbContext<WorkFlowDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    WorkFlowInfrastructureConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    WorkFlowInfrastructureConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
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