using Microsoft.AspNetCore.Mvc;

namespace NetCoreFrame.WebApi.Controllers
{
    public class HomeController : NetCoreFrameWebApiControllerBase
    {
        public ActionResult Index()
        {
            return Redirect("swagger");
        }

    }
}