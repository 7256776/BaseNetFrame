using Abp.Auditing;
using Abp.Web.Mvc.Authorization;
using System.Web.Mvc;

namespace Frame.Web
{
    [AbpMvcAuthorize]
    [DisableAuditing]
    public class J_ComponentsController : FrameExtAbpController
    {

        public J_ComponentsController()
        {
        }

        public ActionResult QuickSidebar()
        {
            return View();
        }

        public ActionResult TopToolsMenu()
        {
            return View();
        }

        public ActionResult SearchForm()
        {
            return View();
        }

        public ActionResult SearchDropdown()
        {
            return View();
        }

        public ActionResult SearchPage()
        {
            return View();
        }

        public ActionResult UserInfoExtens()
        {
            return View();
        }

        public ActionResult HeadToolButton()
        {
            return View();
        }

    }
}