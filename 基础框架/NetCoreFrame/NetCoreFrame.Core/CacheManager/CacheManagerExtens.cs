﻿using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using System;
using System.Collections.Generic;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// ITransientDependency
    /// ISingletonDependency
    /// </summary>
    public class CacheManagerExtens : ICacheManagerExtens, ISingletonDependency
    {

        private readonly ICacheManager _cacheManager;
        private readonly ISysRolesRepository _sysRolesRepository;
        private readonly ISysMenuActionRepository _sysMenuActionRepository;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IRepository<SysNotificationInfo, Guid> _sysNotificationInfoRepository;

        public CacheManagerExtens(
            ICacheManager cacheManager,
            ISysRolesRepository sysRolesRepository,
            ISysMenuActionRepository sysMenuActionRepository,
            IRepository<SysNotificationInfo, Guid> sysNotificationInfoRepository,
            IUserInfoRepository userInfoRepository
            )
        {
            _cacheManager = cacheManager;
            _sysRolesRepository = sysRolesRepository;
            _sysMenuActionRepository = sysMenuActionRepository;
            _sysNotificationInfoRepository = sysNotificationInfoRepository;
            _userInfoRepository = userInfoRepository;
        }

        /// <summary>
        /// 指定用户信息
        /// </summary>
        private const string USER_INFO = "USER_INFO";

        /// <summary>
        /// 指定角色的授权
        /// </summary>
        private const string ROLE_PERMISSION = "ROLE_PERMISSION";

        /// <summary>
        /// 所有模块以及动作请求的授权信息
        /// </summary>
        private const string ALL_PERMISSION = "ALL_PERMISSION";

        /// <summary>
        /// 所有通知类型 
        /// </summary>
        private const string ALL_NOTIFICATION = "ALL_NOTIFICATION";

        #region 指定角色的授权

        /// <summary>
        /// 获取指定角色的授权
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public List<RoleToPermissionCache> GetRoleToPermissionCache(long roleId)
        {
            //另一种写法(注释下)
            //ITypedCache<string, List<RoleToPermissionCache>> myCache = _cacheManager.GetCache(ROLE_PERMISSION).AsTyped<string, List<RoleToPermissionCache>>();
            //List<RoleToPermissionCache> roleToPermissionList = myCache.Get(roleId.ToString(), () => _sysRolesRepository.GetRoleToMenuActions(roleId));

            return _cacheManager
                     .GetCache<string, List<RoleToPermissionCache>>(ROLE_PERMISSION)
                     .Get(roleId.ToString(), () =>
                     {
                         return _sysRolesRepository.GetRoleToMenuActions(roleId);
                     });
        }

        /// <summary>
        /// 移除指定角色的授权
        /// </summary>
        /// <param name="roleId"></param>
        public void RemoveRoleToPermissionCache(long roleId)
        {
            _cacheManager
                .GetCache<string, List<RoleToPermissionCache>>(ROLE_PERMISSION)
                .Remove(roleId.ToString());
        }

        #endregion

        #region 所有模块以及动作请求的授权信息

        /// <summary>
        /// 获取所有模块以及动作请求的授权信息
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public List<MenuActionPermissionCache> GetMenuActionPermissionCache()
        {
            return _cacheManager
                     .GetCache<string, List<MenuActionPermissionCache>>(ALL_PERMISSION)
                     .Get(ALL_PERMISSION, () =>
                     {
                         return _sysMenuActionRepository.GetAllPermissionName();
                     });
        }

        /// <summary>
        /// 移除所有模块以及动作请求的授权信息
        /// </summary>
        public void RemoveMenuActionPermissionCache()
        {
            _cacheManager
                .GetCache<string, List<RoleToPermissionCache>>(ALL_PERMISSION)
                .Remove(ALL_PERMISSION);
        }

        #endregion

        #region 指定用户信息

        /// <summary>
        /// 获取指定用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //[UnitOfWork]
        public UserInfo GetUserInfoCache(long userId)
        {
            return _cacheManager
                     .GetCache<string, UserInfo>(USER_INFO)
                     .Get(userId.ToString(), () =>
                     {
                         return _userInfoRepository.GetUserInfo(userId);
                     });
        }

        /// <summary>
        /// 移除指定用户信息
        /// </summary>
        /// <param name="userId"></param>
        public void RemoveUserInfoCache(long userId)
        {
            _cacheManager
                .GetCache<string, UserInfo>(USER_INFO)
                .Remove(userId.ToString());
        }

        public void ClearUserInfoCache()
        {
            _cacheManager.GetCache<string, UserInfo>(USER_INFO).Clear();
        }


        #endregion

        #region 通知类型

        /// <summary>
        /// 获取指定通知类型
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SysNotificationInfo GetNotificationCache(string notificationName)
        {
            return _cacheManager
                     .GetCache<string, SysNotificationInfo>(ALL_NOTIFICATION)
                     .Get(notificationName, () =>
                     {
                         return _sysNotificationInfoRepository.FirstOrDefault(w => w.NotificationName == notificationName);
                     });
        }

        /// <summary>
        /// 移除指定通知类型
        /// </summary>
        /// <param name="userId"></param>
        public void RemoveNotificationCache(string notificationName)
        {
            _cacheManager
                .GetCache<string, SysNotificationInfo>(ALL_NOTIFICATION)
                .Remove(notificationName);
        }

        #endregion
    }
}
