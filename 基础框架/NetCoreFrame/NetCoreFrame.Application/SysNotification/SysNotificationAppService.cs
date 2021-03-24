using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Notifications;
using Abp.UI;
using Abp.Web.Models;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Application
{
    [AbpAuthorize]
    [Audited]
    [RemoteService(false)]
    public class SysNotificationAppService : NetCoreFrameApplicationBase, ISysNotificationAppService
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
            SysNotificationInfo notificationInfo = ObjectMapper.Map<SysNotificationInfo>(modelInput);
            //验证通知类型名称(NotificationName)是否重复
            var res = await _sysNotificationInfoRepository.IsSubscriptionRepeat(notificationInfo);
            if (res)
            {
                throw new UserFriendlyException("通知类型名称重复", "您设置的通知类型名称:" + modelInput.NotificationName + "重复");
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
                //清楚该订阅的缓存
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
            SysNotificationInfo notificationInfo = ObjectMapper.Map<SysNotificationInfo>(modelInput);
            await _sysNotificationInfoRepository.DeleteNotificationAndSubscriptionAsync(notificationInfo);
        }

        /// <summary>
        ///  获取通知 NotificationName 相关用户订阅的关系
        ///  查询所有用户
        /// </summary>
        /// <param name="notificationInfo"></param>
        /// <returns>
        ///      IsSubscription = true 订阅 
        ///      IsSubscription = false 为订阅
        /// </returns>
        public async Task<List<SysNotificationSubscriptionInfo>> GetSubscriptionByNameAsync(string notificationName)
        {
            SysNotificationInfo notificationInfo = new SysNotificationInfo() { NotificationName = notificationName };
            return await _sysNotificationInfoRepository.GetSubscriptionByNameAsync(notificationInfo);
        }

        /// <summary>
        /// 获取通知 NotificationName 相关用户订阅的关系
        /// 分页查询
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns>
        ///      IsSubscription = true 订阅 
        ///      IsSubscription = false 为订阅
        /// </returns>
        public async Task<PagedResultDto<SysNotificationSubscriptionInfo>> QueryableSubscriptionByNameAsync(RequestParam<SysUserNotificationInfo> requestParam)
        {                          
            //获取数据Linq
            var resultData = await _sysNotificationInfoRepository.QueryableSubscriptionByNameAsync(requestParam.Params);
            //添加分页对象
            return resultData.GetPagingData(requestParam.PagingDto);
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
        /// 设置用户订阅所有的通知
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task UserSubscriptionNotificationInfoAll(long userId)
        {
            var notificationInfo = await GetNotificationInfoAllAsync();
            List<SysUserNotificationInput> sysUserNotification = new List<SysUserNotificationInput>();
            foreach (var item in notificationInfo)
            {
                sysUserNotification.Add(
                    new SysUserNotificationInput()
                    {
                        UserId = userId,
                        NotificationName = item.NotificationName
                    });
            }
            await InsertSubscriptionAsync(new RequestParam<List<SysUserNotificationInput>>() { Params = sysUserNotification });
        }

        /// <summary>
        /// 订阅通知
        /// </summary>
        /// <param name="list">=订阅通知的用户集合</param>
        /// <param name="notificationName">=通知名称</param>
        /// <returns></returns>
        public async Task InsertSubscriptionAsync(RequestParam<List<SysUserNotificationInput>> requestParam)
        {
            Check.NotNull(requestParam.Params, nameof(requestParam.Params));

            foreach (var user in requestParam.Params)
            {
                //判断是否已经订阅
                var isSubscribed = await _notificationStore.IsSubscribedAsync(new UserIdentifier(null, user.UserId.Value), user.NotificationName, null, null);

                if (!isSubscribed)
                {
                    await _notificationStore.InsertSubscriptionAsync(new NotificationSubscriptionInfo()
                    {
                        UserId = user.UserId.Value,
                        TenantId = AbpSession.TenantId,
                        NotificationName = user.NotificationName
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
        public async Task DeleteSubscriptionAsync(RequestParam<List<SysUserNotificationInput>> requestParam)
        {
            Check.NotNull(requestParam.Params, nameof(requestParam.Params));
             
            foreach (var user in requestParam.Params)
            {
                var userIdentifier = new UserIdentifier(AbpSession.TenantId, user.UserId.Value);
                await _notificationStore.DeleteSubscriptionAsync(userIdentifier, user.NotificationName, null, null);
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
        public async Task<PagedResultDto<SysUserNotificationInfo>> GetUserNotificationsAsync(RequestParam<SysUserNotificationInput> requestParam)
        {
            Check.NotNull(requestParam.Params, nameof(requestParam.Params));

            SysUserNotificationInfo sysUserNotificationInfo = ObjectMapper.Map<SysUserNotificationInfo>(requestParam.Params);
            if (sysUserNotificationInfo.UserId==null)
            {
                sysUserNotificationInfo.UserId = AbpSession.UserId;
            }
            var data = await _sysNotificationInfoRepository.GetUserNotificationsAsync(sysUserNotificationInfo);
            return data.OrderBy(o=>o.State).ThenByDescending(o=>o.CreationTime).GetPagingData(requestParam.PagingDto);
        }

        /// <summary>
        /// 清空所订阅的 notificationName 通知信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notificationName"></param>
        /// <returns></returns>
        public async Task CleanUserNotificationByName(SysUserNotificationInput model)
        {
            await _sysNotificationInfoRepository.CleanUserNotificationByName(model.UserId.Value, model.NotificationName);
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
        public async Task UpdateUserNotificationStatus(RequestParam<List<UserNotificationInfoDto>> requestParam)
        {  
            if (requestParam.Params == null || requestParam.Params.Count == 0)
            {
                return;
            }
            await _sysNotificationInfoRepository.UpdateUserNotificationStatus(requestParam.Params);
        }

    }
}
