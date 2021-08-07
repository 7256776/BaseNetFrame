using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppFrame.Models;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.Application.Navigation;
using Abp;
using Abp.Web.Models;

namespace AppFrame.Controllers
{
    /// <summary>
    /// 首页面
    /// 添加授权验证,用于检验未登录自动跳转到登录页
    /// </summary>
    [AbpMvcAuthorize]
    [DisableAuditing]
    public class AppHomeController : AppFrameControllerBase
    {
        private readonly IUserNavigationManager _userNavigationManager;


        public AppHomeController(
            IUserNavigationManager userNavigationManager
            )
        {
            _userNavigationManager = userNavigationManager;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Home()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        //[DisableAbpAntiForgeryTokenValidation]
        public async Task<JsonResult> MenuList()
        {
            UserMenu mainMenu = await _userNavigationManager.GetMenuAsync("MainMenu", new UserIdentifier(null, AbpSession.UserId.Value));

            //获取授权模块的另一种实现(备用方案获取授权模块)
            //mainMenu.Items = await this.BuildPermissionMenu(_navigationManager.MainMenu.Items);

            return Json(mainMenu);
        }

    }
}
