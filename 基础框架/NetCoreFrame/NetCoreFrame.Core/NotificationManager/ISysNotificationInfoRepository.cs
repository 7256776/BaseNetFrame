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
        /// 获取通知 NotificationName 相关用户订阅的关系
        /// 根据订阅的通知名称查询所有
        /// </summary>
        /// <param name="notificationInfo"></param>
        /// <returns>
        ///      IsSubscription = true 订阅 
        ///      IsSubscription = false 为订阅
        /// </returns>
        Task<List<SysNotificationSubscriptionInfo>> GetSubscriptionByNameAsync(SysNotificationInfo notificationInfo);

        /// <summary>
        /// 获取通知 NotificationName 相关用户订阅的关系
        /// 分页查询
        /// </summary>
        /// <returns>
        ///      IsSubscription = true 订阅 
        ///      IsSubscription = false 为订阅
        /// </returns>
        Task<IQueryable<SysNotificationSubscriptionInfo>> QueryableSubscriptionByNameAsync(SysUserNotificationInfo notificationInfo);

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
        /// 清空所订阅的 notificationName 通知信息
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
