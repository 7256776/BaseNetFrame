using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppFrame.Models;
using NetCoreFrame.Web;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using Abp.Web.Models;
using Abp.Dependency;
using Abp.Auditing;

namespace AppFrame.Controllers
{
    [DisableAuditing]
    public class AppLoginController : AppFrameControllerBase
    {
        private readonly IUserInfoAppService _userInfoAppService;
        private readonly IAccounExtens _accounExtens;
        private readonly IIocResolver _iocResolver;

        public AppLoginController(
            IUserInfoAppService userInfoAppService,
            IAccounExtens accounExtens,
            IIocResolver iocResolver
            )
        {
            _userInfoAppService = userInfoAppService;
            _accounExtens = accounExtens;

            _iocResolver = iocResolver;

        }


        public ActionResult Login(string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = GetAppHomeUrl();
            }
            return View(new LoginFormViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        /// <summary>
        ///  登陆验证
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> LoginRequest([FromBody] LoginUserInput model)
        {
            if (string.IsNullOrWhiteSpace(model.ReturnUrl))
            {
                model.ReturnUrl = GetAppHomeUrl();
            }

            //登录验证
            //SysLoginResult<UserInfo> result = await _userInfoAppService.LoginAuth(model);
            //调用接口实现的登录验证
            SysLoginResult<UserInfo> result = await _accounExtens.LoginRequest(model);

            //注册用户信息
            await _userInfoAppService.SetAuthenticationProperties(model, result);
            //
            return Json(new AjaxResponse() { TargetUrl = model.ReturnUrl });
        }

        private string GetAppHomeUrl()
        {
            return "/#/Views/AppHome/Home";
        }

    }
}
