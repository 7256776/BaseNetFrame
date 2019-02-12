using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.Web.Controllers
{
    [AbpMvcAuthorize]
    [DisableAuditing]
    public class J_AuditLogsController : NetCoreFrameControllerBase
    {
        private readonly ISysAuditLogsAppService _sysAuditLogsAppService;

        public J_AuditLogsController(
            ISysAuditLogsAppService sysAuditLogsAppService
            )
        {
            _sysAuditLogsAppService = sysAuditLogsAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagingDto"></param>
        /// <returns></returns>
        public JsonResult GetAuditLogList([FromBody]RequestParam<dynamic> requestParam)
        { 
            var data = _sysAuditLogsAppService.GetAuditLogList(requestParam);
            return Json(data);
        }

        /// <summary>
        /// 清空日志
        /// </summary>
        /// <returns></returns>
        public JsonResult DelAuditLog()
        {
            int res= _sysAuditLogsAppService.DelAuditLog();
            return Json(res >= 0 ? true : false);
        }

    }
}