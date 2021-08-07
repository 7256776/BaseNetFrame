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
using Microsoft.AspNetCore.Http;
using System.IO;
using Abp.Web.Models;
using AppFrame.Models.Communal;

namespace AppFrame.Controllers
{
    /// <summary>
    /// 首页面
    /// 添加授权验证,用于检验未登录自动跳转到登录页
    /// </summary>
    [AbpMvcAuthorize]
    [DisableAuditing]
    public class AppBusinessController : AppFrameControllerBase
    {
        private readonly IAppBuildProjectCategoryAppService _appBuildProjectCategoryAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appBuildProjectCategoryAppService"></param>
        public AppBusinessController(
            IAppBuildProjectCategoryAppService appBuildProjectCategoryAppService
            )
        {
            _appBuildProjectCategoryAppService = appBuildProjectCategoryAppService;

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
        public ActionResult Solution()
        {
            return View();
        }        
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult SolutionInput()
        {
            return View();
        }


        #region 方案
        /// <summary>
        /// 方案集合,以及明细汇总
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> GetSolutionList([FromBody] AppSolutionData model)
        {
            var list = await _appBuildProjectCategoryAppService.GetSolutionList(model);
            return Json(list);
        }

        /// <summary>
        /// 方案对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult GetSolution([FromBody] string id)
        {
            var model = _appBuildProjectCategoryAppService.GetSolution(id);
            return Json(model);
        }

        /// <summary>
        /// 新增方案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<JsonResult> AddSolutionAndBuildProjectCategory([FromBody] AppSolutionInput model)
        {
            var id = await _appBuildProjectCategoryAppService.AddSolutionAndBuildProjectCategory(model);
            return Json(id);
        }

        /// <summary>
        /// 编辑方案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<JsonResult> EditSolution([FromBody] AppSolutionInput model)
        {
            var id = await _appBuildProjectCategoryAppService.EditSolution(model);
            return Json(id);
        }

        /// <summary>
        /// 删除授权账号
        /// </summary>
        /// <param name="ids"></param>
        public Task<JsonResult> DelSolution([FromBody] List<string> ids)
        {
            _appBuildProjectCategoryAppService.DelSolution(ids);
            return Task.FromResult(Json(true));
        }

        #endregion

        #region 指标

        /// <summary>
        /// 指标项目明细集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult GetBuildProjectCategoryDetail([FromBody] AppBuildProjectCategoryData model)
        {
            var list = _appBuildProjectCategoryAppService.GetBuildProjectCategoryDetail(model);
            return Json(list);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetBuildProjectCategory([FromBody] string id)
        {
            var model = await _appBuildProjectCategoryAppService.GetBuildProjectCategory(id);
            return Json(model);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<JsonResult> SaveBuildProjectCategory([FromBody] AppBuildProjectCategoryInput model)
        {
            await _appBuildProjectCategoryAppService.SaveBuildProjectCategory(model);
            return Json(true);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        public Task<JsonResult> DelBuildProjectCategory([FromBody] List<string> ids)
        {
            _appBuildProjectCategoryAppService.DelBuildProjectCategory(ids);
            return Task.FromResult(Json(true));
        }

        /// <summary>
        /// 设置启用状态
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<JsonResult> SetBuildProjectCategoryActive([FromBody] List<AppSolutionData> list)
        {
            await _appBuildProjectCategoryAppService.SetBuildProjectCategoryActive(list);
            return Json(true);
        }
        #endregion

        #region 文件管理
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> AppFileUpload([FromForm(Name = "files")] IFormFile formFile)
        {
            var result = FileUpdata.AppUpload(formFile);
            if (result.Success)
            {
                result.Result.Id = await _appBuildProjectCategoryAppService.AddAppFile(result.Result);
            }
            return Json(result);
        }

        /// <summary>
        /// 查询文件集合
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetFiles([FromBody] string businessId)
        {
            var fileData = _appBuildProjectCategoryAppService.GetFiles(businessId);
            return Json(fileData);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<JsonResult> DelFile([FromBody] List<string> ids)
        {
            _appBuildProjectCategoryAppService.DelAppFile(ids);
            return Task.FromResult(Json(true));
        }
        #endregion
         

    }
}
