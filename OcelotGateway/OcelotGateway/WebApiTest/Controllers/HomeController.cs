using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EntityObjectModel;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Models;

namespace WebApiTest.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 通过后台请求获取结果
        /// </summary>
        /// <returns></returns>
        public JsonResult DoApi([FromBody]HttpClientSetting httpModel)
        {
            HttpClientHubBase httpClientHubBase = new HttpClientHubBase();

            httpClientHubBase._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", httpModel.Token);

            if (httpModel.ActionType.ToUpper() == "GET")
            {
                //var data = httpClientHubBase.GetData<UserInfo>(httpModel.Url);
                try
                {
                    var data = httpClientHubBase.GetData<dynamic>(httpModel.Url);
                    return Json(data);
                }
                catch (Exception ex)
                {

              
                }
               
            }
            else if (httpModel.ActionType.ToUpper() == "POST")
            {
                var data = httpClientHubBase.PostData<dynamic>(httpModel.Url, httpModel.ParamData);
                return Json(data);
            }

            return Json(true);
        }

      

    }
}
