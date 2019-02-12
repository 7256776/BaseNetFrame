﻿using Abp.AspNetCore;
using Abp.AspNetCore.SignalR.Hubs;
using Abp.Castle.Logging.Log4Net;
using Abp.Extensions;
using Abp.Runtime.Security;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Rewrite.Internal;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreFrame.Web
{
    public class Startup
    {
        //默认的Cors策略名称
        private const string _defaultCorsPolicyName = "localhost";

        protected readonly IConfigurationRoot _appConfiguration;

        protected StartupOptionModel startupOptionModel = new StartupOptionModel();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            //设置项目wwwroot物理路径   (这种写法很丑需改善)
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
        /// 这货就是冒充 Global.asax 其他程序引用后需要重写并且调用 BuildConfigureServices构建基本配置
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            BuildConfigureServices(services);

            // 配置Abp和依赖注入 
            return services.AddAbp<NetCoreFrameWebModule>(
               options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                   f => f.UseAbpLog4Net().WithConfig("log4net.config")
               )
           );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //加载session中间件在 UseMvc 之前调用
            app.UseSession();
            //自定义http跳转https重定向的中间件
            app.UseRewriter(new RewriteOptions().AddRedirectForwardedHttpToHttps());
            //Https 重定向中间件
            //app.UseHttpsRedirection();

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
                app.UseExceptionHandler("/J_Error");
            }

            //静态文件服务中间件(默认文件夹wwwroot)
            app.UseStaticFiles();
            //授权验证中间件
            app.UseAuthentication();
            //abp本地化中间件
            app.UseAbpRequestLocalization();
            //SignalR中间件
            app.UseSignalR(routes =>
            {
                routes.MapHub<AbpCommonHub>("/signalr");
            });
            //app.UseCookiePolicy();
            //读取Views文件夹下的js和css 
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Views")),
                RequestPath = new PathString("/Views"),
                ContentTypeProvider = new FileExtensionContentTypeProvider
                (
                    new Dictionary<string, string>(
                        StringComparer.OrdinalIgnoreCase){
                        { ".js", "application/javascript" },
                        { ".css", "text/css" }
                    }
                )
            });
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
                    template: "{controller=J_Home}/{action=index}/{id?}");
            });

            #region Swagger
            // Swagger服务中间件 URL: /swagger
            app.UseSwagger();
            // swagger-ui 资源中间件能够支持 (HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(_appConfiguration["App:ServerRootAddress"] + "/swagger/v1/swagger.json", "AuthScaffold API V1");
                options.IndexStream = () => Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("NetCoreFrame.Web.wwwroot.swagger.ui.index.html");
            });
            #endregion
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
                    //设置 自动验证防伪造令牌属性
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                }
            );

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //设置默认获取本地时区
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            });

            //注册HttpClient 对象
            services.AddHttpClient();

            #region Session
            //Session 保存到内存
            //services.AddDistributedMemoryCache();
            services.AddSession();
            #endregion

            #region cookies授权
            /* CookieAuthenticationDefaults.AuthenticationScheme
             * CookieAuthenticationDefaults.AuthenticationScheme 是特定Cookie认证方案的值。处理多个Cookie验证实例，并且需要限制对一个实例的授权时用。 
             */
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                //当用户尝试访问资源但尚未认证时，这是请求重定向的相对路径
                options.LoginPath = new PathString(startupOptionModel.LoginPath);
                //当用户尝试访问资源但没有通过任何授权策略时，这是请求会重定向的相对路径资源
                options.AccessDeniedPath = new PathString(startupOptionModel.LoginPath);
            });
            #endregion

            #region 注册EfDBContext,或者在启动类注册
            //services.AddAbpDbContext<NetCoreFrameDbContext>(options =>
            //{
            //    options.DbContextOptions.UseSqlServer(options.ConnectionString);
            //});
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

            //注册自定义的配置文件并绑定对象
            services.Configure<CustomWebResource>(_appConfiguration.GetSection("CustomWebResource"));
            //注册资源文件管理类
            services.AddScoped<IWebResourceManager, WebResourceManager>();

            services.AddSignalR();

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
