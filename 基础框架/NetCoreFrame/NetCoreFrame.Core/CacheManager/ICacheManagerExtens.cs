using System.Collections.Generic;

namespace NetCoreFrame.Core
{
    public interface ICacheManagerExtens
    {
        /// <summary>
        /// 获取指定角色的授权
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<RoleToPermissionCache> GetRoleToPermissionCache(long roleId);

        List<RoleToPermissionCache> GetRoleToPermissionCache1(long roleId, List<RoleToPermissionCache> list);

        /// <summary>
        /// 移除指定角色的授权
        /// </summary>
        /// <param name="roleId"></param>
        void RemoveRoleToPermissionCache(long roleId);

        /// <summary>
        /// 获取所有模块以及动作请求的授权信息
        /// </summary>
        /// <returns></returns>
        List<MenuActionPermissionCache> GetMenuActionPermissionCache();

        /// <summary>
        /// 移除所有模块以及动作请求的授权信息
        /// </summary>
        void RemoveMenuActionPermissionCache();

        /// <summary>
        /// 获取指定用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        UserInfo GetUserInfoCache(long userId);

        /// <summary>
        /// 移除指定用户信息
        /// </summary>
        /// <param name="userId"></param>
        void RemoveUserInfoCache(long userId);

        void ClearUserInfoCache();

        /// <summary>
        /// 获取指定通知类型
        /// </summary>
        /// <param name="notificationName"></param>
        /// <returns></returns>
        SysNotificationInfo GetNotificationCache(string notificationName);

        /// <summary>
        /// 移除指定通知类型
        /// </summary>
        /// <param name="notificationName"></param>
        void RemoveNotificationCache(string notificationName);
    }
}
