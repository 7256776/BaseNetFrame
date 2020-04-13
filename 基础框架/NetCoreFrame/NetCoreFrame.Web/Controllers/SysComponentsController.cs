using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.Web.Controllers
{
    /// <summary>
    /// 组件启动类
    /// </summary>
    //[AbpMvcAuthorize]
    [DisableAuditing]
    public class SysComponentsController : NetCoreFrameControllerBase
    {

        public SysComponentsController()
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