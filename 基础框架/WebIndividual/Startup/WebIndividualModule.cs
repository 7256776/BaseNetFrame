using Abp.AspNetCore;
using Abp.AspNetCore.SignalR;
using Abp.AutoMapper;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.EntityFrameworkCore;
using NetCoreFrame.Core;
using NetCoreFrame.Web;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace WebIndividual
{
    [DependsOn(
        typeof(AbpAspNetCoreModule),
        typeof(AbpAspNetCoreSignalRModule),
        typeof(NetCoreFrameWebModule)
        )]
    public class WebIndividualModule : AbpModule
    {
        /// <summary>
        /// 初始化前
        /// </summary>
        public override void PreInitialize()
        {

            //注册EfDBContext,或者在Startup启动类注册
            Configuration.Modules.AbpEfCore().AddDbContext<WebIndividualDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    options.DbContextOptions.UseSqlServer(options.ExistingConnection);
                }
                else
                {
                    options.DbContextOptions.UseSqlServer(options.ConnectionString);
                }
            });
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //这里会自动去扫描程序集中配置好的映射关系
            //DapperExtensions.SetMappingAssemblies(new List<Assembly> { Assembly.GetExecutingAssembly() });

            //Configuration.Modules.AbpAutoMapper().Configurators.Add(
            //    // 扫描程序集，查找从AutoMapper.Profile继承的类
            //    cfg => cfg.AddProfiles(Assembly.GetExecutingAssembly())
            //);

        }

        /// <summary>
        /// 请求初始化
        /// </summary>
        public override void PostInitialize()
        {

        }

    }
}
