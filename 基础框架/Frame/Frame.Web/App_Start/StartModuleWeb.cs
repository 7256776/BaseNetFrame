using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Resources.Embedded;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using Castle.MicroKernel.Registration;
using Frame.Application;
using Frame.WebApi;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Frame.Web
{
    /// <summary>
    /// 设置依赖对象
    /// </summary>
    [DependsOn(
        typeof(StartModuleApplication), 
        typeof(StartModuleWebApi),
        typeof(AbpAutoMapperModule),
        typeof(AbpWebMvcModule),
        typeof(AbpWebSignalRModule)
    )]
    public class StartModuleWeb : AbpModule
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void PreInitialize()
        {
            //反伪造 防止CSRF攻击
            Configuration.Modules.AbpWeb().AntiForgery.IsEnabled = true;

            //注册嵌入资源 目录View下的所有视图文件
            Configuration.EmbeddedResources.Sources.Add(new EmbeddedResourceSet("/Views/", Assembly.GetExecutingAssembly(), "Frame.Web.Views"));


            //设置映射关系 默认按照对象属性名称匹配
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                //Add your custom AutoMapper mappings here...
                //mapper.CreateMap<,>()
            });
             
        }

        /// <summary>
        /// 再次初始化
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //注册验证过滤器
            IocManager.IocContainer.Register(
            Component
                .For<IAuthenticationManager>()
                .UsingFactoryMethod(() => HttpContext.Current.GetOwinContext().Authentication)
                .LifestyleTransient()
                );

            //注册启动
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //动态模型绑定器
            ModelBinders.Binders.Add(typeof(JObject), new JObjectModelBinder());
        }
    }
}