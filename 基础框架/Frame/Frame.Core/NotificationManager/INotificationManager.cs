using Abp;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frame.Core
{
    public interface INotificationManager : ITransientDependency
    {

        #region 异步发送通知
        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="data">推送的数据对象(自定义对象需要继承 NotificationData)</param>
        /// <param name="entityIdentifier">实体通知对象(new EntityIdentifier(typeof(对象类型), 对象id))</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="userIds">接收通知的用户id集合 (如果未null将发送到所有人)</param>
        /// <param name="excludedUserIds">不接收通知的用户id集合</param>
        /// <returns></returns>
        Task PublishAsync(string notificationName, NotificationData data = null, EntityIdentifier entityIdentifier = null, NotificationSeverity severity = NotificationSeverity.Info, UserIdentifier[] userIds = null, UserIdentifier[] excludedUserIds = null);

        /// <summary>
        /// 发送通知到所有用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        Task PublishAllAsync(string notificationName, string message, NotificationSeverity severity = NotificationSeverity.Info, string detailed = null);

        /// <summary>
        /// 发送通知到用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="userIdentifier">用户对象集合</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        Task PublishAsync(string notificationName, string message, UserIdentifier[] userIdentifier, NotificationSeverity severity = NotificationSeverity.Info, string detailed = null);

        /// <summary>
        /// 发送通知到用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="userId">用户id集合</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        Task PublishAsync(string notificationName, string message, long[] userId = null, NotificationSeverity severity = NotificationSeverity.Info, string detailed = null);

        /// <summary>
        /// 发送通知到用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="userIdentifier">用户对象</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        Task PublishAsync(string notificationName, string message, UserIdentifier userIdentifier, NotificationSeverity severity = NotificationSeverity.Info, string detailed = null);

        /// <summary>
        /// 发送通知到用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="userId">用户id</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        Task PublishAsync(string notificationName, string message, long userId, NotificationSeverity severity = NotificationSeverity.Info, string detailed = null);

        #endregion

        #region 同步发送通知
        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="data">推送的数据对象(自定义对象需要继承 NotificationData)</param>
        /// <param name="entityIdentifier">实体通知对象(new EntityIdentifier(typeof(对象类型), 对象id))</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="userIds">接收通知的用户id集合 (如果未null将发送到所有人)</param>
        /// <param name="excludedUserIds">不接收通知的用户id集合</param>
        /// <returns></returns>
        void Publish(string notificationName, NotificationData data = null, EntityIdentifier entityIdentifier = null, NotificationSeverity severity = NotificationSeverity.Info, UserIdentifier[] userIds = null);

        /// <summary>
        /// 发送通知到所有用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        void PublishAll(string notificationName, string message, NotificationSeverity severity = NotificationSeverity.Info, string detailed = null);

        /// <summary>
        /// 发送通知到用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="userIdentifier">用户对象集合</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        void Publish(string notificationName, string message, UserIdentifier[] userIdentifier, NotificationSeverity severity = NotificationSeverity.Info, string detailed = null);

        /// <summary>
        /// 发送通知到用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="userId">用户id集合</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        void Publish(string notificationName, string message, long[] userId = null, NotificationSeverity severity = NotificationSeverity.Info, string detailed = null);

        /// <summary>
        /// 发送通知到用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="userIdentifier">用户对象</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        void Publish(string notificationName, string message, UserIdentifier userIdentifier, NotificationSeverity severity = NotificationSeverity.Info, string detailed = null);

        /// <summary>
        /// 发送通知到用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="userId">用户id</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        void Publish(string notificationName, string message, long userId, NotificationSeverity severity = NotificationSeverity.Info, string detailed = null);

        #endregion

        /// <summary>
        /// 发送点对点消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="chatDetailed"></param>
        /// <param name="severity"></param>
        /// <returns></returns>
        Task SendChatAsync(long userId, string chatDetailed, NotificationSeverity severity = NotificationSeverity.Info);

        /// <summary>
        /// 获取用户列表(并且判断是否在线)
        /// </summary>
        /// <returns></returns>
        Task<List<OnlineClientExtension>> GetAllClients();

    }
}
