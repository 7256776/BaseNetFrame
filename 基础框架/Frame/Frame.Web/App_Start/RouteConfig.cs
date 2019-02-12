using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Frame.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //WebApi路由
            routes.MapHttpRoute(
                name: "FrameApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            routes.MapRoute(
                name: "JLRoute",
                url: "{controller}/{action}/{id}",
                //defaults: new { controller = "J_Account", action = "Login", id = UrlParameter.Optional }
                defaults: new { controller = "J_Home", action = "index", id = UrlParameter.Optional }
                //namespaces: new string[] { "Frame.Web" }
            );
        }
    }
}
