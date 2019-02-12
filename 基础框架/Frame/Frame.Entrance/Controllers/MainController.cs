using Frame.Web;
using System.Web.Mvc;

namespace Frame.Entrance.Controllers
{
    public class MainController : FrameExtAbpController
    {
        public ActionResult Index()
        {
            return View();
        }

   
    }
}