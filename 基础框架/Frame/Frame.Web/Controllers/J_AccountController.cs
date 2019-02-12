using Abp.Auditing;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Security.AntiForgery;
using Frame.Application;
using Frame.Core;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Frame.Web
{
    [DisableAuditing]
    public class J_AccountController : FrameExtAbpController
    {

        private readonly IUserInfoAppService _userInfoAppService;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly ILoginExtens _loginExtens;
        private readonly ILogoutExtens _logoutExtens;


        public J_AccountController(
            IUserInfoAppService userInfoAppService,
            IAuthenticationManager authenticationManager,
            ILoginExtens loginExtens,
            ILogoutExtens logoutExtens
            )
        {
            _userInfoAppService = userInfoAppService;
            _authenticationManager = authenticationManager;
            _loginExtens = loginExtens;
            _logoutExtens = logoutExtens;
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
                returnUrl = Request.ApplicationPath;
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
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [DisableAbpAntiForgeryTokenValidation]
        public async Task<JsonResult> LoginRequest(LoginUser model, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }
            else
            {
                returnUrl = "/J_Home/Index/#/Views/J_Home/DesktopPage";
            }
            //登录验证
            SysLoginResult<UserInfo> result = await _loginExtens.LoginRequest(model, returnUrl);  

            //注册用户信息
            _userInfoAppService.SetAuthenticationProperties(model, result);
            //
            return Json(new AjaxResponse() { TargetUrl = returnUrl });
        }

        /// <summary>
        /// 注销用户
        /// </summary>
        /// <returns></returns>      
        [Audited]
        public ActionResult Logout()
        {
            //设置注销前的调用
            _logoutExtens.LogoutExtension();

            _authenticationManager.SignOut();
            //return View();
            //跳转到桌面页面自动判断是否cookic失效
            return RedirectToAction("Index", "J_Home");
        }

        /// <summary>
        /// 获取登录用户所有授权模块以及动作
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public JsonResult GetUserPermission()
        {
            var data = _userInfoAppService.GetUserPermission();
            return Json(data);
        }
        #endregion

        #region 用户管理
        [AbpMvcAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询用户集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("UserInfoManager")]
        public JsonResult GetUserList(UserQuery model, PagingDto pagingDto)
        {
            var data = _userInfoAppService.GetUserList(model, pagingDto);
            return Json(data);
        }

        /// <summary>
        /// 获取用户对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public JsonResult GetUserModel(long id)
        {
            var data = _userInfoAppService.GetUserModel(id);
            return Json(data);
        }

        /// <summary>
        /// 保存用户对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("UserInfoManager.SaveUser")]
        public async Task<JsonResult> SaveUserModel(UserInput model)
        {
            var ajaxResponse = await _userInfoAppService.SaveUserModel(model);
            return Json(ajaxResponse);
        }

        /// <summary>
        /// 删除用户对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("UserInfoManager.DelUser")]
        public async Task<JsonResult> DelUserModel(List<UserInput> model)
        {
            await _userInfoAppService.DelUserModel(model);
            return Json(true);
        }

        #endregion

        #region 用户设置
        [AbpMvcAuthorize]
        public ActionResult UserSettings()
        {
            return View();
        }

        /// <summary>
        /// 修改用户基础信息
        /// </summary> 
        [AbpMvcAuthorize]
        public async Task<JsonResult> SeetingUserInfo(UserInfoInput model)
        {
            var ajaxResponse = await _userInfoAppService.SeetingUserInfo(model);
            return Json(ajaxResponse);
            //return Json(true);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> UpdateUserPass(UserPassInput model)
        {
            var ajaxResponse = await _userInfoAppService.UpdateUserPass(model);
            return Json(ajaxResponse);
        }

        /// <summary>
        /// 设置用户图像
        /// </summary>
        /// <param name="imgId"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> SaveAvatars(string imgId)
        {
            var ajaxResponse = await _userInfoAppService.SaveAvatars(imgId);
            return Json(ajaxResponse);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("UserInfoManager.ResetPass")]
        public async Task<JsonResult> ResetUserPass(long id)
        {
            var ajaxResponse = await _userInfoAppService.ResetUserPass(id);
            return Json(ajaxResponse);
        }
        #endregion




    }
}