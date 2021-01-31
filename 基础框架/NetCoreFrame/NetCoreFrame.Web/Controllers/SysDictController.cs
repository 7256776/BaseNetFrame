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
    public class SysDictController : NetCoreFrameControllerBase
    {
        private readonly ISysDictAppService _sysDictAppService;
        private readonly ISysDictTypeAppService _sysDictTypeAppService;
        private readonly ISysDictExtension _sysDictExtension;

        public SysDictController(
            ISysDictAppService sysDictAppService,
            ISysDictTypeAppService sysDictTypeAppService,
            ISysDictExtension sysDictExtension
            )
        {
            _sysDictAppService = sysDictAppService;
            _sysDictTypeAppService = sysDictTypeAppService;
            _sysDictExtension = sysDictExtension;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  查询字典类型列表
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("DictManager")]
        public async Task<JsonResult> GetSysDictTypeList()
        {
            var data = await _sysDictTypeAppService.GetSysDictTypeListAsync();
            return Json(data);
        }

        /// <summary>
        ///  查询字典类型对象
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("DictManager")]
        public async Task<JsonResult> GetSysDictTypeById([FromBody]string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json(false);
            var data = await _sysDictTypeAppService.GetSysDictTypeByIdAsync(Guid.Parse(id));
            return Json(data);
        }

        /// <summary>
        /// 保存字典类型
        /// </summary>
        /// <param name="modelInput"></param>
        /// <param name="listSysDict"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("DictManager.SaveDictType")]
        public async Task<JsonResult> SaveSysDictTypeModel([FromBody]SysDictTypeInput modelInput)
        {
            //保存字典类型值
            var ajaxResponse = await _sysDictTypeAppService.SaveSysDictTypeModel(modelInput);

            return Json(ajaxResponse);
        }

        /// <summary>
        /// 删除字典类型值
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("DictManager.DelDictType")]
        public JsonResult DelSysDictType([FromBody]SysDictTypeInput model)
        {
            if (model.Id == null)
            {
                return Json(false);
            }
            //删除字典类型数据
            _sysDictTypeAppService.DelSysDictType(model);
            return Json(true);
        }

        /// <summary>
        /// 查询当前字典类型的字典值列表
        /// </summary>
        /// <param name="dictType"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("DictManager")]
        public JsonResult GetSysDictListByDictType([FromBody]string dictType)
        {
            var data = _sysDictAppService.GetSysDictListByDictType(dictType);
            return Json(data);
        }

        /// <summary>
        /// 保存字典编码
        /// </summary>
        /// <param name="sysDictInput"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("DictManager.SaveDict")]
        public async Task<JsonResult> SaveSysDictCodeModel([FromBody]List<SysDictInput> sysDictInput)
        {
            //保存字典编码值
            var ajaxResponse = await _sysDictAppService.SaveSysDictModel(sysDictInput);

            return Json(ajaxResponse);
        }

        /// <summary>
        /// 删除字典编码值
        /// </summary>
        /// <param name="listSysDict"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("DictManager.DelDict")]
        public JsonResult DeleteSysDict([FromBody]List<SysDictInput> listSysDict)
        {
            _sysDictAppService.DelSysDict(listSysDict);
            return Json(true);
        }

        /// <summary>
        /// 通过字典类型查询有效的字典值列表
        /// </summary>
        /// <param name="dictType">字典类型</param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public JsonResult GetDictByType([FromBody]string dictType)
        {
            var data = _sysDictExtension.GetDictByType(dictType);
            return Json(data);
        }

        /// <summary>
        /// 查询有效的字典类型列表
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public JsonResult GetDictType()
        {
            var data = _sysDictExtension.GetDictType();
            return Json(data);
        }

    }
}