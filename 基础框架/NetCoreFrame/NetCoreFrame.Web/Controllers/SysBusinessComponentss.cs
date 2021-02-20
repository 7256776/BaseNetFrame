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
    public class SysBusinessComponentss : NetCoreFrameControllerBase
    {

        public SysBusinessComponentss()
        {
        }

        public ActionResult OrgTreeSelection()
        {
            return View();
        }

 
    }
}