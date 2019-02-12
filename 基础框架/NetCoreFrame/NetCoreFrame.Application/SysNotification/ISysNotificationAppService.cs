﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Notifications;
using Abp.Web.Models;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.Application
{
    public interface ISysNotificationAppService : IApplicationService
    {
        /// <summary>
        /// 新增/修改 通知对象
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        Task<AjaxResponse> SaveNotificationInfoAsync(SysNotificationInfoInput modelInput);

        /// <summary>
        /// 删除通知对象
        /// </summary>
        /// <param name="modelInput"></param>
        /// <returns></returns>
        Task DelNotificationInfoAsync(SysNotificationInfoInput modelInput);

        /// <summary>
        ///  查询订阅的通知以及用户信息
        /// </summary>
        /// <param name="notificationInfo"></param>
        /// <returns></returns>
        Task<List<SysNotificationSubscriptionInfo>> GetSubscriptionByNameAsync(string notificationName);

        /// <summary>
        ///  查询通知名称列表
        /// </summary>
        /// <param name="notificationInfo"></param>
        /// <returns></returns>
        Task<List<SysNotificationInfo>> GetNotificationInfoAllAsync();

        /// <summary>
        ///  查询通知对象
        /// </summary>
        /// <param name="notificationInfo"></param>
        /// <returns></returns>
        Task<SysNotificationInfo> GetNotificationInfoByIdAsync(string id);

        /// <summary>
        /// 设置用户订阅所有的通知
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task UserSubscriptionNotificationInfoAll(long userId);

        /// <summary>
        /// 订阅通知
        /// </summary>
        /// <param name="list">=订阅通知的用户集合</param>
        /// <returns></returns>
        Task InsertSubscriptionAsync(RequestParam<List<SysUserNotification>> requestParam);

        /// <summary>
        /// 退订通知
        /// </summary>
        /// <param name="list">=订阅通知的用户集合</param>
        /// <returns></returns>
        Task DeleteSubscriptionAsync(RequestParam<List<SysUserNotification>> requestParam);

        /// <summary>
        /// 获取用户订阅的通知
        /// </summary>
        /// <returns></returns>
        Task<List<SysNotificationInfo>> GetUserSubscriptionAsync();

        /// <summary>
        /// 获取用户订阅的通知消息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagingDto"></param>
        /// <returns></returns>
        Task<PagedResultDto<SysUserNotificationInfo>> GetUserNotificationsAsync(RequestParam<SysUserNotification> requestParam);

        /// <summary>
        /// 清空消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notificationName"></param>
        /// <returns></returns>
        Task CleanUserNotificationByName(SysUserNotification model);

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
        Task UpdateUserNotificationStatus(RequestParam<List<UserNotificationInfoDto>> requestParam);


    }
}
