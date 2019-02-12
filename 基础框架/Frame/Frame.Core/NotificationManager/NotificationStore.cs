using Abp;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Abp.Notifications;
using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frame.Core
{
    /// <summary>
    /// 通知数据持久化实现
    /// 也可以采用ISingletonDependency(单例模式)
    /// </summary>
    public class NotificationStore : INotificationStore, ITransientDependency
    {
        private readonly IRepository<NotificationInfo, Guid> _notificationRepository;
        private readonly IRepository<NotificationSubscriptionInfo, Guid> _notificationSubscriptionsRepository;
        private readonly IRepository<TenantNotificationInfo, Guid> _tenantNotificationRepository;
        private readonly IRepository<UserNotificationInfo, Guid> _userNotificationInfoRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ILogger Logger { get; set; }

        /// <summary>
        /// 初始化通知数据持久化实现<see cref="NotificationStore"/> class.
        /// </summary>
        public NotificationStore(
            IRepository<NotificationInfo, Guid> notificationRepository,
            IRepository<TenantNotificationInfo, Guid> tenantNotificationRepository,
            IRepository<UserNotificationInfo, Guid> userNotificationInfoRepository,
            IRepository<NotificationSubscriptionInfo, Guid> notificationSubscriptionsRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _notificationRepository = notificationRepository;
            _tenantNotificationRepository = tenantNotificationRepository;
            _userNotificationInfoRepository = userNotificationInfoRepository;
            _notificationSubscriptionsRepository = notificationSubscriptionsRepository;
            _unitOfWorkManager = unitOfWorkManager;

            Logger = NullLogger.Instance;
        }

        #region 通知订阅

        /// <summary>
        /// 新增 订阅
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task InsertSubscriptionAsync(NotificationSubscriptionInfo subscription)
        {
            await _notificationSubscriptionsRepository.InsertAsync(subscription);
        }

        /// <summary>
        /// 删除用户订阅的通知
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <param name="notificationName">通知名称</param>
        /// <param name="entityTypeName">忽略该参数</param>
        /// <param name="entityId">忽略该参数</param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task DeleteSubscriptionAsync(UserIdentifier user, string notificationName, string entityTypeName = null, string entityId = null)
        {
            //关闭多租户查询
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                await _notificationSubscriptionsRepository.DeleteAsync(s =>
                s.UserId == user.UserId &&
                s.NotificationName == notificationName);
            }
        }

        /// <summary>
        /// 获取订阅了 通知名称=<see cref="notificationName"/> 集合
        /// </summary>
        /// <param name="notificationName">通知名称</param>
        /// <param name="entityTypeName">忽略该参数</param>
        /// <param name="entityId">忽略该参数</param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(string notificationName, string entityTypeName = null, string entityId = null)
        {  
            //关闭多租户查询
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                return _notificationSubscriptionsRepository.GetAllListAsync(s => s.NotificationName == notificationName);
            }
        }

        /// <summary>
        /// 获取租户订阅的通知
        /// (注该方法忽略租户id过滤查询的结果是所有订阅 <see cref="notificationName"/>的信息)
        /// </summary>
        /// <param name="tenantId">租户Id</param>
        /// <param name="notificationName">通知名称</param>
        /// <param name="entityTypeName">忽略该参数</param>
        /// <param name="entityId">忽略该参数</param>
        /// <returns></returns>
        [UnitOfWork]
        protected virtual async Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(int? tenantId, string notificationName, string entityTypeName, string entityId)
        {
            //关闭多租户查询
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                return await _notificationSubscriptionsRepository.GetAllListAsync(s => s.NotificationName == notificationName);
            }
        }

        /// <summary>
        ///  获取多个租户订阅的通知
        ///  (注该方法忽略租户id过滤查询的结果是所有订阅 <see cref="notificationName"/>的信息)
        /// </summary>
        /// <param name="tenantIds">租户集合</param>
        /// <param name="notificationName">通知名称</param>
        /// <param name="entityTypeName">忽略该参数</param>
        /// <param name="entityId">忽略该参数</param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(int?[] tenantIds, string notificationName, string entityTypeName = null, string entityId = null)
        {
            var subscriptions = new List<NotificationSubscriptionInfo>();
            foreach (var tenantId in tenantIds)
            {
                subscriptions.AddRange(await GetSubscriptionsAsync(tenantId, notificationName, entityTypeName, entityId));
            }
            return subscriptions;
        }

        /// <summary>
        /// 获取用户订阅的通知
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(UserIdentifier user)
        {
            //关闭多租户查询
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                return await _notificationSubscriptionsRepository.GetAllListAsync(s => s.UserId == user.UserId);
            }
        }

        /// <summary>
        /// 验证该用户是否订阅了 <see cref="notificationName"/> 通知
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <param name="notificationName">通知名称</param>
        /// <param name="entityTypeName">忽略该参数</param>
        /// <param name="entityId">忽略该参数</param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<bool> IsSubscribedAsync(UserIdentifier user, string notificationName, string entityTypeName = null, string entityId = null)
        {  //关闭多租户查询
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                return await _notificationSubscriptionsRepository.CountAsync(s =>
                s.UserId == user.UserId &&
                s.NotificationName == notificationName
                ) > 0;
            }
        }

        #endregion

        #region 通知信息列表(该表似乎是在发送通知的过程中存储通知数据)
        /// <summary>
        /// 新增通知信息(发送时产生)
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task InsertNotificationAsync(NotificationInfo notification)
        {
            await _notificationRepository.InsertAsync(notification);
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }

        /// <summary>
        /// 获取通知或null
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async  Task<NotificationInfo> GetNotificationOrNullAsync(Guid notificationId)
        {
            return await _notificationRepository.FirstOrDefaultAsync(notificationId);
        }

        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public virtual async Task DeleteNotificationAsync(NotificationInfo notification)
        {
            await _notificationRepository.DeleteAsync(notification);
        }
        #endregion

        #region 维护接收到的通知

        /// <summary>
        /// 保存接收到的通知
        /// </summary>
        /// <param name="userNotification"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task InsertUserNotificationAsync(UserNotificationInfo userNotification)
        {
            //关闭多租户查询
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                await _userNotificationInfoRepository.InsertAsync(userNotification);
                await _unitOfWorkManager.Current.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 设置通知状态
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="notificationsToUserId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task UpdateUserNotificationStateAsync(int? tenantId, Guid notificationsToUserId, UserNotificationState state)
        {
            var userNotificationInfo = await _userNotificationInfoRepository.FirstOrDefaultAsync(notificationsToUserId);
            if (userNotificationInfo == null)
            {
                return;
            }
            userNotificationInfo.State = state;
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }

        /// <summary>
        /// 设置用户 <see cref="user"/> 所有通知的状态
        /// </summary>
        /// <param name="user"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task UpdateAllUserNotificationStatesAsync(UserIdentifier user, UserNotificationState state)
        {
            var userNotifications = await _userNotificationInfoRepository.GetAllListAsync(un => un.UserId == user.UserId);
            foreach (var userNotification in userNotifications)
            {
                userNotification.State = state;
            }
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }

        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="notificationsToUserId"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task DeleteUserNotificationAsync(int? tenantId, Guid notificationsToUserId)
        {
            await _userNotificationInfoRepository.DeleteAsync(notificationsToUserId);
        }

        /// <summary>
        /// 删除通知用户 <see cref="user"/> 所有通知
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task DeleteAllUserNotificationsAsync(UserIdentifier user)
        {
            await _userNotificationInfoRepository.DeleteAsync(un => un.UserId == user.UserId);
        }

        /// <summary>
        /// 查询用户接收到的所有通知
        /// </summary>
        /// <param name="user"></param>
        /// <param name="state"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual Task<List<UserNotificationInfoWithNotificationInfo>> GetUserNotificationsWithNotificationsAsync(UserIdentifier user, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue)
        {
            //关闭多租户查询
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var query = from userNotificationInfo in _userNotificationInfoRepository.GetAll()
                            join tenantNotificationInfo in _tenantNotificationRepository.GetAll() on userNotificationInfo.TenantNotificationId equals tenantNotificationInfo.Id
                            where userNotificationInfo.UserId == user.UserId && (state == null || userNotificationInfo.State == state.Value)
                            orderby tenantNotificationInfo.CreationTime descending
                            select new { userNotificationInfo, tenantNotificationInfo = tenantNotificationInfo };

                query = query.PageBy(skipCount, maxResultCount);

                var list = query.ToList();

                return Task.FromResult(list.Select(
                    a => new UserNotificationInfoWithNotificationInfo(a.userNotificationInfo, a.tenantNotificationInfo)
                    ).ToList());
            }
        }

        /// <summary>
        /// 获取用户接收到通知的总条数
        /// </summary>
        /// <param name="user"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<int> GetUserNotificationCountAsync(UserIdentifier user, UserNotificationState? state = null)
        {
            //关闭多租户查询
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                return await _userNotificationInfoRepository.CountAsync(un => un.UserId == user.UserId && (state == null || un.State == state.Value));
            }
        }

        /// <summary>
        /// 查询该租户下用户的通知
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userNotificationId"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual Task<UserNotificationInfoWithNotificationInfo> GetUserNotificationWithNotificationOrNullAsync(int? tenantId, Guid userNotificationId)
        {
            //关闭多租户查询
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var query = from userNotificationInfo in _userNotificationInfoRepository.GetAll()
                            join tenantNotificationInfo in _tenantNotificationRepository.GetAll() on userNotificationInfo.TenantNotificationId equals tenantNotificationInfo.Id
                            where userNotificationInfo.Id == userNotificationId
                            select new { userNotificationInfo, tenantNotificationInfo = tenantNotificationInfo };

                var item = query.FirstOrDefault();
                if (item == null)
                {
                    return Task.FromResult((UserNotificationInfoWithNotificationInfo)null);
                }

                return Task.FromResult(new UserNotificationInfoWithNotificationInfo(item.userNotificationInfo, item.tenantNotificationInfo));
            }
        }

        /// <summary>
        /// 新增用户通知信息
        /// </summary>
        /// <param name="tenantNotificationInfo"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task InsertTenantNotificationAsync(TenantNotificationInfo tenantNotificationInfo)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                await _tenantNotificationRepository.InsertAsync(tenantNotificationInfo);
            }
        }

        #endregion

    }


}
