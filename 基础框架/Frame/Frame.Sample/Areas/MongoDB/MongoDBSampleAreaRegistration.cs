using System.Web.Mvc;

namespace Frame.Sample
{
    public class MongoDBAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MongoDB";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MongoDB_default",
                "MongoDB/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}