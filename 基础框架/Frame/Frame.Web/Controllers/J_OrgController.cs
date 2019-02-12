using Abp.Auditing;
using Abp.Web.Mvc.Authorization;
using Frame.Application;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Frame.Web
{
    [DisableAuditing]
    public class J_OrgController : FrameExtAbpController
    {
        private readonly ISysOrgAppService _sysOrgAppService;

        public J_OrgController(ISysOrgAppService sysOrgAppService)
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
        public async Task<JsonResult> SaveSysOrgModel(SysOrgInput model)
        {
            var ajaxResponse = await _sysOrgAppService.SaveSysOrgModel(model);
            return Json(ajaxResponse);
        }

        /// <summary>
        /// 删除一个组织机构节点
        /// </summary>
        /// <param name="id"></param>
        [AbpMvcAuthorize("OrgManager.DelSysOrg")]
        public JsonResult DelSysOrg(Guid id)
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
        public JsonResult GetSysOrgModel(string orgId)
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
        public JsonResult CheckOrgCode(Guid? orgId,string orgCode)
        {
            var result = _sysOrgAppService.CheckOrgCode(orgId, orgCode);
            return Json(result);
        }
    }
}