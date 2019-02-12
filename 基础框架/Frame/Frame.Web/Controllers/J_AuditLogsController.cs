using Abp.Auditing;
using Abp.Web.Mvc.Authorization;
using Frame.Application;
using Frame.Core;
using System.Web.Mvc;

namespace Frame.Web
{
    [AbpMvcAuthorize]
    [DisableAuditing]
    public class J_AuditLogsController : FrameExtAbpController
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
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagingDto"></param>
        /// <returns></returns>
        public JsonResult GetAuditLogList(SysAuditInput model, PagingDto pagingDto)
        {
            model.PagingModel = pagingDto;
            var data = _sysAuditLogsAppService.GetAuditLogList(model);
            return Json(data);
        }

        public JsonResult DelAuditLog()
        {
            int res= _sysAuditLogsAppService.DelAuditLog();
            return Json(res >= 0 ? true : false);
        }

    }
}