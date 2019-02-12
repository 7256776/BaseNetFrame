using Abp.Castle.Logging.Log4Net;
using Abp.Web;
using Castle.Facilities.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Frame.Sample
{
    /// <summary>
    /// AbpWebApplication<EntranceStartModule>
    /// FrameMvcApplication
    /// </summary>
    public class MvcApplication : AbpWebApplication<SampleStartModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            //AreaRegistration.RegisterAllAreas();
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                  f => f.UseAbpLog4Net().WithConfig(Server.MapPath("log4net.config"))
              );

            base.Application_Start(sender, e);
        }


    }
}
