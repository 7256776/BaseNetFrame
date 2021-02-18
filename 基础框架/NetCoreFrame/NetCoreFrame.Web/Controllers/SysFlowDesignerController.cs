using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;
using NetCoreWorkFlow.Application;
using NetCoreWorkFlow.Core;

namespace NetCoreFrame.Web.Controllers
{
    [DisableAuditing]
    public class SysFlowDesignerController : NetCoreFrameControllerBase
    {

        private readonly ISysWorkFlowSettingAppService _sysWorkFlowSettingAppService;
        private readonly ISysWorkFlowRoleAppService _sysWorkFlowRoleAppService;
        private readonly ISysWorkFlowBaseInfoAppService _sysWorkFlowBaseInfoAppService;

        public SysFlowDesignerController(
            ISysWorkFlowSettingAppService sysWorkFlowSettingAppService,
            ISysWorkFlowRoleAppService sysWorkFlowRoleAppService,
            ISysWorkFlowBaseInfoAppService sysWorkFlowBaseInfoAppService

            )
        {
            _sysWorkFlowSettingAppService = sysWorkFlowSettingAppService;
            _sysWorkFlowRoleAppService = sysWorkFlowRoleAppService;
            _sysWorkFlowBaseInfoAppService = sysWorkFlowBaseInfoAppService;
        }

        #region 
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult FlowUserSelect()
        {
            return View();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult FlowConfig()
        {
            return View();
        }

        #region 设计器

        /// <summary>
        ///  获取流程对象
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> GetWorkFlowSetting([FromBody] string id)
        {
            var data = await _sysWorkFlowSettingAppService.GetWorkFlowSetting(id);
            return Json(data);
        }

        /// <summary>
        /// 查询流程集合
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> GetWorkFlowSettingList()
        {
            var data = await _sysWorkFlowSettingAppService.GetWorkFlowSettingList();
            return Json(data);
        }

        /// <summary>
        /// 新增流程配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> InserWorkFlowSetting([FromBody] SysWorkFlowSettingInput model)
        {
            await _sysWorkFlowSettingAppService.InserWorkFlowSetting(model);
            return Json(model.Id);
        }

        /// <summary>
        /// 新增流程配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> UpdataWorkFlowSetting([FromBody] SysWorkFlowSettingInput model)
        {
            await _sysWorkFlowSettingAppService.UpdataWorkFlowSetting(model);
            return Json(model.Id);
        }

        /// <summary>
        /// 删除流程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> DeleteWorkFlowSetting([FromBody] string id)
        {
            await _sysWorkFlowSettingAppService.DeleteWorkFlowSetting(id);
            return Json(true);
        }
        #endregion

        #region 流程角色配置
        /// <summary>
        /// 查询角色集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> GetWorkFlowRoleList()
        {
            var data =await _sysWorkFlowRoleAppService.GetWorkFlowRoleList();
            return Json(data);
        }

        /// <summary>
        /// 查询角色对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> GetRoleModel([FromBody] string id)
        {
            var data = await _sysWorkFlowRoleAppService.GetWorkFlowRoleModel(id);
            return Json(data);
        }

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> SaveWorkFlowRole([FromBody] SysWorkFlowRoleInput model)
        {
            var ajaxResponse = await _sysWorkFlowRoleAppService.SaveWorkFlowRole(model);
            return Json(ajaxResponse);
        }

        /// <summary>
        /// 删除角色对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public Task<JsonResult> DelRoleModel([FromBody] string id)
        {
            _sysWorkFlowRoleAppService.DelWorkFlowRole(new List<string>() { id });
            return Task.FromResult(Json(true));
        }
        #endregion




        #region 用户选择窗口
        [AbpMvcAuthorize]
        public JsonResult GetFlowOrg()
        {
            var data = _sysWorkFlowBaseInfoAppService.GetFlowOrgAll();
            return Json(data);
        }

        [AbpMvcAuthorize]
        public JsonResult GetFlowUserPaging([FromBody]FlowPagingParam<SysFlowUserSearch> flowPagingDto)
        {
            var data = _sysWorkFlowBaseInfoAppService.GetFlowUserPaging(flowPagingDto);
            return Json(data);
        }

        #endregion
    }
}