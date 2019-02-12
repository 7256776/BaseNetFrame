using System.Web.Mvc;

namespace Frame.Sample
{
    public class SampleDataSourceAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SampleDataSource";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SampleDataSource_default",
                "SampleDataSource/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}