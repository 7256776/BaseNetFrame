using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebIndividual.Controllers
{
    public class PublishingContentController : AbpController
    {
        public IWebFileAppService _webFileAppService;

        public PublishingContentController(IWebFileAppService webFileAppService)
        {
            _webFileAppService = webFileAppService;
        }


        public IActionResult Index()
        {
            return View();
        }
        #region 


        /// <summary>
        /// 查询集合
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize]
        public JsonResult GetDocList()
        {
            List<WebDocDto> data = _webFileAppService.GetDocList();
            return Json(data);
        }

        /// <summary> 
        /// 查询对象       
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize]
        public JsonResult GetDocModel([FromBody] string id)
        {
            WebDocDto data = _webFileAppService.GetDocModel(new Guid(id));
            return Json(data);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize]
        public JsonResult SaveDocModel([FromBody] WebDocDto model)
        {
            _webFileAppService.SaveDocModel(model);
            return Json(true);

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize]
        public JsonResult DelDocModel([FromBody] List<WebDocDto> model)
        {
            _webFileAppService.DelDocModel(model);
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
                result.Result.Id = await _webFileAppService.AddFile(result.Result);
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
            var fileData = _webFileAppService.GetFiles(businessId);
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
            _webFileAppService.DelFile(ids);
            return Task.FromResult(Json(true));
        }
        #endregion

    }
}
