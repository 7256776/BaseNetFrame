using Abp.AspNetCore.Mvc.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Core;
using NetCoreFrame.Web;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Sample.Controllers
{
    public class SampleAccountController : NetCoreFrameControllerBase
    {

        /// <summary>
        /// 通过重新定向js加载的视图进行原模块的重写
        /// 1. 新增重写的控制器以及视图, 不能与原模块目录同名
        /// 2. 设置与原模块相同的目录, 设置与原模块同名js, 并修改js文件中的  template: Vue.frameTemplate('SampleAccount/Index')
        /// </summary>
        /// <param name="auditLogRepository"></param>
        public SampleAccountController(IRepository<SysSetting, Guid> auditLogRepository)
        {
        }


        [AbpMvcAuthorize]
        public IActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize]
        public ActionResult UserSettings()
        {
            return View();
        }

    }
}
