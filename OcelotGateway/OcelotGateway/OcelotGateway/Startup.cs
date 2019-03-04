using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotGateway
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        //默认的Cors策略名称
        private const string _defaultCorsPolicyName = "localhost";

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            #region 获取配置文件
            _configuration = new ConfigurationBuilder()
                                             .SetBasePath(env.ContentRootPath)
                                             .AddJsonFile("ocelot.configuration.json")
                                             .AddEnvironmentVariables()
                                             .Build();
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(_configuration);

            #region  设置跨域访问的规则
            services.AddCors(options => options.AddPolicy(_defaultCorsPolicyName,
                    builder =>
                        builder.WithOrigins(new string[] { "*" })
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                )
            );
            #endregion

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication("TestKey", options =>
            {
                options.Authority = "http://localhost:27749";
                options.RequireHttpsMetadata = false;
                options.ApiName = "apiA";
                options.JwtValidationClockSkew = TimeSpan.FromSeconds(0);       //token过期时间偏移量
            }
            );

            //设置网关
            services.AddOcelot(_configuration);
        }

     

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //配置跨域访问中间件(必须使用 services.AddCors() 设置筛选规则)
            app.UseCors(_defaultCorsPolicyName);
            //添加验证
            app.UseAuthentication();
            //Wait 等待其他加载完成后执行。
            app.UseOcelot().Wait();//;

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Ocelot网关启动出状况!");
            });
        }
    }
}
