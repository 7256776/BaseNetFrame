using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppFrame.Models;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.Application.Navigation;
using Abp;
using AppFrame.Core;
using AppFrame.Application;

namespace AppFrame.Controllers
{
    /// <summary>
    /// 首页面
    /// 添加授权验证,用于检验未登录自动跳转到登录页
    /// </summary>
    [AbpMvcAuthorize]
    [DisableAuditing]
    public class AppBaseConfigController : AppFrameControllerBase
    {
        private readonly IAppBuildProjectCategoryAppService _appBuildProjectCategoryAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appBuildProjectCategoryAppService"></param>
        public AppBaseConfigController(
            IAppBuildProjectCategoryAppService appBuildProjectCategoryAppService
            )
        {
            _appBuildProjectCategoryAppService = appBuildProjectCategoryAppService;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult PointerTypeConfig()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult PointerTypeInput()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Commodity()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult CommodityInput()
        {
            return View();
        }

        #region 指标配置

        /// <summary>
        /// 指标项目明细集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetBuildProjectCategoryConfigList([FromBody] AppBuildProjectCategoryConfigData model)
        {
            var list = await _appBuildProjectCategoryAppService.GetBuildProjectCategoryConfigList(model);
            return Json(list);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetBuildProjectCategoryConfig([FromBody] string id)
        {
            var model = await _appBuildProjectCategoryAppService.GetBuildProjectCategoryConfig(id);
            return Json(model);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<JsonResult> SaveBuildProjectCategoryConfig([FromBody] AppBuildProjectCategoryConfigInput model)
        {
            await _appBuildProjectCategoryAppService.SaveBuildProjectCategoryConfig(model);
            return Json(true);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        public Task<JsonResult> DelBuildProjectCategoryConfig([FromBody] List<string> ids)
        {
            _appBuildProjectCategoryAppService.DelBuildProjectCategoryConfig(ids);
            return Task.FromResult(Json(true));
        }

        #endregion

        #region 商品

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<JsonResult> SaveCommodity([FromBody] AppCommodityInput model)
        {
            Guid id = await _appBuildProjectCategoryAppService.SaveCommodity(model);
            return Json(id);
        }

        /// <summary>
        /// 商品集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetCommodityList([FromBody] AppCommodityData model)
        {
            var data = await _appBuildProjectCategoryAppService.GetCommodityList(model);
            return Json(data);

        }

        /// <summary>
        /// 商品对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetCommodity([FromBody] string id)
        {
            var data = await _appBuildProjectCategoryAppService.GetCommodity(new Guid(id));
            return Json(data);

        }

        /// <summary>
        /// 商品对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<JsonResult> DelCommodity([FromBody] string id)
        {
            _appBuildProjectCategoryAppService.DelCommodity(id);
            return Task.FromResult(Json(true));
        }
        #endregion

    }
}
