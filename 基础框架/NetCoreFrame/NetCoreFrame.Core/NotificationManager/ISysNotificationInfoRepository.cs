using Abp.Domain.Repositories;
using Abp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 通知基础信息仓储
    /// </summary>
    public interface ISysNotificationInfoRepository : IRepository<SysNotificationInfo, Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationInfo"></param>
        /// <returns></returns>
        Task AddAllUserSubscriptionAsync(SysNotificationInfo notificationInfo);

        /// <summary>
        /// 变更订阅了通知(NotificationName)的用户到新的通知类型名称
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<SysNotificationInfo> UpdateAllUserSubscriptionAsync(SysNotificationInfo model);

        /// <summary>
        /// 验证通知类型名称(NotificationName)是否重复
        /// </summary>
        /// <param name="notificationInfo"></param>
        /// <returns></returns>
        Task<bool> IsSubscriptionRepeat(SysNotificationInfo notificationInfo);

        /// <summary>
        /// 查询订阅了该通知的用户
        /// </summary>
        /// <param name="notificationInfo"></param>
        /// <returns></returns>
        Task<List<SysNotificationSubscriptionInfo>> GetSubscriptionByNameAsync(SysNotificationInfo notificationInfo);

        /// <summary>
        /// 删除通知基础信息以及该通知的订阅
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task DeleteNotificationAndSubscriptionAsync(SysNotificationInfo model);

        /// <summary>
        /// 获取用户订阅的通知
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<SysNotificationInfo>> GetUserSubscriptionAsync(long userId);

        /// <summary>
        /// 获取用户订阅的通知消息信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagingDto"></param>
        /// <returns></returns>
        Task<IQueryable<SysUserNotificationInfo>> GetUserNotificationsAsync(SysUserNotificationInfo model);

        /// <summary>
        /// 清空消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notificationName"></param>
        /// <returns></returns>
        Task CleanUserNotificationByName(long userId, string notificationName);

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notificationName"></param>
        /// <returns></returns>
        Task DelUserNotificationAsync(List<Guid> idList);

        /// <summary>
        /// 修改消息读取状态
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        Task UpdateUserNotificationStatus(List<UserNotificationInfoDto> list);

    }
}
