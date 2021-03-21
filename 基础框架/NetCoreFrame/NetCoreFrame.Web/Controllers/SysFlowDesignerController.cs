using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.Dependency;
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
        private readonly ISysWorkFlowTypeAppService _sysWorkFlowTypeAppService;
        private readonly ISysWorkFlowDataSourceAppService _sysWorkFlowDataSourceAppService;
        
        private readonly ISysDataSourceFieldAppService _sysDataSourceFieldAppService;

        public SysFlowDesignerController(
            ISysWorkFlowSettingAppService sysWorkFlowSettingAppService,
            ISysWorkFlowRoleAppService sysWorkFlowRoleAppService,
            ISysWorkFlowBaseInfoAppService sysWorkFlowBaseInfoAppService,
            ISysWorkFlowTypeAppService sysWorkFlowTypeAppService,
            ISysWorkFlowDataSourceAppService sysWorkFlowDataSourceAppService,
            ISysDataSourceFieldAppService sysDataSourceFieldAppService

            )
        {
            _sysWorkFlowSettingAppService = sysWorkFlowSettingAppService;
            _sysWorkFlowRoleAppService = sysWorkFlowRoleAppService;
            _sysWorkFlowBaseInfoAppService = sysWorkFlowBaseInfoAppService;
            _sysWorkFlowTypeAppService = sysWorkFlowTypeAppService;
            _sysWorkFlowDataSourceAppService = sysWorkFlowDataSourceAppService;

            _sysDataSourceFieldAppService = sysDataSourceFieldAppService;
        }

        #region 
        /// <summary>
        /// 流程用户选择窗体
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

        /// <summary>
        /// 保存流程角色与用户关系
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> SaveWorkFlowRoleToUser([FromBody] SysWorkFlowRoleToUserInput model)
        {
            var row = await _sysWorkFlowRoleAppService.SaveWorkFlowRoleToUser(model);
            return Json(row);
        }

        /// <summary>
        /// 删除流程角色与用户关系
        /// </summary>
        /// <param name="flowPagingDto"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> DelWorkFlowRoleToUser([FromBody]SysWorkFlowRoleToUserInput model)
        {
            await _sysWorkFlowRoleAppService.DelWorkFlowRoleToUser(model);
            return Json(true);
        }

        /// <summary>
        /// 流程角色相关用户
        /// </summary>
        /// <param name="flowPagingDto"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public JsonResult GetFlowUserPaging([FromBody]FlowPagingParam<SysFlowUserSearch> flowPagingDto)
        {
            var data = _sysWorkFlowBaseInfoAppService.GetFlowUserPaging(flowPagingDto);
            return Json(data);
        }
        #endregion

        #region 审核流程类型

        /// <summary>
        /// 查询流程类型集合
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public JsonResult GetWorkFlowTypeList([FromBody]SysWorkFlowTypeModel model)
        {
            var data = _sysWorkFlowTypeAppService.GetWorkFlowTypeList(model);
            return Json(data);
        }

        /// <summary> 
        /// 查询角色对象       
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> GetWorkFlowTypeModel([FromBody]string id)
        {
            var data = await _sysWorkFlowTypeAppService.GetWorkFlowTypeModel(id);
            return Json(data);
        }

        /// <summary>
        /// 保存角色(新增,修改)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> SaveWorkFlowType([FromBody]SysWorkFlowTypeInput model)
        {
             await _sysWorkFlowTypeAppService.SaveWorkFlowType(model);
            return Json(true);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public Task<JsonResult> DelWorkFlowType([FromBody]List<string> ids)
        {
            _sysWorkFlowTypeAppService.DelWorkFlowType(ids);
            return Task.FromResult(Json(true));
        }
        #endregion

        #region 审核数据源
        /// <summary>
        /// 查询数据源集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public JsonResult GetWorkFlowDataSourceList([FromBody]SysWorkFlowDataSourceParam model)
        { 
            var data = _sysWorkFlowDataSourceAppService.GetWorkFlowDataSourceList(model);
            return Json(data);
        }

        /// <summary>
        /// 查询数据源对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> GetWorkFlowDataSource([FromBody] string id)
        {
            var data = await _sysWorkFlowDataSourceAppService.GetWorkFlowDataSource(id);
            return Json(data);
        }

        /// <summary>
        /// 保存数据源
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> SaveWorkFlowDataSource([FromBody] SysWorkFlowDataSourceInput model)
        {
            await _sysWorkFlowDataSourceAppService.SaveWorkFlowDataSource(model);
            return Json(true);
        }

        /// <summary>
        /// 删除数据源对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public Task<JsonResult> DelWorkFlowDataSource([FromBody]List<string> ids)
        {
            _sysWorkFlowDataSourceAppService.DelWorkFlowDataSource(ids);
            return Task.FromResult(Json(true));
        }

        /// <summary>
        /// 查询数据源关联字段集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public JsonResult GetDataStructure([FromBody] string dataSourceId)
        {
            var data = _sysDataSourceFieldAppService.GetDataStructure(dataSourceId);
            return Json(data);
        }

        /// <summary>
        /// 查询数据源关联字段集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public Task<JsonResult> SaveWorkFlowDataSourceItem([FromBody] List<SysWorkFlowDataSourceItemInput> list)
        {
            _sysDataSourceFieldAppService.SaveWorkFlowDataSourceItem(list);
            return Task.FromResult(Json(true));
        }
        #endregion



        #region 用户选择窗口
        /// <summary>
        /// 获取所有系统组织机构
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public JsonResult GetSysOrg()
        {
            var data = _sysWorkFlowBaseInfoAppService.GetSysOrgAll();
            return Json(data);
        }

        [AbpMvcAuthorize]
        public JsonResult GetSysUserPaging([FromBody]FlowPagingParam<SysFlowUserSearch> flowPagingDto)
        {
            var data = _sysWorkFlowBaseInfoAppService.GetSysUserPaging(flowPagingDto);
            return Json(data);
        }

        #endregion

    }
}