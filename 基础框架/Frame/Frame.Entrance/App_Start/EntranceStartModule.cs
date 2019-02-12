using Abp.AutoMapper;
using Abp.Modules;
using Abp.Web.Mvc;
using Frame.Web;
using System.Reflection;
using System.Web.Optimization;

namespace Frame.Entrance
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(AbpWebMvcModule),
        typeof(StartModuleWeb)
   )]
    public class EntranceStartModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //AreaRegistration.RegisterAllAreas();
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //注册当前项目的自定义css样式等文件(同名称的注册名称会覆盖掉原有的)
            Frame.Main.Entrance.BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

    }


}