using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Frame.Sample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //注册路由并且添加到路由集合索引 0 的位置
            //Route myRoute = new Route(
            //    url: "{controller}/{action}/{id}", 
            //    routeHandler: new MvcRouteHandler()
            // );
            ////设置默认路由的地址
            //Dictionary<string, object> dic = new Dictionary<string, object>();
            //dic.Add("controller", "Account");
            //dic.Add("action", "Login");
            //dic.Add("id", UrlParameter.Optional);
            //myRoute.Defaults = new RouteValueDictionary(dic);
            //routes.Insert(0, myRoute);
         
            //上面注册路由方式等同于下面的方式(只是把当前程序集的路由作为第一默认地址)

            //routes.MapRoute(
            //    name: "SampleRoute",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            //);

        }
    }
}
