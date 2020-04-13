using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.Web.Controllers
{
    [DisableAuditing]
    public class SysLoginController : NetCoreFrameControllerBase
    {
        private readonly IUserInfoAppService _userInfoAppService;
        private readonly IAccounExtens _accounExtens;

        public SysLoginController(
            IUserInfoAppService userInfoAppService,
            IAccounExtens accounExtens
            )
        {
            _userInfoAppService = userInfoAppService;
            _accounExtens = accounExtens;
        }


        #region 登录验证

        /// <summary>
        /// 登录入口页面
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
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
        //[IgnoreAntiforgeryToken]
        public async Task<JsonResult> LoginRequest([FromBody]LoginUser model)
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

        /// <summary>
        /// 注销用户
        /// </summary>
        /// <returns></returns>      
        [Audited]
        [HttpGet]
        public ActionResult Logout()
        {
            //设置注销前的调用
            _accounExtens.LogoutExtension();

            //
            _userInfoAppService.SignOut();
            //跳转到桌面页面自动判断是否cookic失效
            return RedirectToAction("Index", "SysHome");
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> UpdateUserPass([FromBody]UserPassInput model)
        {
            //修改密码前的接口实现
            var ajaxResponse = await _accounExtens.UpdateUserPassExtension(model);
            //原接口调用的业务
            //var ajaxResponse = await _userInfoAppService.UpdateUserPass(model);
            return Json(ajaxResponse);
        }

        /// <summary>
        ///  设置默认跳转页面
        /// </summary>
        /// <returns></returns>
        private string GetAppHomeUrl()
        {
            //默认首页在js文件有设置因此此处可以不采用该方案,备选方案
            return "/SysHome/Index/#/Views/SysHome/DesktopPage";
            // return Url.Action("Index", "SysHome");
        }

        #endregion





    }
}