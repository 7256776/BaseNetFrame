using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Runtime.Session;
using System.Collections.Generic;

namespace NetCoreFrame.Core
{

    public class NavigationMenusExt : ITransientDependency
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly NavigationMenusProvider _navigationMenusProvider;
        private readonly INavigationManager _navigationManager;
        private readonly IPermissionManager _permissionManager;

        public IAbpSession AbpSession { get; set; }

        public NavigationMenusExt(
            NavigationMenusProvider navigationMenusProvider,
            INavigationManager navigationManager,
            IPermissionManager permissionManager,
            IUserNavigationManager userNavigationManager
        )
        {
            _navigationMenusProvider = navigationMenusProvider;
            _navigationManager = navigationManager;
            _permissionManager = permissionManager;
            _userNavigationManager = userNavigationManager;

            AbpSession = NullAbpSession.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void UpNavigationMenusProvider(SysMenus model = null)
        {
            #region 更新全局模块列表(主要解决运行时模块列表没有更新的情况)
            _navigationManager.MainMenu.Items.Clear();
            _navigationMenusProvider.CreateMenuItemDefinition(_navigationManager.MainMenu);
            #endregion
            //新增,编辑 更新基础模块的同时添加新加的授权信息
            //删除 此处为null直接更新原有菜单信息
            if (model == null)
                return;
            #region  更新全局授权列表(主要解决运行时授权列表没有更新的情况)
            //添加授权模块
            Permission currentPermission = _permissionManager.GetPermissionOrNull(model.PermissionName.ToLower());
            if (currentPermission == null)
            {
                IReadOnlyList<Permission> permissionList = _permissionManager.GetAllPermissions();
                currentPermission = permissionList[0].CreateChildPermission(model.PermissionName.ToLower(), model.MenuDisplayName.ToLocalizable());
            }
            //添加授权动作
            foreach (var action in model.SysMenuActions)
            {
                if (!string.IsNullOrEmpty(action.PermissionName) && _permissionManager.GetPermissionOrNull(action.PermissionName.ToLower()) == null)
                {
                    currentPermission.CreateChildPermission(action.PermissionName.ToLower(), action.ActionDisplayName.ToLocalizable());
                }
            }

            #endregion
        }


    }
}
