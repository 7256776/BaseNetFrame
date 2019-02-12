﻿using Abp;
using Abp.Application.Navigation;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.Configuration;
using Abp.Threading;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.Web.Controllers
{
    [AbpMvcAuthorize]
    [DisableAuditing]
    public class J_HomeController : NetCoreFrameControllerBase
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly ISettingManager _settingManager;

        public J_HomeController(
            IUserNavigationManager userNavigationManager,
             ISettingManager settingManager
            )
        {
            _userNavigationManager = userNavigationManager;
            _settingManager = settingManager;
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
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult DesktopPage()
        {
            return View();
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        //[DisableAbpAntiForgeryTokenValidation]
        public JsonResult MenuList()
        {
            //这货是异步的必须采用该方案获取返回值
            UserMenu MainMenu = AsyncHelper.RunSync(() => _userNavigationManager.GetMenuAsync("MainMenu", new UserIdentifier(null, AbpSession.UserId.Value)));
            return Json(MainMenu);
        }

      


    }
}