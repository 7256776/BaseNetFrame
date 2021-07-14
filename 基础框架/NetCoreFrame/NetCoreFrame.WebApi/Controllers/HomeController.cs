using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.WebApi.Controllers
{
    public class HomeController : NetCoreFrameWebApiControllerBase
    {
        public ActionResult Index()
        {
            return Redirect("swagger");
        }

        public Task<JsonResult> ResultTaskData()
        {
            TestData testData = new TestData()
            {
                TestDate = DateTime.Now,
                TestStr = "这是返回值"
            };
            return Task.FromResult(Json(testData));
        }


        [HttpPost]
        public JsonResult ResultData()
        {
            TestData testData = new TestData()
            {
                TestDate = DateTime.Now,
                TestStr = "这是返回值"
            };
            return Json(testData);
        }


    }

    public class TestData
    {
        public DateTime TestDate { get; set; }
        public string TestStr { get; set; }

    }


}