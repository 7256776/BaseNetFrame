using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Abp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace NetCoreFrame.Core
{
    public class SysNotificationInfoRepository : EfCoreRepositoryBase<NetCoreFrameDbContext, SysNotificationInfo, Guid>, ISysNotificationInfoRepository
    {

        private readonly IRepository<UserInfo, long> _userInfoRepository;
        private readonly IRepository<NotificationSubscriptionInfo, Guid> _notificationSubscriptionsRepository;
        private readonly IRepository<TenantNotificationInfo, Guid> _tenantNotificationRepository;
        private readonly IRepository<UserNotificationInfo, Guid> _userNotificationInfoRepository;
        private readonly INotificationStore _notificationStore;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysNotificationInfoRepository(
            IDbContextProvider<NetCoreFrameDbContext> dbcontext,
            IRepository<UserInfo, long> userInfoRepository,
            IRepository<NotificationSubscriptionInfo, Guid> notificationSubscriptionsRepository,
            IRepository<TenantNotificationInfo, Guid> tenantNotificationRepository,
            IRepository<UserNotificationInfo, Guid> userNotificationInfoRepository,
            INotificationStore notificationStore) : base(dbcontext)
        {
            _notificationStore = notificationStore;
            _userInfoRepository = userInfoRepository;
            _tenantNotificationRepository = tenantNotificationRepository;
            _userNotificationInfoRepository = userNotificationInfoRepository;
            _notificationSubscriptionsRepository = notificationSubscriptionsRepository;
        }

        /// <summary>
        /// 添加所有用户订阅通知(NotificationName)
        /// (激活状态,未删除的用户)
        /// </summary>
        /// <param name="notificationInfo"></param>
        /// <returns></returns>
        public async Task AddAllUserSubscriptionAsync(SysNotificationInfo notificationInfo)
        {
            var userList = _userInfoRepository.GetAllList(p => p.IsActive == true);

            foreach (var item in userList)
            {
                NotificationSubscriptionInfo subscription = new NotificationSubscriptionInfo()
                {
                    TenantId = 1,
                    UserId = item.Id,
                    NotificationName = notificationInfo.NotificationName
                };
                await _notificationStore.InsertSubscriptionAsync(subscription);
            }
        }

        /// <summary>
        /// 变更订阅了通知(NotificationName)的用户到新的通知类型名称
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<SysNotificationInfo> UpdateAllUserSubscriptionAsync(SysNotificationInfo model)
        {
            //获取修改前通知类型名称
            var oldModel = Get(model.Id);
            //如果两次名称不相同就同步修改订阅表
            if (oldModel.NotificationName != model.NotificationName)
            {
                //查询以往的通知类型名称所有订阅的用户,用于修改新设置通知类型名称
                var subList = _notificationSubscriptionsRepository.GetAllList(p => p.NotificationName == oldModel.NotificationName);
                foreach (var item in subList)
                {
                    item.NotificationName = model.NotificationName;
                    await _notificationSubscriptionsRepository.UpdateAsync(item);
                }
            }
            //此处必须返回同一个对象的上下文才可以保存
            oldModel.NotificationName = model.NotificationName;
            oldModel.NotificationDescribe = model.NotificationDescribe;
            oldModel.NotificationDisplayName = model.NotificationDisplayName;
            oldModel.NotificationType = model.NotificationType;
            return await Task.FromResult(oldModel);
        }

        /// <summary>
        /// 验证通知类型名称是否重复
        /// </summary>
        /// <param name="notificationInfo"></param>
        /// <returns></returns>
        public Task<bool> IsSubscriptionRepeat(SysNotificationInfo notificationInfo)
        {
            var data = base.FirstOrDefault(w => w.NotificationName == notificationInfo.NotificationName && w.Id != notificationInfo.Id);
            if (data != null)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        /// <summary>
        /// 删除通知基础信息以及该通知的订阅
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task DeleteNotificationAndSubscriptionAsync(SysNotificationInfo model)
        {
            //
            await _notificationSubscriptionsRepository.DeleteAsync(s => s.NotificationName == model.NotificationName);
            //
            await DeleteAsync(model);
        }

        /// <summary>
        /// 获取订阅通知 NotificationName 的用户
        /// </summary>
        /// <param name="notificationInfo"></param>
        /// <returns></returns>
        public async Task<List<SysNotificationSubscriptionInfo>> GetSubscriptionByNameAsync(SysNotificationInfo notificationInfo)
        {
            var list = from u in base.Context.UserInfos
                       join n in base.Context.NotificationSubscriptionInfos on
                                new { id = u.Id, nname = notificationInfo.NotificationName } equals new { id = n.UserId, nname = n.NotificationName }
                       into br
                       from un in br.DefaultIfEmpty()
                       select new SysNotificationSubscriptionInfo
                       {
                           UserId = u.Id,
                           UserCode = u.UserCode,
                           UserNameCn = u.UserNameCn,
                           ImageUrl = u.ImageUrl,
                           TenantId = un.TenantId,
                           EntityId = un.EntityId,
                           EntityTypeAssemblyQualifiedName = un.EntityTypeAssemblyQualifiedName,
                           EntityTypeName = un.EntityTypeName,
                           NotificationName = un.NotificationName,
                           IsSubscription = un.NotificationName == null ? false : true
                       };

            return await Task.FromResult(list.ToList());
        }

        /// <summary>
        /// 获取用户订阅的通知
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<SysNotificationInfo>> GetUserSubscriptionAsync(long userId)
        {
            var list = from subscription in base.Context.NotificationSubscriptionInfos
                       join notification in base.Context.SysNotificationInfos on subscription.NotificationName equals notification.NotificationName
                       where subscription.UserId == userId
                       select notification;
            return await Task.FromResult(list.ToList());
        }

        /// <summary>
        /// 获取用户订阅的通知消息信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagingDto"></param>
        /// <returns></returns>
        public virtual async Task<IQueryable<SysUserNotificationInfo>> GetUserNotificationsAsync(SysUserNotificationInfo model)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var query = from userNotificationInfo in base.Context.UserNotificationInfos
                            join userInfo in base.Context.UserInfos on userNotificationInfo.UserId equals userInfo.Id
                            join tenantNotificationInfo in base.Context.TenantNotificationInfos on userNotificationInfo.TenantNotificationId equals tenantNotificationInfo.Id
                            where
                            userNotificationInfo.UserId == model.UserId &&
                            (model.State == null || userNotificationInfo.State == model.State.Value) &&
                            (model.NotificationName == null || tenantNotificationInfo.NotificationName == model.NotificationName) 
                            //取消排序,由主查询进行处理
                            //orderby tenantNotificationInfo.CreationTime descending 
                            select new SysUserNotificationInfo
                            {
                                Id = userNotificationInfo.Id,
                                UserCode = userInfo.UserCode,
                                UserNameCn = userInfo.UserNameCn,
                                Severity = tenantNotificationInfo.Severity,
                                CreatorUserId = tenantNotificationInfo.CreatorUserId,
                                CreationTime = tenantNotificationInfo.CreationTime,
                                TenantId = userNotificationInfo.TenantId,
                                UserId = userNotificationInfo.UserId,
                                Data = tenantNotificationInfo.Data,
                                NotificationName = tenantNotificationInfo.NotificationName,
                                DataTypeName = tenantNotificationInfo.DataTypeName,
                                EntityTypeName = tenantNotificationInfo.EntityTypeName,
                                EntityTypeAssemblyQualifiedName = tenantNotificationInfo.EntityTypeAssemblyQualifiedName,
                                EntityId = tenantNotificationInfo.EntityId,
                                State = userNotificationInfo.State
                            };
                return await Task.FromResult(query);
            }
        }

        /// <summary>
        /// 清空消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notificationName"></param>
        /// <returns></returns>
        public async Task CleanUserNotificationByName(long userId, string notificationName)
        {
            #region Sql方式 清空该用户所订阅的 notificationName 通知信息
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append($" DELETE FROM Sys_NotificationsToUser WHERE UserId='{userId}' AND TenantNotificationId IN (SELECT Id FROM Sys_NotificationsToTenant WHERE NotificationName='{notificationName}')");

            StringBuilder sqlDelStr = new StringBuilder();
            sqlDelStr.Append($" DELETE FROM Sys_NotificationsToTenant WHERE Id NOT IN (SELECT TenantNotificationId FROM Sys_NotificationsToUser)");

            await base.Context.Database.ExecuteSqlCommandAsync(sqlStr.ToString());
            await base.Context.Database.ExecuteSqlCommandAsync(sqlDelStr.ToString());
            #endregion
            #region ef方式 逐条删除效率慢
            ////获取该通知相关的通知信息
            //var notificationList = base.Context.TenantNotificationInfos.Where(w => w.NotificationName == notificationName).Select(e => e.Id);
            ////删除用户通知表的通知信息
            //_userNotificationInfoRepository.DeleteAsync(d =>d.UserId==userId && notificationList.Contains(d.TenantNotificationId));
            ////先执行删除
            //UnitOfWorkManager.Current.SaveChanges();
            ////查询是否该通知是否已经被所有用户删除
            //var idList = base.Context.TenantNotificationInfos
            //                          .Where(w => w.NotificationName == notificationName &&
            //                                               !Context.UserNotificationInfos.Select(s => s.TenantNotificationId).Contains(w.Id)).Select(e => e.Id);
            ////移除掉所有用户删除的通知
            //_tenantNotificationRepository.DeleteAsync(d => idList.Contains(d.Id));
            //return Task.FromResult(0);
            #endregion
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notificationName"></param>
        /// <returns></returns>
        public async Task DelUserNotificationAsync(List<Guid> idList)
        {
            #region ef方式 逐条删除 

            foreach (var item in idList)
            {
                //删除用户通知表的通知信息
                await _userNotificationInfoRepository.DeleteAsync(d => d.Id == item);
            }

            //先执行删除
            UnitOfWorkManager.Current.SaveChanges();

            //该方案在EfCore下需要分两次查询因此改为直接脚本执行
            ////查询是否该通知是否已经被所有用户删除
            //var dataList1 = base.Context.TenantNotificationInfos
            //                          .Where(w => !Context.UserNotificationInfos.Select(s => s.TenantNotificationId).Contains(w.Id)).Select(e => e.Id);
            ////移除掉所有用户删除的通知
            //await _tenantNotificationRepository.DeleteAsync(d => data.Contains(d.Id));


            var result = base.Context.Database.ExecuteSqlCommand("DELETE FROM Sys_NotificationsToTenant WHERE Id NOT IN(SELECT DISTINCT TenantNotificationId FROM Sys_NotificationsToUser)");

            #endregion
        }

        /// <summary>
        /// 修改消息读取状态
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task UpdateUserNotificationStatus(List<UserNotificationInfoDto> list)
        {
            var idList = list.Select(s => s.Id).ToArray();
            UserNotificationState state = list[0].State;
            var data = _userNotificationInfoRepository.GetAll().Where(w => idList.Contains(w.Id));
            foreach (var item in data)
            {

                item.State = state;
                await _userNotificationInfoRepository.UpdateAsync(item);
            }
        }

    }
}
