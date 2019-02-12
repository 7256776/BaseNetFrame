using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreFrame.Core
{
    public interface ISysMenuActionRepository : IRepository<SysMenuAction, long>
    {
        /// <summary>
        /// 新增动作列表
        /// </summary>
        /// <param name="modelList"></param>
        void AddMenusAction(List<SysMenuAction> modelList);

        /// <summary>
        /// 更新动作列表
        /// </summary>
        /// <param name="modelList"></param>
        /// <param name="menuId"></param>
        void UpdataMenusAction(List<SysMenuAction> modelList, long menuId);

        /// <summary>
        /// 删除模块动作列表
        /// </summary>
        /// <param name="menuId"></param>
        void DelMenusAction(long menuId);

        /// <summary>
        /// 新增角色授权的菜单以及动作
        /// </summary>
        /// <param name="model"></param>
        //int AddRoleToMenuAction(List<SysMenus> model, long roleID);
        int AddRoleToMenuAction(List<SysMenus> model, long roleID);

        /// <summary>
        /// 删除角色授权的菜单以及动作
        /// </summary>
        /// <param name="roleId"></param>
        int DelRoleToMenuAction(long roleId);

        /// <summary>
        /// 获取当前角色授权的菜单以及动作
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IQueryable<SysRoleToMenuAction> GetMenuActionByRole(long roleId);

        /// <summary>
        /// 获取所有模块以及动作请求的授权信息
        /// </summary>
        /// <returns></returns>
        List<MenuActionPermissionCache> GetAllPermissionName();
    }
}
