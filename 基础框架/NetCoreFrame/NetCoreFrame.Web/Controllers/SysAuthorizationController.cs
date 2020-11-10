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
    [DisableAuditing]
    public class SysAuthorizationController : NetCoreFrameControllerBase
    {
        private readonly ISysApiAccountAppService _sysApiAccountAppService;
        private readonly ISysApiClientAppService _sysApiClientAppService;
        private readonly ISysApiResourceAppService _sysApiResourceAppService;
        private readonly ISysApiResourceToClientAppService _sysApiResourceToClientAppService;
        private readonly ISysApiClienToAccountAppService _sysApiClienToAccountAppService;

        public SysAuthorizationController(
            ISysApiAccountAppService sysApiAccountAppService,
            ISysApiClientAppService sysApiClientAppService,
            ISysApiResourceAppService sysApiResourceAppService,
            ISysApiResourceToClientAppService sysApiResourceToClientAppService,
            ISysApiClienToAccountAppService sysApiClienToAccountAppService
            )
        {
            _sysApiAccountAppService = sysApiAccountAppService;
            _sysApiClientAppService = sysApiClientAppService;
            _sysApiResourceAppService = sysApiResourceAppService;
            _sysApiResourceToClientAppService = sysApiResourceToClientAppService;
            _sysApiClienToAccountAppService = sysApiClienToAccountAppService;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        #region 授权账号
        /// <summary>
        /// 获取授权账号集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> GetSysApiAccountList([FromBody]SysApiAccountData model)
        {
            var list = await _sysApiAccountAppService.GetSysApiAccountList(model);
            return Json(list);
        }

        /// <summary>
        /// 获取授权账号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> GetSysApiAccount([FromBody]string id)
        {
            var model = await _sysApiAccountAppService.GetSysApiAccount(id);
            return Json(model);
        }

        /// <summary>
        /// 保存授权账号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> SaveSysApiAccount([FromBody]SysApiAccountInput model)
        {
            var flag = await _sysApiAccountAppService.SaveSysApiAccount(model);
            return Json(flag);
        }

        /// <summary>
        /// 删除授权账号
        /// </summary>
        /// <param name="id"></param>
        [AbpMvcAuthorize]
        public async Task<JsonResult> DelSysApiAccount([FromBody]string id)
        {
            List<string> strList = new List<string>();
            strList.Add(id);
            var flag = await _sysApiAccountAppService.DelSysApiAccount(strList);
            return Json(flag);
        }

        /// <summary>
        /// 删除授权账号
        /// </summary>
        /// <param name="ids"></param>
        [AbpMvcAuthorize]
        public async Task<JsonResult> DelMultiSysApiAccount([FromBody]List<string> ids)
        {
            var flag = await _sysApiAccountAppService.DelSysApiAccount(ids);
            return Json(flag);
        }

        #endregion

        #region 授权客户
        /// <summary>
        /// 获取授权客户集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> GetSysApiClientAllList([FromBody]SysApiClientData model)
        {
            var list = await _sysApiClientAppService.GetSysApiClientList(model);
            return Json(list);
        }

        /// <summary>
        /// 获取授权有效的客户集合
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> GetSysApiClientList()
        {
            SysApiClientData model = new SysApiClientData() { IsActive =true};
            var list = await _sysApiClientAppService.GetSysApiClientList(model);
            return Json(list);
        }

        /// <summary>
        /// 获取授权客户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> GetSysApiClient([FromBody]string id)
        {
            var model = await _sysApiClientAppService.GetSysApiClient(id);
            return Json(model);
        }

        /// <summary>
        /// 保存授权客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> SaveSysApiClient([FromBody]SysApiClientInput model)
        {
            var flag = await _sysApiClientAppService.SaveSysApiClient(model);
            return Json(flag);
        }

        /// <summary>
        /// 删除授权客户
        /// </summary>
        /// <param name="id"></param>
        [AbpMvcAuthorize]
        public async Task<JsonResult> DelSysApiClient([FromBody]string id)
        {
            List<string> strList = new List<string>();
            strList.Add(id);
            var flag = await _sysApiClientAppService.DelSysApiClient(strList);
            return Json(flag);
        }

        /// <summary>
        /// 删除授权客户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> DelMultiSysApiClient([FromBody]List<string> ids)
        {
            var flag = await _sysApiClientAppService.DelSysApiClient(ids);
            return Json(flag);
        }
        #endregion

        #region 授权资源
        /// <summary>
        /// 获取授权资源集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> GetSysApiResourceAllList()
        { 
            var list = await _sysApiResourceAppService.GetSysApiResourceList(new SysApiResourceData());
            return Json(list);
        }

        /// <summary>
        /// 获取授权有效的资源集合
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> GetSysApiResourceList()
        {
            SysApiResourceData model = new SysApiResourceData() { IsActive = true };
            var list = await _sysApiResourceAppService.GetSysApiResourceList(model);
            return Json(list);
        }

        /// <summary>
        /// 获取授权资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> GetSysApiResource([FromBody]string id)
        {
            var model = await _sysApiResourceAppService.GetSysApiResource(id);
            return Json(model);
        }

        /// <summary>
        /// 保存授权资源
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> SaveSysApiResource([FromBody]SysApiResourceInput model)
        {
            var flag = await _sysApiResourceAppService.SaveSysApiResource(model);
            return Json(flag);
        }

        /// <summary>
        /// 删除授权资源
        /// </summary>
        /// <param name="id"></param>
        [AbpMvcAuthorize]
        public async Task<JsonResult> DelSysApiResource([FromBody]string id)
        {
            List<string> strList = new List<string>();
            strList.Add(id);
            var flag = await _sysApiResourceAppService.DelSysApiResource(strList);
            return Json(flag);
        }

        /// <summary>
        /// 删除授权资源
        /// </summary>
        /// <param name="ids"></param>
        [AbpMvcAuthorize]
        public async Task<JsonResult> DelMultiSysApiResource([FromBody]List<string> ids)
        {
            var flag = await _sysApiResourceAppService.DelSysApiResource(ids);
            return Json(flag);
        }
        #endregion

        #region 授权 服务 客户 账号 关系

        /// <summary>
        /// 删除授权资源
        /// </summary>
        /// <param name="ids"></param>
        [AbpMvcAuthorize]
        public async Task<JsonResult> GetApiClientByResource([FromBody]string apiResourceId)
        {
            var data = await _sysApiResourceToClientAppService.GetApiClientByResource(apiResourceId);
            return Json(data);
        }

        [AbpMvcAuthorize]
        public async Task<JsonResult> GetApiAccountByClient([FromBody]string apiClientId)
        {
            var data = await _sysApiClienToAccountAppService.GetApiAccountByClient(apiClientId);
            return Json(data);
        }
        #endregion

    }

 
}