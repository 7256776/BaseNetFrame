using Abp.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using Newtonsoft.Json;
using System;

namespace NetCoreFrame.Web.Controllers
{
    public class SampleMvcController : AbpController
    {

        public ActionResult Index()
        {
            return View();
        }
        #region 基础应用
        [HttpPost]
        public JsonResult ParamModel([FromBody]SampleModel model)
        {
            return Json(model);
        }

        [HttpPost]
        public JsonResult ParamStr([FromBody]string str)
        {
            return Json(str);
        }

        [HttpPost]
        public JsonResult ParamIntAndStr(string id, [FromBody]string str)
        {
            return Json(id + str);
        }

        [HttpPost]
        public JsonResult ParamStrAndStr(string id, [FromBody]SampleModel str)
        {
            return Json("");
        }

        [HttpPost]
        public JsonResult ParamDynamic([FromBody]dynamic obj)
        {
            var strName = Convert.ToString(obj["name"]) + "--" + Convert.ToString(obj["id"]);

            var oCharging = JsonConvert.DeserializeObject<SampleModel>(obj.ToString());

            return Json(strName);
        }

        [HttpPost]
        public JsonResult ParamGuid([FromBody]Guid obj)
        {
            return Json(obj.ToString());
        }

        [HttpPost]
        public JsonResult ParamArr([FromBody]string[] ids)
        {
            return Json(ids);
        }

        #endregion

        #region 参数对象封装
        [HttpPost]
        public JsonResult ParamModelEx([FromBody]RequestParam<SampleModel> model)
        {
            return Json(model.Params);
        }

        [HttpPost]
        public JsonResult ParamMultiDynamic([FromBody]RequestParam<dynamic> param)
        {
            var data1 = JsonConvert.DeserializeObject<SampleModel>(param.Params.data1.ToString());
            var data2 = JsonConvert.DeserializeObject<SampleModel>(param.Params.data2.ToString());
            return Json(param);
        }

        [HttpPost]
        public JsonResult ParamDynamicEx([FromBody]RequestParam param)
        {
            return Json(param);
        }

        #endregion
    }

    public class SampleModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public bool IsBool { get; set; }

    }

}
