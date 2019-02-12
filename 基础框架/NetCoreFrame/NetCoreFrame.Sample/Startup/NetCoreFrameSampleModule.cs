using Abp.AspNetCore;
using Abp.AspNetCore.SignalR;
using Abp.AutoMapper;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.EntityFrameworkCore;
using NetCoreFrame.Web;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace NetCoreFrame.Sample
{
    [DependsOn(
        typeof(AbpAspNetCoreModule),
        typeof(AbpAspNetCoreSignalRModule),
        typeof(NetCoreFrameWebModule)
        )]
    public class NetCoreFrameSampleModule : AbpModule
    {
         
        public override void PreInitialize()
        {
            //注册EfDBContext,或者在Startup启动类注册
            Configuration.Modules.AbpEfCore().AddDbContext<SampleFrameDbContext>(options =>
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

        public override void PostInitialize()
        {
           
        }


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





    }


    //public static class NetCoreFrameDbContextConfigurer
    //{
    //    public static void Configure(DbContextOptionsBuilder<SampleFrameDbContext> builder, string connectionString)
    //    {
    //        builder.UseSqlServer(connectionString);
    //    }

    //    public static void Configure(DbContextOptionsBuilder<SampleFrameDbContext> builder, DbConnection connection)
    //    {
    //        builder.UseSqlServer(connection);
    //    }
    //}

}
