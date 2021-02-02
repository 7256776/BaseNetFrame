using Abp;
using Abp.Application.Navigation;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.Configuration;
using Abp.Localization;
using Abp.Threading;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.Web.Controllers
{
    [AbpMvcAuthorize]
    [DisableAuditing]
    public class SysHomeController : NetCoreFrameControllerBase
    {
        private readonly INavigationManager _navigationManager;
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly ISettingManager _settingManager;
        private readonly ILocalizationContext _localizationContext;
        private readonly ILanguageManager _languageManager;

        private readonly Abp.Authorization.IPermissionChecker _permissionChecker;

        private readonly UserInfoManager _userInfoManager;


        public SysHomeController(
            IUserNavigationManager userNavigationManager,
            ISettingManager settingManager,
            INavigationManager navigationManager,
            ILocalizationContext localizationContext,
            ILanguageManager languageManager,
             Abp.Authorization.IPermissionChecker permissionChecker,
        UserInfoManager userInfoManager
            )
        {
            _permissionChecker = permissionChecker;

            _localizationContext = localizationContext;
            _languageManager = languageManager;

            _navigationManager = navigationManager;
            _userInfoManager = userInfoManager;
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
        public async Task<JsonResult> MenuList()
        {
            UserMenu mainMenu = await _userNavigationManager.GetMenuAsync("MainMenu", new UserIdentifier(null, AbpSession.UserId.Value));

            //获取授权模块的另一种实现(备用方案获取授权模块)
            //mainMenu.Items = await this.BuildPermissionMenu(_navigationManager.MainMenu.Items);

            return Json(mainMenu);
        }

        /// <summary>
        /// 验证授权模块
        /// 备用方案获取授权模块
        /// </summary>
        /// <param name="menuItemDefinitions"></param>
        /// <returns></returns>
        //public async Task<List<UserMenuItem>> BuildPermissionMenu(List<MenuItemDefinition> menuItemDefinitions)
        //{
        //    List<UserMenuItem> userMenuItems = new List<UserMenuItem>();
        //    foreach (var menuItem in menuItemDefinitions)
        //    {
        //        //递归验证子模块授权
        //        var nextUserMenuItems = await this.BuildPermissionMenu(menuItem.Items);
        //        bool isPermission = await _userInfoManager.IsGrantedAsync(menuItem.RequiredPermissionName);
        //        if (!isPermission)
        //        {
        //            continue;
        //        }
        //        userMenuItems.Add(new UserMenuItem()
        //        {
        //            Name = menuItem.Name,
        //            Icon = menuItem.Icon,
        //            DisplayName = menuItem.DisplayName.Localize(_localizationContext),
        //            Order = menuItem.Order,
        //            Url = menuItem.Url,
        //            CustomData = menuItem.CustomData,
        //            Target = menuItem.Target,
        //            IsEnabled = menuItem.IsEnabled,
        //            IsVisible = menuItem.IsVisible,
        //            Items = nextUserMenuItems
        //        });
        //    }
        //    return userMenuItems;
        //}




    }
}