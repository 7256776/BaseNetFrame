using Abp.Dapper;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;

namespace NetCoreWorkFlow.Core
{
    [DependsOn(
        typeof(AbpEntityFrameworkCoreModule),
        typeof(AbpDapperModule)
        )]
    public class NetCoreWorkFlowCoreModule : AbpModule
    {

        private readonly IConfigurationRoot _appConfiguration;

        public NetCoreWorkFlowCoreModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.GetConfigurationCache();
        }

        public override void PreInitialize()
        {
            //设置数据连接字符串的名称对应 connectionStrings配置的Name值
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(ConstantConfig.DBDefaultName);
           
            //设置工作单元
            Configuration.UnitOfWork.IsolationLevel = IsolationLevel.ReadCommitted;
            Configuration.UnitOfWork.Timeout = TimeSpan.FromMinutes(1);

            //注册EfDBContext,或者在Startup启动类注册
            Configuration.Modules.AbpEfCore().AddDbContext<NetCoreWorkFlowDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    NetCoreWorkFlowDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    NetCoreWorkFlowDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                }
            });

        }

        public override void Initialize()
        { 
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            //获取启动时间
            //IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }



    }
}
