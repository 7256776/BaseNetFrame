using Abp.AspNetCore.Mvc.Controllers;
using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace WebIndividual.Controllers
{
    [DisableAuditing]
    public class WebHomeController : AbpController
    {
        public IWebFileAppService _webFileAppService;

        public WebHomeController(IWebFileAppService webFileAppService)
        {
            _webFileAppService = webFileAppService;
        }

        public IActionResult Index()
        {
            List<WebDocPageDto> data = _webFileAppService.GetWebDocList();
            return View(data);
        }


    }
}
