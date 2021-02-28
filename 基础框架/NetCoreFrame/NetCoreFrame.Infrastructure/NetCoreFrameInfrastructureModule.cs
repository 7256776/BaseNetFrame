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
            Configuration.Modules.AbpEfCore().AddDbContext<FrameDbContext>(options =>
            {
                DbContextConfigurer<FrameDbContext>.Configure(options);
            });

            Configuration.Modules.AbpEfCore().AddDbContext<WorkFlowDbContext>(options =>
            {
                DbContextConfigurer<WorkFlowDbContext>.Configure(options);
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