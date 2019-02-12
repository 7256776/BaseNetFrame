using Abp.Application.Navigation;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Localization;
using Frame.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frame.Sample
{
    /// <summary>
    /// 获取菜单模块
    /// </summary>
    public class SampleNavigationMenusProvider : NavigationProvider
    { 

        public SampleNavigationMenusProvider()
        {
        }


        private List<SysMenus> SampleMenus
        {
            get
            {
                #region 示例模块菜单
                //设置主键id
                int menuID_1 = 7788414;
                //主菜单
                List<SysMenus> menus = new List<SysMenus>();
                //子菜单(布局页面)
                List<SysMenus> menusSub = new List<SysMenus>();
                menus.Add(new SysMenus
                {
                    Id = menuID_1,
                    MenuDisplayName = "布局示例",
                    MenuName = "j-layoutsample",
                    PermissionName = "LayoutSample",
                    RequiresAuthModel = "1",
                    Url = "",
                    Icon = "fa-list-ol",
                    OrderBy = 99,
                    IsActive = true,
                    ChildrenMenus = menusSub
                });

                menusSub.Add(new SysMenus
                {
                    ParentID = menuID_1,
                    MenuDisplayName = "布局页面1",
                    MenuName = "j-layoutsample1",
                    PermissionName = "LayoutSample1",
                    RequiresAuthModel = "1",
                    Url = "/Areas/LayoutSample/Views/LayoutSample/Layout1",
                    Icon = "fa-list-ol",
                    OrderBy = 1,
                    IsActive = true,
                    ChildrenMenus = new List<SysMenus>()
                });
                menusSub.Add(new SysMenus
                {
                    ParentID = menuID_1,
                    MenuDisplayName = "布局页面2",
                    MenuName = "j-layoutsample2",
                    PermissionName = "LayoutSample2",
                    RequiresAuthModel = "1",
                    Url = "/Areas/LayoutSample/Views/LayoutSample/Layout2",
                    Icon = "fa-list-ol",
                    OrderBy = 2,
                    IsActive = true,
                    ChildrenMenus = new List<SysMenus>()
                });
                menusSub.Add(new SysMenus
                {
                    ParentID = menuID_1,
                    MenuDisplayName = "布局页面3",
                    MenuName = "j-layoutsample3",
                    PermissionName = "LayoutSample3",
                    RequiresAuthModel = "1",
                    Url = "/Areas/LayoutSample/Views/LayoutSample/Layout3",
                    Icon = "fa-list-ol",
                    OrderBy = 3,
                    IsActive = true,
                    ChildrenMenus = new List<SysMenus>()
                });

                //子菜单(MongoDB示例)
                List<SysMenus> menusSub1 = new List<SysMenus>();
                menus.Add(new SysMenus
                {
                    Id = menuID_1,
                    MenuDisplayName = "MongoDB",
                    MenuName = "j-mongodbsample",
                    PermissionName = "MongoDBSample",
                    RequiresAuthModel = "1",
                    Url = "",
                    Icon = "fa-list-ol",
                    OrderBy = 99,
                    IsActive = true,
                    ChildrenMenus = menusSub1
                });

                menusSub1.Add(new SysMenus
                {
                    ParentID = menuID_1,
                    MenuDisplayName = "MongoDB(实体对象)",
                    MenuName = "j-mongodb",
                    PermissionName = "MongoDBSample",
                    RequiresAuthModel = "1",
                    Url = "/Areas/MongoDB/Views/MongoDBSample/index",
                    Icon = "fa-list-ol",
                    OrderBy = 1,
                    IsActive = true,
                    ChildrenMenus = new List<SysMenus>()
                });

                menusSub1.Add(new SysMenus
                {
                    ParentID = menuID_1,
                    MenuDisplayName = "MongoDB(自定义对象)",
                    MenuName = "j-objectmongodb",
                    PermissionName = "MongoDBSample",
                    RequiresAuthModel = "1",
                    Url = "/Areas/MongoDB/Views/MongoDBSample/IndexObject",
                    Icon = "fa-list-ol",
                    OrderBy = 1,
                    IsActive = true,
                    ChildrenMenus = new List<SysMenus>()
                });
                #endregion

                return menus;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void SetNavigation(INavigationProviderContext context)
        {
            foreach (SysMenus item in SampleMenus)
            {
                MenuItemDefinition menuItemDefinition = new MenuItemDefinition(
                                                  name: item.MenuName,
                                                  displayName: new FixedLocalizableString(item.MenuDisplayName),
                                                  icon: item.Icon,
                                                  url: item.Url,
                                                  requiresAuthentication: item.IsRequiresAuth,
                                                  requiredPermissionName: item.PermissionName,
                                                  customData: null,
                                                  order: Convert.ToInt32(item.OrderBy)
                                                  );

                SetChildrenMenusNavigation(menuItemDefinition, item);

                context.Manager.MainMenu.AddItem(menuItemDefinition);
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
                                                displayName: new FixedLocalizableString(item.MenuDisplayName),
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


    }
}