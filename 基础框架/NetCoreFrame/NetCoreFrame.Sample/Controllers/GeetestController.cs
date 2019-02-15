using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCoreFrame.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetCoreFrame.Sample.Controllers
{
    public class GeetestController : NetCoreFrameControllerBase
    {

        private IOptions<GeetestOptions> _geetestOptions;
        private ILoggerFactory _loggerFactory;
       
        public GeetestController(
            IOptions<GeetestOptions> geetestOptions,
            ILoggerFactory loggerFactory)
        {
            _geetestOptions = geetestOptions;
            _loggerFactory = loggerFactory;
        }


        public IActionResult Index()
        {
            return View();
        }
    
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> getCaptcha()
        {
            //GeetestLib geetest = new GeetestLib(_geetestOptions, _loggerFactory);
            GeetestLib geetest = new GeetestLib(_geetestOptions, _loggerFactory);
            string userID = Guid.NewGuid().ToString("N");
            HttpContext.Session.SetString("USERID", userID);
            //验证初始化预处理  判断极验服务器是否宕机 =1正常   =0不正常
            Byte gtServerStatus = await geetest.preProcess(userID, "web");
            //session存储
            HttpContext.Session.SetInt32(GeetestLib.gtServerStatusSessionKey, gtServerStatus);
            //返回授权结果
            var result = geetest.getResponseStr();
            return Json(result);
        }

        /// <summary>
        /// 二次进行验证码验证
        /// </summary>
        /// <param name="geetestChallenge">本次验证会话的唯一标识</param>
        /// <param name="geetestValidate">拖动完成后server端返回的验证结果标识字符串</param>
        /// <param name="geetestSeccode">验证结果的校验码，如果gt-server返回的不与这个值相等则表明验证失败</param>
        /// <returns></returns>
        public async Task<JsonResult> ValidateCaptcha([FromBody]GeetestData model)
        {
            GeetestLib geetest = new GeetestLib(_geetestOptions, _loggerFactory);
            //
            Byte gt_server_status_code = (Byte)HttpContext.Session.GetInt32(GeetestLib.gtServerStatusSessionKey);
            //获取session的用户id
            string userID = HttpContext.Session.GetString("USERID");
            int result = 0;
            if (gt_server_status_code == 1)
            {
                //二次远程api验证
                result = await geetest.enhencedValidateRequest(model.GeetestChallenge, model.GeetestValidate, model.GeetestSeccode, userID);
            }
            else { 
                //服务器挂哒采用了默认的验证,这个验证没有实际意义需要自己添加.
                result = geetest.failbackValidateRequest(model.GeetestChallenge, model.GeetestValidate, model.GeetestSeccode);
            }
            return Json(result == 1 ? "success" : "cuowu");
        }


    }
}
