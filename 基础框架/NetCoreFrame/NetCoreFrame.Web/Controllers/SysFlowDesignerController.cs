using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreFrame.Web.Controllers
{
    [DisableAuditing]
    public class SysFlowDesignerController : NetCoreFrameControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}