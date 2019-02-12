using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Threading.Tasks;

namespace NetCoreFrame.Sample.Controllers
{
    public class SampleController : NetCoreFrameControllerBase
    {
        private readonly IDataModelAppService _dataModelAppService;
        public SampleController(IDataModelAppService dataModelAppService)
        {
            _dataModelAppService = dataModelAppService;
        }

        #region 页面初始化
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Metadata()
        {
            return View();
        }

        public IActionResult MetadataEdit()
        {
            return View();
        }

        public IActionResult MetadataForm()
        {
            return View();
        }

        public IActionResult MetadataDynamicForm()
        {
            return View();
        }

        #endregion

        #region 基础设置

        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTableList([FromBody]string tabType = "")
        {
            var data = _dataModelAppService.GetTableList(tabType);
            return Json(data);
        }

        /// <summary>
        /// 获取表与字段信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTableAndeFieldList()
        {
            var data = _dataModelAppService.GetTableAndeFieldList();
            foreach (var tab in data)
            {
                foreach (var field in tab.FieldInfoList)
                {
                    field.TableName = field.FieldName;
                    field.TableCode = field.FieldCode;
                    field.IsField = true;
                }
            }
            return Json(data);
        }

        /// <summary>
        /// 或字段对象
        /// 根据字段id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetFieldInfoModel([FromBody]Guid id)
        {
            var data = _dataModelAppService.GetFieldInfoModel(id);
            return Json(data);
        }

        /// <summary>
        /// 获取字段对象
        /// 根据表id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetFieldInfoByTableModel([FromBody]Guid id)
        {
            var data = _dataModelAppService.GetFieldInfoByTableModel(id);
            return Json(data);
        }

        /// <summary>
        /// 保存字段信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<JsonResult> SaveMetaModel([FromBody]dynamic model)
        {
            var jsonStr = JsonConvert.SerializeObject(model);
            var m = JsonConvert.DeserializeObject<FieldInfoDto>(jsonStr);
            m.FieldJson = jsonStr;

            var data = await _dataModelAppService.SaveMetaModel(m);
            return Json(data);
        }

        #endregion

        #region EntityFramework
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<JsonResult> SaveFormModel([FromBody]TempTableDto model)
        {
            var data = await _dataModelAppService.SaveFormModel(model);
            return Json(data);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTempTableDto()
        {
            //IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            //JsonSerializerSettings ssne = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd HH:mm:ss" };
            var data = _dataModelAppService.GetTempTableDto();
            return Json(data);
        }

        #endregion

        #region Dapper
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<JsonResult> SaveFormDapperModel([FromBody]TempTableDto model)
        {
            var data = await _dataModelAppService.SaveFormDapperModel(model);
            return Json(data);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTempTableDapperDto()
        {
            var data = _dataModelAppService.GetTempTableDapperDto();
            return Json(data);
        }

        #endregion

        #region Dapper
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult SaveFormSqlModel([FromBody]dynamic model)
        {
            var data =  _dataModelAppService.SaveFormSqlModel(model);
            return Json(data);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTempTableSqlDto([FromBody]string tableName)
        {
            var data = _dataModelAppService.GetTempTableSqlDto(  tableName);
            return Json(data);
        }

        #endregion
    }
}
