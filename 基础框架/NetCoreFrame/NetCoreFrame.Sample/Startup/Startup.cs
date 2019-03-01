using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCoreFrame.Sample.Controllers;
using Newtonsoft.Json;

namespace NetCoreFrame.Sample
{
    public class LocalSystemClock : Microsoft.Extensions.Internal.ISystemClock
    {
        public DateTimeOffset UtcNow => DateTime.Now;
    }

    public class Startup : NetCoreFrame.Web.Startup
    {

        public Startup(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }


        /// <summary>
        /// 注入服务配置
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            base.BuildConfigureServices(services);
             
            //Redis缓存SqlServerCache
            services.AddDistributedRedisCache(options =>
            {
                options.InstanceName = "TestDb";
                options.Configuration = _appConfiguration.GetConnectionString("RedisConnectionString");       //localhost
            });

            //数据库缓存SqlServerCache
            //services.AddDistributedSqlServerCache(options =>
            //{
            //    options.SystemClock = new LocalSystemClock();                                                    //本地时间
            //    options.ConnectionString = _appConfiguration.GetConnectionString("Default");    //数据库连接
            //    options.SchemaName = "dbo";                                                                              //数据库对象名称
            //    options.TableName = "AspNetCoreCache";                                                            //数据库存缓存的对象
            //    options.DefaultSlidingExpiration = TimeSpan.FromSeconds(10);                             //10秒钟过期
            //    options.ExpiredItemsDeletionInterval = TimeSpan.FromMinutes(5);                        //刷新间隔
            //});

            //添加验证码配置
            services.Configure<GeetestOptions>(_appConfiguration.GetSection("GeetestOptions"));
            // 配置Log4Net日志
            return services.AddAbp<NetCoreFrameSampleModule>(
                      options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                          f => f.UseAbpLog4Net().WithConfig("log4net.config")
                          ));
        }

        /// <summary>
        /// 注册配置信息
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public override void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        { 
            base.Configure(app, env, loggerFactory);
        }

    }
}
