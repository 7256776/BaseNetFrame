using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace NetCoreFrame.WebApi.Controllers
{
    /// <summary>
    /// NetCore 中创建WebApi注意事项
    ///     1. 需要给类添加标签
    ///         [Route("api/[controller]/[action]")]
    ///         [ApiController]
    ///     2.继承Controller 或 ControllerBase
    /// 
    /// Abp授权的引入注意事项
    ///     1. 由于采用了abp框架因此需要继承NetCoreFrameWebApiControllerBase
    ///     2. 给函数或类添加授权属性标签 [AbpMvcAuthorize], 授权的业务逻辑与MVC中所使用的的属于同一套方案.
    /// ControllerBase //Controller
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestServiceController : NetCoreFrameWebApiControllerBase
    {
        [HttpPost]
        [Authorize]
        public JsonResult ResultPostData()
        {
            return Json("这是Post返回数据");
        }

        [HttpPost]
        [AbpMvcAuthorize("PermissionName.ResultPostData")]
        public JsonResult ResultPermissionPostData()
        {
            return Json("这是Post返回数据");
        }

        [HttpGet]
        [AbpMvcAuthorize]
        public JsonResult ResultGetData()
        {
            return Json("这是Get返回数据");
        }

        [HttpPut]
        public JsonResult ResultPutData()
        {
            return Json("这是Put返回数据");
        }

        [HttpDelete]
        public JsonResult ResultDeleteData()
        {
            return Json("Delete返回数据");
        }

    }
}