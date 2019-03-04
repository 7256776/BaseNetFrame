using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using EntityObjectModel;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.Timeout;
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

        public IActionResult PolicyIndex()
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


        public JsonResult PolicySample()
        {
            #region 回退
            // ISyncPolicy policy = Policy.Handle<ArgumentException>()
            //.Fallback(() =>
            //{
            //    Console.WriteLine("Error occured");
            //});

            // policy.Execute(() =>
            // {
            //    //执行业务
            //     throw new ArgumentException("Hello Polly!");
            // });
            #endregion

            #region 重试
            //ISyncPolicy policy1 = Policy.Handle<ArgumentException>().Retry(1);

            //policy1.Execute(() =>
            //{
            //    //执行的业务
            //    string aaa = "";
            //    throw new ArgumentException("Hello Polly!");  
            //});
            #endregion

            #region 重试次数
            //ISyncPolicy policy2 = Policy.Handle<Exception>().CircuitBreaker(3, TimeSpan.FromSeconds(10));
            //while (true)
            //{
            //    try
            //    {
            //        policy2.Execute(() =>
            //        {
            //            throw new Exception("Special error occured");
            //        });
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //    Thread.Sleep(500);
            //}
            #endregion

            #region 超时
            try
            {
                ISyncPolicy policyException = Policy.Handle<TimeoutRejectedException>()
                    .Fallback(() =>
                    {
                        Console.WriteLine("Fallback");
                    });

                //悲观策略 超时就直接异常后再执行后续业务
                ISyncPolicy policyTimeout = Policy.Timeout(3, Polly.Timeout.TimeoutStrategy.Pessimistic);
                //乐观策略 超时就执行后续业务
                //ISyncPolicy policyTimeout = Policy.Timeout(3, Polly.Timeout.TimeoutStrategy.Optimistic);
                ISyncPolicy mainPolicy = Policy.Wrap(policyTimeout, policyException);
                mainPolicy.Execute(() =>
                {
                    Console.WriteLine("Job Start...");
                    Thread.Sleep(5000);
                    //throw new TimeoutRejectedException();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception : {ex.GetType()} : {ex.Message}");
            }

            #endregion

            return Json(true);
        }



    }
}
