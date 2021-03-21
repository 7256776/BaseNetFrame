using Abp;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Notifications;
using Abp.RealTime;
using Abp.Threading;
using Castle.Core.Logging;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Application
{
    public class NotificationManager : INotificationManager
    {
        public ILogger Logger { get; set; }

        private readonly ICacheManagerExtens _cacheManagerExtens;

        //发布通知
        private readonly INotificationPublisher _notificationPublisher;
      
        //管理持久化的通知(调用的是通知持久化对象NotificationStore)
        //订阅的通知信息
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;

        //管理启动类设置的通知(调用的是启动类配置的通知,并且结合系统统一的权限验证来判断是否拥有该通知的权限)
        //private readonly INotificationDefinitionManager _notificationDefinitionManager;

        //接收或发送的通知信息的 管理持久化的通知(调用的是通知持久化对象NotificationStore)
        //private readonly IUserNotificationManager _userNotificationManager;

        //管理在线用户
        private readonly IOnlineClientManager _onlineClientManager;

        //signalR推送接口
        private readonly IRealTimeNotifier _realTimeNotifier;

        private readonly IAbpSessionExtens AbpSessionExtens;

        private readonly IRepository<UserInfo, long> _userInfoRepository;

        public NotificationManager(
            INotificationPublisher notificationPublisher,
            INotificationSubscriptionManager notificationSubscriptionManager,
            ICacheManagerExtens cacheManagerExtens,
            IRealTimeNotifier realTimeNotifier,
            IAbpSessionExtens abpSessionExtens,
            IOnlineClientManager onlineClientManager,
            IRepository<UserInfo, long> userInfoRepository
            )
        {
            Logger = NullLogger.Instance;
            _notificationPublisher = notificationPublisher;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _cacheManagerExtens = cacheManagerExtens;
            _realTimeNotifier = realTimeNotifier;
            AbpSessionExtens = abpSessionExtens;
            _onlineClientManager = onlineClientManager;
            _userInfoRepository = userInfoRepository;
        }

        /// <summary>
        /// 初始发送的数据对象
        /// </summary>
        /// <param name="notificationName"></param>
        /// <param name="message"></param>
        /// <param name="detailed"></param>
        /// <returns></returns>
        private FrameNotificationData InitNotificationData(string notificationName, string message, string detailed)
        {
            var notification = _cacheManagerExtens.GetNotificationCache(notificationName);
            //设置通知内容以及详细信息
            FrameNotificationData frameNotificationData = new FrameNotificationData(message);
            frameNotificationData.NotificationType = notification.NotificationType;
            frameNotificationData.Title = notification.NotificationDisplayName;
            frameNotificationData.NotificationDetailed = detailed;
            frameNotificationData.SendId = AbpSessionExtens.UserId.Value;
            return frameNotificationData;
        }

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
        public virtual async Task PublishAsync(string notificationName, NotificationData data = null, EntityIdentifier entityIdentifier = null, NotificationSeverity severity = NotificationSeverity.Info, UserIdentifier[] userIds = null, UserIdentifier[] excludedUserIds = null)
        {
            if (userIds != null)
            {
                //获取订阅了该通知的用户
                userIds = userIds.Where(w => _notificationSubscriptionManager.IsSubscribed(w, notificationName)).ToArray();
            }
            await _notificationPublisher.PublishAsync(notificationName, data, entityIdentifier, severity, userIds, excludedUserIds);
        }

        /// <summary>
        /// 发送通知到所有用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        public virtual async Task PublishAllAsync(string notificationName, string message, NotificationSeverity severity = NotificationSeverity.Info, string detailed = null)
        {
            FrameNotificationData frameNotificationData = InitNotificationData(notificationName, message, detailed);
            await PublishAsync(notificationName, frameNotificationData, severity: severity);
        }

        /// <summary>
        /// 发送通知到用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="userIdentifier">用户对象集合</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        public virtual async Task PublishAsync(string notificationName, string message, UserIdentifier[] userIdentifier , NotificationSeverity severity = NotificationSeverity.Info, string detailed = null)
        {
            FrameNotificationData frameNotificationData = InitNotificationData(  notificationName, message, detailed);
            await PublishAsync(notificationName, frameNotificationData, severity: severity, userIds: userIdentifier);
        }

        /// <summary>
        /// 发送通知到用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="userId">用户id集合</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        public virtual async Task PublishAsync(string notificationName, string message, long[] userId = null, NotificationSeverity severity = NotificationSeverity.Info, string detailed = null)
        {
            List<UserIdentifier> userList = new List<UserIdentifier>();
            //设置接收用户
            foreach (var u in userId)
            {
                userList.Add(new UserIdentifier(1, u));
            }
            UserIdentifier[] userIdentifiers = userList.ToArray();

            await PublishAsync(notificationName, message, userIdentifiers, severity, detailed);
        }

        /// <summary>
        /// 发送通知到用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="userIdentifier">用户对象</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        public virtual async Task PublishAsync(string notificationName, string message, UserIdentifier userIdentifier , NotificationSeverity severity = NotificationSeverity.Info, string detailed = null)
        {
            //设置接收用户
            UserIdentifier[] userIdentifiers = null;
            if (userIdentifier != null)
            {
                userIdentifiers = new UserIdentifier[] { userIdentifier };
            }
            await PublishAsync(notificationName, message, userIdentifiers, severity, detailed);
        }

        /// <summary>
        /// 发送通知到用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="userId">用户id</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        public virtual async Task PublishAsync(string notificationName, string message, long userId, NotificationSeverity severity = NotificationSeverity.Info, string detailed = null)
        {
            //设置接收用户
            UserIdentifier[] userIdentifier = new UserIdentifier[] { new UserIdentifier(1, userId) };
            //
            await PublishAsync(notificationName, message, userIdentifier, severity, detailed);
        }
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
        public virtual void Publish(string notificationName, NotificationData data = null, EntityIdentifier entityIdentifier = null, NotificationSeverity severity = NotificationSeverity.Info, UserIdentifier[] userIds = null)
        {
            AsyncHelper.RunSync(() => PublishAsync(notificationName, data, entityIdentifier, severity, userIds));
        }

        /// <summary>
        /// 发送通知到所有用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        public virtual void PublishAll(string notificationName, string message, NotificationSeverity severity = NotificationSeverity.Info, string detailed = null)
        {
            AsyncHelper.RunSync(() => PublishAllAsync(notificationName, message, severity, detailed));
        }

        /// <summary>
        /// 发送通知到用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="userIdentifier">用户对象集合</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        public virtual void Publish(string notificationName, string message, UserIdentifier[] userIdentifier, NotificationSeverity severity = NotificationSeverity.Info, string detailed = null)
        {
            AsyncHelper.RunSync(() => PublishAsync(notificationName, message, userIdentifier, severity, detailed));
        }

        /// <summary>
        /// 发送通知到用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="userId">用户id集合</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        public virtual void Publish(string notificationName, string message, long[] userId = null, NotificationSeverity severity = NotificationSeverity.Info, string detailed = null)
        {
            AsyncHelper.RunSync(() => PublishAsync(notificationName, message, userId, severity, detailed));
        }

        /// <summary>
        /// 发送通知到用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="userIdentifier">用户对象</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        public virtual void Publish(string notificationName, string message, UserIdentifier userIdentifier, NotificationSeverity severity = NotificationSeverity.Info, string detailed = null)
        {
            AsyncHelper.RunSync(() => PublishAsync(notificationName, message, userIdentifier, severity, detailed));
        }

        /// <summary>
        /// 发送通知到用户
        /// </summary>
        /// <param name="notificationName">订阅名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="userId">用户id</param>
        /// <param name="severity">通知类型枚举</param>
        /// <param name="detailed">消息详细信息</param>
        /// <returns></returns>
        public virtual void Publish(string notificationName, string message, long userId, NotificationSeverity severity = NotificationSeverity.Info, string detailed = null)
        {
            AsyncHelper.RunSync(() => PublishAsync(notificationName, message, userId, severity, detailed));
        }

        #endregion

        #region 聊天消息发送
        /// <summary>
        /// 发送点对点消息
        /// </summary>
        /// <param name="recipientId"></param>
        /// <param name="chatDetailed"></param>
        /// <param name="severity"></param>
        /// <returns></returns>
        public virtual async Task SendChatAsync(long recipientId, string chatDetailed, NotificationSeverity severity = NotificationSeverity.Info)
        {
            //UserIdentifier userIdentifier = new UserIdentifier(AbpSessionExtens.TenantId, recipientId);
            UserIdentifier userIdentifier = new UserIdentifier(null, recipientId);
            //用户不在线直接返回
            var onlineClients = _onlineClientManager.GetAllByUserId(userIdentifier);
            if (onlineClients == null || onlineClients.Count==0)
            {
                return;
            }

            UserInfo userModel = _cacheManagerExtens.GetUserInfoCache(recipientId);
            string promptContent = "您有一条来自[" + userModel.UserNameCn + "]的消息";
            string promptTitle = "您有一条新消息";

            FrameNotificationData frameNotificationData = new FrameNotificationData(promptContent);
            frameNotificationData.NotificationType = "chat";    //推送的类型用于前端JS判断
            frameNotificationData.Title = promptTitle;
            frameNotificationData.NotificationDetailed = chatDetailed;
            frameNotificationData.SendId = AbpSessionExtens.UserId.Value;

            TenantNotification tenantNotification = new TenantNotification()
            {
                Id = Guid.NewGuid(),
                Data = frameNotificationData,
                Severity = severity,
                NotificationName = "站内短信",
                TenantId = userIdentifier.TenantId,
                CreationTime = DateTime.Now
            };

            List<UserNotification> userNotification = new List<UserNotification>();
            userNotification.Add(
             new UserNotification
             {
                 Id = Guid.NewGuid(),
                 Notification = tenantNotification,
                 UserId = userIdentifier.UserId,
                 State = UserNotificationState.Unread,
                 TenantId = userIdentifier.TenantId
             });

            await _realTimeNotifier.SendNotificationsAsync(userNotification.ToArray());
        }

        #endregion

        #region 在线用户

        /// <summary>
        /// 获取用户列表(并且判断是否在线)
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<OnlineClientExtension>> GetAllClients()
        {
            //查询用户列表排除自己
            var userData = await _userInfoRepository.GetAllListAsync(g => g.Id != AbpSessionExtens.UserId.Value);
            //
            var onlineClients = _onlineClientManager.GetAllClients();
            //
            List<OnlineClientExtension> onlineClientList = new List<OnlineClientExtension>();
            foreach (var item in userData)
            {
                OnlineClientExtension onlineClientEx = new OnlineClientExtension();
                //
                var oC = onlineClients.Where(w => w.UserId == item.Id);
                if (oC.Any())
                {
                    onlineClientEx.ConnectionId = oC.ToList()[0].ConnectionId;
                    onlineClientEx.ConnectTime = oC.ToList()[0].ConnectTime;
                    onlineClientEx.IpAddress = oC.ToList()[0].IpAddress;
                    onlineClientEx.TenantId = oC.ToList()[0].TenantId;
                }
                onlineClientEx.UserId = item.Id;
                onlineClientEx.IsOnline = oC.Any();
                onlineClientEx.UserCode = item.UserCode;
                onlineClientEx.UserNameCn = item.UserNameCn;
                onlineClientEx.ImageUrl = item.ImageUrl;

                onlineClientList.Add(onlineClientEx);
            }
            return onlineClientList;
        }

        #endregion


    }
}
