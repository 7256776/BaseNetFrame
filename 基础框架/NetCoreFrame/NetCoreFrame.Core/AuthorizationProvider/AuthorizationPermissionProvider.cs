using Abp.Authorization;
using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 获取所有授权许可
    /// </summary>
    public class AuthorizationPermissionProvider : AuthorizationProvider
    {
        /*
         * 可以实现系统运行中动态添加授权信息
         * 1) 构造函数注入 IPermissionManager 对象 
         * 2) 获取授权对象,方式如下:
         *      Permission currentPermission = permissionManager.GetPermission("需要添加新授权的上级授权对象名称");
         * 3) 获取的currentPermission对象由于这货是引用类型,因此可以这样添加子授权对象,方式如下:
         *      currentPermission.CreateChildPermission("新的授权名称");
         * 4) 还可以通过该方式查询所有授权对象,方式如下:
         *      permissionManager.GetAllPermissions();
        */
        private readonly IRepository<SysMenuAction, long> _sysMenuActionRepository;
        private readonly IRepository<SysMenus, long> _sysMenusRepository;

        public AuthorizationPermissionProvider(
            IRepository<SysMenus, long> sysMenusRepository,
            IRepository<SysMenuAction, long> sysMenuActionRepository
            )
        {
            _sysMenusRepository = sysMenusRepository;
            _sysMenuActionRepository = sysMenuActionRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //获取所有模块授权
            var dataMenus = _sysMenusRepository.GetAllList().Where(w => w.IsActive == true && !string.IsNullOrEmpty(w.PermissionName));
            //获取所有动作请求授权
            var actionList = _sysMenuActionRepository.GetAllList().Where(w => w.IsActive == true);
            //
            Dictionary<string, string> tmpFilter = new Dictionary<string, string>();

            foreach (var item in dataMenus)
            {
                if (IsRepeatAndAdd(tmpFilter, item.PermissionName))
                {
                    continue;
                }
                var root = context.CreatePermission(item.PermissionName.ToLower(), item.MenuDisplayName.ToLocalizable());
                var sysMenuActions = actionList.Where(w => w.MenuID == item.Id).ToList();
                foreach (var action in sysMenuActions)
                {
                    if (string.IsNullOrEmpty(action.PermissionName) || IsRepeatAndAdd(tmpFilter, action.PermissionName))
                    {
                        continue;
                    }
                    root.CreateChildPermission(action.PermissionName.ToLower(), action.ActionDisplayName.ToLocalizable());
                }
            }

            #region 设置示例
            //var administration = context.CreatePermission("Administration.UserManagement");
            //var userManagement = administration.CreateChildPermission("Administration.UserManagement");
            //userManagement.CreateChildPermission("Administration.UserManagement.CreateUser");
            //var roleManagement = administration.CreateChildPermission("Administration.RoleManagement");
            #endregion
        }

        /// <summary>
        ///  判断授权编码是否重复
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsRepeatAndAdd(Dictionary<string, string> dict, string value)
        {
            if (dict.ContainsKey(value))
            {
                return true;
            }
            dict.Add(value, value);
            return false;
        }

    }
}
