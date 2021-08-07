using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace AppFrame.Models
{

    public class Startup : NetCoreFrame.Web.Startup
    {

        public Startup(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            startupOptionModel.LoginPath = "/AppLogin/Login";
            startupOptionModel.MapRouteController = "AppHome";
            startupOptionModel.MapRouteAction = "Index";
        }

        /// <summary>
        /// 注入服务配置
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            base.BuildConfigureServices(services);
            //获取上传文件大小设置
            var multipartBodyLengthLimit = _appConfiguration["File:MultipartBodyLengthLimit"];
            long limit = 0;
            long.TryParse(multipartBodyLengthLimit, out limit);
            //设置上传文件大小
            services.Configure<FormOptions>(x =>
            {
                x.MultipartBodyLengthLimit = limit * 1024 * 1024;   //还可以使用格式: M_KB_B
            });

            // 配置Log4Net日志
            return services.AddAbp<AppFrameModule>(
                      options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                          f => f.UseAbpLog4Net().WithConfig("log4net.config")
                          ));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public override void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            base.Configure(app, env, loggerFactory);
            // 扩展其他目录允许访问
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "Uploads")),//允许访问的目录
                RequestPath = "/Uploads",//访问目录的名称
                EnableDirectoryBrowsing = true
            });
        }




    }
}
