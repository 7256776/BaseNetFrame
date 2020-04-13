using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Application;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.Web.Controllers
{
    [DisableAuditing]
    public class SysOrgController : NetCoreFrameControllerBase
    {
        private readonly ISysOrgAppService _sysOrgAppService;

        public SysOrgController(ISysOrgAppService sysOrgAppService)
        {
            _sysOrgAppService = sysOrgAppService;
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
        /// 获取组织结构
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("OrgManager")]
        public JsonResult GetSysOrgList()
        {
            var data = _sysOrgAppService.GetSysOrgList();
            return Json(data) ;
        }

        /// <summary>
        /// 保存(新增/修改)
        /// </summary>
        /// <param name="model"></param>
        [AbpMvcAuthorize("OrgManager.SaveSysOrg")]
        public async Task<JsonResult> SaveSysOrgModel([FromBody]SysOrgInput model)
        {
            var ajaxResponse = await _sysOrgAppService.SaveSysOrgModel(model);
            return Json(ajaxResponse);
        }

        /// <summary>
        /// 删除一个组织机构节点
        /// </summary>
        /// <param name="id"></param>
        [AbpMvcAuthorize("OrgManager.DelSysOrg")]
        public JsonResult DelSysOrg([FromBody]Guid id)
        {
            _sysOrgAppService.DelSysOrg(id);
            return Json(true);
        }

        /// <summary>
        /// 查找某个组织机构节点
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("OrgManager")]
        public JsonResult GetSysOrgModel([FromBody]string orgId)
        {
            var id = new Guid(orgId);
            var data = _sysOrgAppService.GetSysOrgModel(id);
            return Json(data);
        }

        /// <summary>
        /// 判断OrgCode是否存在
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="orgCode"></param>
        /// <returns>true: 已存在  false: 不存在</returns>
        [AbpMvcAuthorize]
        public JsonResult CheckOrgCode([FromBody]SysOrgData model)
        {
            var result = _sysOrgAppService.CheckOrgCode(model);
            return Json(result);
        }
    }
}