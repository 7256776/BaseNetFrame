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
    public class SysAuditLogsController : NetCoreFrameControllerBase
    {
        private readonly ISysAuditLogsAppService _sysAuditLogsAppService;

        public SysAuditLogsController(
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
        [AbpMvcAuthorize("LogManager")]
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
            //页面取消操作成功判断执行完成即可,刷新列表
            int res= _sysAuditLogsAppService.DelAuditLog();
            return Json(res >= 0 ? true : false);
        }

    }
}