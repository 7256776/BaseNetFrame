using Microsoft.AspNetCore.Mvc;

namespace WebApiTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}