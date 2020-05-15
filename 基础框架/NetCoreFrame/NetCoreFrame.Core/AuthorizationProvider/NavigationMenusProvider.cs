using Abp.Application.Navigation;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 获取菜单模块
    /// </summary>
    public class NavigationMenusProvider : NavigationProvider
    {
        /*
        * 可以实现系统运行中动态添加菜单模块信息
        * 1) 构造函数注入 INavigationManager 对象 
        * 2) 然后就可以开始A了,方式如下:
        *  navigationManager.MainMenu.AddItem(
        *   new MenuItemDefinition(
        *       "菜单名称",
        *       new LocalizableString("zjfAbc", "NET"),
        *       url: "",
        *       icon: "图标css",
        *       requiresAuthentication: false,
        *       requiredPermissionName: "依赖授权名称"
        *     )
        *   ); 
        * 3) 还可以通过 构造注入 IUserNavigationManager 查询所有模块对象,该方式会触发授权验证,方式如下:
        *      userNavigationManager.GetMenusAsync();
       */
        private readonly ISysMenusRepository _sysMenusRepository;
        private readonly ISettingManager _settingManager;
        private readonly IRepository<SysMenus, long> _menusRepository;

        public NavigationMenusProvider(
            ISysMenusRepository sysMenusRepository,
            IRepository<SysMenus, long> menusRepository,
            ISettingManager settingManager
            )
        {
            _sysMenusRepository = sysMenusRepository;
            _menusRepository = menusRepository;
            _settingManager = settingManager;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void SetNavigation(INavigationProviderContext context)
        {
            CreateMenuItemDefinition(context.Manager.MainMenu);
        }

        public void CreateMenuItemDefinition(MenuDefinition menuDefinition)
        {
            //获取所有模块
            List<SysMenus> data = _menusRepository.GetAllList().Where(p => p.IsActive == true).ToList();
            //转换模块数据格式,转换成子父节点包含的形式
            data = _sysMenusRepository.ConvertMenusByChildrenList(data);

            foreach (SysMenus item in data)
            {
                MenuItemDefinition menuItemDefinition = new MenuItemDefinition(
                                                name: item.MenuName,
                                                //displayName: item.MenuDisplayName.L(),
                                                displayName: GetMenuLocalizable(item.MenuDisplayName),
                                                icon: item.Icon,
                                                url: item.Url,
                                                requiresAuthentication: item.IsRequiresAuth,
                                                requiredPermissionName: item.PermissionName,
                                                customData: null,
                                                order: Convert.ToInt32(item.OrderBy)
                                                );

                SetChildrenMenusNavigation(menuItemDefinition, item);

                menuDefinition.AddItem(menuItemDefinition);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        private void SetChildrenMenusNavigation(MenuItemDefinition subMenuItemDefinition, SysMenus model)
        {
            foreach (var item in model.ChildrenMenus)
            {
                MenuItemDefinition menuItemDefinition = new MenuItemDefinition(
                                                name: item.MenuName,
                                                //displayName: item.MenuDisplayName.L(),
                                                displayName: GetMenuLocalizable( item.MenuDisplayName),
                                                icon: item.Icon,
                                                url: item.Url,
                                                requiresAuthentication: item.IsRequiresAuth,
                                                requiredPermissionName: item.PermissionName,
                                                customData: null,
                                                order: Convert.ToInt32(item.OrderBy)
                                                );

                subMenuItemDefinition.AddItem(menuItemDefinition);

                SetChildrenMenusNavigation(menuItemDefinition, item);
            }
        }

        /// <summary>
        /// 是否启用多语言菜单
        /// </summary>
        /// <param name="menuDisplayName"></param>
        /// <returns></returns>
        private ILocalizableString GetMenuLocalizable(string menuDisplayName)
        {
            string isMultilingual = _settingManager.GetSettingValueForApplication(ConstantConfig.IsMultilingual);
            if (isMultilingual == "true")
            {
                return menuDisplayName.ToLocalizable();
            }
            return new FixedLocalizableString(menuDisplayName);
        }


    }
}