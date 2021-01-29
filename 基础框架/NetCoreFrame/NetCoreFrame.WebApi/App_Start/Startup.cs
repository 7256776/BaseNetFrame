using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.Extensions;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace NetCoreFrame.WebApi
{
    public class Startup
    {
        //默认的Cors策略名称
        private const string _defaultCorsPolicyName = "localhost";

        protected readonly IConfigurationRoot _appConfiguration;

        /// <summary>
        /// 启动类构造
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            //设置项目wwwroot物理路径  
            ConstantConfig.WebWWWrootPath = hostingEnvironment.WebRootPath + "\\";
            //设置项目根目录物理路径
            ConstantConfig.WebContentRootPath = hostingEnvironment.ContentRootPath + "\\";
            _appConfiguration = AppConfigurations.Get(
                hostingEnvironment.ContentRootPath,
                hostingEnvironment.EnvironmentName,
                hostingEnvironment.IsDevelopment());

            //Environment
        }

        /// <summary>
        /// 运行时调用。使用此方法将服务添加到容器中
        /// 这货就是冒充 Global.asax 其他程序引用后需要重写并且调用 BuildConfigureServices构建基本配置
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            BuildConfigureServices(services);
            // 配置Abp和依赖注入 
            return services.AddAbp<NetCoreFrameWebApiModule>(
               options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                   f => f.UseAbpLog4Net().WithConfig("log4net.config")
               )
           );
        }

        /// <summary>
        /// 运行时调用。使用此方法配置HTTP请求管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //加载session中间件在 UseMvc 之前调用
            app.UseSession();
            //配置Abp中间件 初始化ABP框架
            app.UseAbp(options =>
            {
                //禁用请求本地化自动添加
                options.UseAbpRequestLocalization = false;
            });
            //配置跨域访问中间件(必须使用 services.AddCors() 设置筛选规则) **方案二
            app.UseCors(_defaultCorsPolicyName);

            //异常处理
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/SysError");
            }
            //静态文件服务中间件(默认文件夹wwwroot)
            app.UseStaticFiles();

            //服务端授权中间件 (Jwt或OIDC)
            app.UseAuthTokenMiddleware(_appConfiguration);

            //abp本地化中间件
            app.UseAbpRequestLocalization();
            //启用嵌入资源
            app.UseEmbeddedFiles();
            //注册路由
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "defaultWithArea",
                    template: "{area}/{controller}/{action}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=index}/{id?}");
            });
            // Swagger服务中间件 URL: /swagger
            app.UseSwagger();
            // swagger-ui 资源中间件能够支持 (HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(_appConfiguration["App:ServerRootAddress"] + "/swagger/v1/swagger.json", "AuthScaffold API V1");
                options.IndexStream = () => Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("NetCoreFrame.WebApi.wwwroot.swagger.ui.index.html");
            });
        }

        #region 构建启动服务配置
        public virtual void BuildConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc(
                options =>
                {
                    //设置跨域筛选器(必须使用 services.AddCors() 设置筛选规则) **方案一 (两种方案选其一)
                    options.Filters.Add(new CorsAuthorizationFilterFactory(_defaultCorsPolicyName));
                }
            );

            #region json
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //设置默认获取本地时区
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            });
            #endregion

            //注册HttpClient 对象
            services.AddHttpClient();

            #region Session
            //Session 保存到内存
            //services.AddDistributedMemoryCache();
            services.AddSession();
            #endregion

            #region 注入用户授权
            services.AddLogging();
            services.AddFrameIdentity<UserInfo>()
                .AddFrameUserManager<UserInfoManager>()
                .AddFrameUserStore<UserInfoStore>()
                .AddFrameSignInManager<SignInManager>()
                .AddFrameUserClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
                .AddPermissionChecker<PermissionChecker>()
                .AddDefaultTokenProviders();
            #endregion

            //授权中间件 (Jwt或OIDC)
            services.AddBearerAuthentication(_appConfiguration);

            #region  设置跨域访问的规则
            services.AddCors(options => options.AddPolicy(_defaultCorsPolicyName,
                    builder =>
                        builder.WithOrigins(
                            // 获取appsettings.json配置的跨域地址(App:CorsOrigins) ,如设置 '*' 表示允许全部地址跨域
                            _appConfiguration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                )
            );

            #endregion

            #region 配置SwaggerUI
            // Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "NetCoreFrame API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);

                // 定义正在使用的无记名授权方案
                options.AddSecurityDefinition("bearerAuth", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            });
            #endregion

        }
        #endregion

    }



}
