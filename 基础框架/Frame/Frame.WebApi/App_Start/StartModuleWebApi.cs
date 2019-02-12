using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Frame.Application;
using Frame.Core;
using Swashbuckle.Application;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Frame.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
        typeof(AbpWebApiModule),
        typeof(StartModuleApplication)
        )]
    public class StartModuleWebApi : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void PreInitialize()
        {
           
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            #region 
            /*
             * 动态创建服务层的接口为 WebApi服务,例如服务层类名称为 MyService , 函数名称为 MyFn 
             * 因此Api地址= http://127.0.0.1/api/services/frame/myService/myFn
             * 注:如服务层接口类名称包含AppService后缀, 例如 UserInfo + AppService 的方式.所创建api的地址会自动剔除 (AppService),
             * 因此Api地址=:http://127.0.0.1/api/services/frame/userInfo/服务层函数名称
             */

            //仅注册ISysMenusAppService服务接口
            //Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
            //.For<ISysMenusAppService>("frame/menu")
            //.Build();
            #endregion

            //创建所有服务层的接口为api服务,并对添加了IgnoreFrameApi属性的服务忽略创建
            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
            .ForAll<IApplicationService>(typeof(StartModuleApplication).Assembly, "frame")
            .ForMethods(builder =>
            {
                if (builder.Method.IsDefined(typeof(IgnoreFrameApi)))
                {
                    builder.DontCreate = true;
                }
            })
            .Build();

            //Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
            //.For<IUserInfoAppService>("UserInfo/task")
            //.Build();

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
             
            // SwaggerUi 测试地址 http://localhost:2018/swagger/ui/index
            ConfigureSwaggerUi();

            //Configuration.Modules.AbpWebApi().

            //注册启动
            //AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// 初始化SwaggerUi
        /// </summary>
        private void ConfigureSwaggerUi()
        {
            Configuration.Modules.AbpWebApi().HttpConfiguration
            .EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Frame.WebApi");
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            })
            .EnableSwaggerUi(c =>
            {
                c.InjectJavaScript(Assembly.GetExecutingAssembly(), "Frame.WebApi.js.Swagger-Custom.js");
            });
        }
    }
}
 
 
 
 
 