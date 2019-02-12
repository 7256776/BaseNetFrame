using Abp.Dependency;
using Abp.Modules;
using Abp.Resources.Embedded;
using Abp.Web.Mvc;
using Frame.Application;
using Frame.Core;
using Frame.MongoDB;
using Frame.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Frame.Sample
{
    [DependsOn(
       typeof(StartModuleWeb),
       typeof(StartModuleMongoDB)
   )]
    public class SampleStartModule : AbpModule
    {
        public override void PreInitialize()
        {
            //注册嵌入资源 目录Areas下的所有视图文件
            Configuration.EmbeddedResources.Sources.Add(new EmbeddedResourceSet("/Areas/", Assembly.GetExecutingAssembly(), "Frame.Sample.Areas"));
            //设置示例菜单
            Configuration.Navigation.Providers.Add<SampleNavigationMenusProvider>();
        }


        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //AreaRegistration.RegisterAllAreas();
            //注册当前项目的路由(该步骤可忽略)
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //注册当前项目的自定义css样式等文件(同名称的注册名称会覆盖掉原有的)
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public override void PostInitialize()
        {
           
        }


         

    }


}