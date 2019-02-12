using System.Web.Mvc;

namespace Frame.Sample
{
    public class LayoutSampleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LayoutSample";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LayoutSample_default",
                "LayoutSample/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}