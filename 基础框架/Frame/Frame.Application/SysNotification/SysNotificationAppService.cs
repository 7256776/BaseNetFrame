using Abp;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Notifications;
using Abp.Web.Models;
using Frame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frame.Application
{
    [AbpAuthorize]
    [Audited]
    public class SysNotificationAppService : FrameExtApplicationService, ISysNotificationAppService
    {
        private readonly INotificationStore _notificationStore;
        private readonly ICacheManagerExtens _cacheManagerExtens;
        private readonly ISysNotificationInfoRepository _sysNotificationInfoRepository;


        public SysNotificationAppService(
            INotificationStore notificationStore,
            ICacheManagerExtens cacheManagerExtens,
            ISysNotificationInfoRepository sysNotificationInfoRepository
            )
        {
            _notificationStore = notificationStore;
            _cacheManagerExtens = cacheManagerExtens;
            _sysNotificationInfoRepository = sysNotificationInfoRepository;
        }

        /// <summary>
        /// 新增/修改 通知对象
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public async Task<AjaxResponse> SaveNotificationInfoAsync(SysNotificationInfoInput modelInput)
        {
            SysNotificationInfo notificationInfo = modelInput.MapTo<SysNotificationInfo>();

            //验证通知类型名称(NotificationName)是否重复
            var res = await _sysNotificationInfoRepository.IsSubscriptionRepeat(notificationInfo);
            if (res)
            {
                //throw new UserFriendlyException(L("IsRepeat", modelInput.NotificationName));
                return new AjaxResponse { Success = false, Error = new ErrorInfo(L("IsRepeat", modelInput.NotificationName)) };
            }
            if (modelInput.Id == null)
            {
                //新增时默认设置所有所有用户订阅该通知
                await _sysNotificationInfoRepository.AddAllUserSubscriptionAsync(notificationInfo);
            }
            else
            {
                //编辑状态设置编辑后的通知类型名称
                notificationInfo = await _sysNotificationInfoRepository.UpdateAllUserSubscriptionAsync(notificationInfo);
                //
                _cacheManagerExtens.RemoveNotificationCache(notificationInfo.NotificationName);
            }
            //
            var gid = await _sysNotificationInfoRepository.InsertOrUpdateAndGetIdAsync(notificationInfo);
            return new AjaxResponse { Success = true, Result = gid };
        }

        /// <summary>
        /// 删除通知对象
        /// </summary>
        /// <param name="modelInput"></param>
        /// <returns></returns>
        public async Task DelNotificationInfoAsync(SysNotificationInfoInput modelInput)
        {
            SysNotificationInfo notificationInfo = modelInput.MapTo<SysNotificationInfo>();
            await _sysNotificationInfoRepository.DeleteNotificationAndSubscriptionAsync(notificationInfo);
        }

        /// <summary>
        ///  查询订阅该通知的用户
        /// </summary>
        /// <param name="notificationInfo"></param>
        /// <returns></returns>
        public async Task<List<SysNotificationSubscriptionInfo>> GetSubscriptionByNameAsync(string notificationName)
        {
            SysNotificationInfo notificationInfo = new SysNotificationInfo() { NotificationName = notificationName };
            return await _sysNotificationInfoRepository.GetSubscriptionByNameAsync(notificationInfo);
        }

        /// <summary>
        ///  查询通知名称列表
        /// </summary>
        /// <param name="notificationInfo"></param>
        /// <returns></returns>
        public async Task<List<SysNotificationInfo>> GetNotificationInfoAllAsync()
        {
            return await _sysNotificationInfoRepository.GetAllListAsync();
        }

        /// <summary>
        ///  查询通知对象
        /// </summary>
        /// <param name="notificationInfo"></param>
        /// <returns></returns>
        public async Task<SysNotificationInfo> GetNotificationInfoByIdAsync(string id)
        {
            return await _sysNotificationInfoRepository.GetAsync(Guid.Parse(id));
        }

        /// <summary>
        /// 订阅通知
        /// </summary>
        /// <param name="list">=订阅通知的用户集合</param>
        /// <param name="notificationName">=通知名称</param>
        /// <returns></returns>
        public async Task InsertSubscriptionAsync(List<long> userList, string notificationName)
        {
            foreach (var user in userList)
            {
                //判断是否已经订阅
                var isSubscribed = await _notificationStore.IsSubscribedAsync(new UserIdentifier(null, user), notificationName, null, null);

                if (!isSubscribed)
                {
                    await _notificationStore.InsertSubscriptionAsync(new NotificationSubscriptionInfo()
                    {
                        UserId = user,
                        TenantId = AbpSession.TenantId,
                        NotificationName = notificationName
                    });
                }
            }
        }

        /// <summary>
        /// 退订通知
        /// </summary>
        /// <param name="list">=订阅通知的用户集合</param>
        /// <param name="notificationName">=通知名称</param>
        /// <returns></returns>
        public async Task DeleteSubscriptionAsync(List<long> userList, string notificationName)
        {
            if (string.IsNullOrEmpty(notificationName))
            {
                return;
            }
            foreach (var user in userList)
            {
                var userIdentifier = new UserIdentifier(AbpSession.TenantId, user);
                await _notificationStore.DeleteSubscriptionAsync(userIdentifier, notificationName, null, null);
            }
        }

        /// <summary>
        /// 获取用户订阅的通知
        /// </summary>
        /// <returns></returns>
        public async Task<List<SysNotificationInfo>> GetUserSubscriptionAsync()
        {
            return await _sysNotificationInfoRepository.GetUserSubscriptionAsync(AbpSession.UserId.Value);
        }

        /// <summary>
        /// 获取用户订阅的通知消息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagingDto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<SysUserNotificationInfo>> GetUserNotificationsAsync(SysUserNotification model, PagingDto pagingDto)
        {
            SysUserNotificationInfo sysUserNotificationInfo = model.MapTo<SysUserNotificationInfo>();
            if (sysUserNotificationInfo.UserId==null)
            {
                sysUserNotificationInfo.UserId = AbpSession.UserId;
            }
            var data = await _sysNotificationInfoRepository.GetUserNotificationsAsync(sysUserNotificationInfo);
            return data.OrderBy(o=>o.State).ThenByDescending(o=>o.CreationTime).GetPagingData(pagingDto);
        }

        /// <summary>
        /// 清空消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notificationName"></param>
        /// <returns></returns>
        public async Task CleanUserNotificationByName(long userId, string notificationName)
        {
            await _sysNotificationInfoRepository.CleanUserNotificationByName(userId, notificationName);
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notificationName"></param>
        /// <returns></returns>
        public async Task DelUserNotificationAsync(List<Guid> idList)
        {
            await _sysNotificationInfoRepository.DelUserNotificationAsync(idList);
        }

        /// <summary>
        /// 修改消息读取状态
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task UpdateUserNotificationStatus(List<Guid> idList, UserNotificationState state)
        {
            if (idList == null || idList.Count == 0)
            {
                return;
            }
            await _sysNotificationInfoRepository.UpdateUserNotificationStatus(idList, state);
        }

    }
}
