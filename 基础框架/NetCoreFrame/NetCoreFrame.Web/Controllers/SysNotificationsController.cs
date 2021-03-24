using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.Notifications;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.Web.Controllers
{
    [AbpMvcAuthorize]
    [DisableAuditing]
    public class SysNotificationsController : NetCoreFrameControllerBase
    {
        /// <summary>
        /// 通知管理
        /// </summary>
        private readonly INotificationManager _notificationManager;

        private readonly ISysNotificationAppService _sysNotificationAppService;
        private readonly ISysChatRecordAppService _sysChatRecordAppService;

        public SysNotificationsController(
            INotificationManager notificationManager,
            ISysNotificationAppService sysNotificationAppService,
            ISysChatRecordAppService sysChatRecordAppService
            )
        {
            _notificationManager = notificationManager;
            _sysNotificationAppService = sysNotificationAppService;
            _sysChatRecordAppService = sysChatRecordAppService;
        }

        #region 通知设置与维护
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult UserInbox()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 保存通知信息
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public async Task<JsonResult> SaveNotificationInfo([FromBody]SysNotificationInfoInput modelInput)
        {
            var ajaxResponse = await _sysNotificationAppService.SaveNotificationInfoAsync(modelInput);
            return Json(ajaxResponse);
        }

        /// <summary>
        /// 删除通知类型
        /// </summary>
        /// <param name="modelInput"></param>
        /// <returns></returns>
        public async Task<JsonResult> DelNotificationInfo([FromBody]SysNotificationInfoInput modelInput)
        {
            await _sysNotificationAppService.DelNotificationInfoAsync(modelInput);
            return Json(true);
        }

        /// <summary>
        ///  查询通知名称列表
        /// </summary>
        /// <param name="notificationInfo"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetNotificationInfoAll()
        {
            var data = await _sysNotificationAppService.GetNotificationInfoAllAsync();
            return Json(data);
        }

        /// <summary>
        ///  查询通知对象
        /// </summary>
        /// <param name="notificationInfo"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetNotificationInfoById([FromBody]string id)
        {
            var data = await _sysNotificationAppService.GetNotificationInfoByIdAsync(id);
            return Json(data);
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
        public async Task<JsonResult> GetSubscriptionByName([FromBody]RequestParam<SysUserNotificationInfo> requestParam)
        {
            var data = await _sysNotificationAppService.QueryableSubscriptionByNameAsync(requestParam);
            return Json(data);
        }

        /// <summary>
        /// 新增用户订阅
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<JsonResult> InsertSubscription([FromBody]RequestParam<List<SysUserNotificationInput>> requestParam)
        {
            await _sysNotificationAppService.InsertSubscriptionAsync(requestParam);
            return Json(true);
        }

        /// <summary>
        /// 删除用户订阅的通知
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<JsonResult> DeleteSubscription([FromBody]RequestParam<List<SysUserNotificationInput>> requestParam)
        {
            await _sysNotificationAppService.DeleteSubscriptionAsync(requestParam);
            return Json(true);
        }

        #endregion

        #region 用户接收的通知消息维护

        /// <summary>
        /// 获取用户订阅的消息
        /// </summary>
        /// <param name="list">=订阅通知的用户集合</param>
        /// <returns></returns>
        public async Task<JsonResult> GetSubscriptionByUser()
        {
            //Task < JsonResult >
            var data = await _sysNotificationAppService.GetUserSubscriptionAsync();
            return Json(data);
        }

        /// <summary>
        /// 获取用户订阅的通知消息
        /// </summary>
        /// <param name="state"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetUserNotifications([FromBody]RequestParam<SysUserNotificationInput> requestParam)
        {
            var data = await _sysNotificationAppService.GetUserNotificationsAsync(requestParam);
            return Json(data);
        }

        /// <summary>
        /// 清空消息
        /// </summary>
        /// <param name="notificationName"></param>
        /// <returns></returns>
        public async Task<JsonResult> CleanUserNotificationByName([FromBody]string notificationName)
        {
            long userId = AbpSession.UserId.Value;
            await _sysNotificationAppService.CleanUserNotificationByName(new SysUserNotificationInput() { UserId = userId, NotificationName = notificationName });
            return Json(true);
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notificationName"></param>
        /// <returns></returns>
        public async Task<JsonResult> DelUserNotification([FromBody]List<Guid> idList)
        {
            if (idList == null || idList.Count == 0)
            {
                return Json(false);
            }
            await _sysNotificationAppService.DelUserNotificationAsync(idList);
            return Json(true);
        }

        /// <summary>
        /// 修改消息读取状态
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<JsonResult> UpdateUserNotificationStatus([FromBody]RequestParam<List<UserNotificationInfoDto>> requestParam)
        {
            if (requestParam.Params == null || requestParam.Params.Count == 0)
            {
                return Json(false);
            }
            await _sysNotificationAppService.UpdateUserNotificationStatus(requestParam);
            return Json(true);
        }

        #endregion

        #region 通知与聊天消息发送

        /// <summary>
        /// 发送消息(广播)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<JsonResult> NotificationsSend([FromBody]SysNotificationSendInput model)
        {
            foreach (var item in model.NotificationNameList)
            {
                if (model.Recipient == null)
                {
                    await _notificationManager.PublishAllAsync(item, model.NotificationTitle, model.Severity, model.NotificationContent);
                }
                else
                {
                    await _notificationManager.PublishAsync(item, model.NotificationTitle, model.Recipient, model.Severity, model.NotificationContent);
                }
            }
            return Json(true);
        }

        /// <summary>
        /// 发送消息(聊天)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<JsonResult> SendChatAsync([FromBody]SysCharSendInput model)
        {
            //推送消息到用户
            await _notificationManager.SendChatAsync(model.ReceiveUserId, model.ChatDetailed, model.Severity);
            //持久化聊天消息
            await _sysChatRecordAppService.InsertChatRecordAsync(model);
            return Json(true);
        }

        /// <summary>
        /// 获取接收到的消息数量
        /// </summary>
        /// <param name="receiveUserId"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetChatRecordSummary([FromBody]UserNotificationState state)
        { 
            var data = await _sysChatRecordAppService.GetChatRecordSummaryAsync(state);
            return Json(data);
        }

        /// <summary>
        /// 获取来自指定发送人的所有消息 SysCharPage model, PagingDto pagingDto
        /// </summary>
        /// <param name="receiveUserId"></param>
        /// <returns></returns>
        public JsonResult GetChatRecordList([FromBody]RequestParam<SysCharParam> requestParam)
        {
            var data = _sysChatRecordAppService.GetChatRecordListAsync(requestParam);
            return Json(data);
        }

        /// <summary>
        /// 获取发送人与接收人相互的所有消息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagingDto"></param>
        /// <returns></returns>
        public JsonResult GetChatRecordMutualList([FromBody]RequestParam<SysCharParam> requestParam)
        {
            var data = _sysChatRecordAppService.GetChatRecordMutualListAsync(requestParam);
            return Json(data);
        }

        /// <summary>
        /// 删除聊天消息
        /// </summary>
        /// <param name="senderUserId">发送人</param>
        /// <returns></returns>
        public async Task<JsonResult> DeleteChatRecord([FromBody]long senderUserId)
        {
            var res = await _sysChatRecordAppService.DeleteChatRecordAsync(senderUserId);
            return Json(res);
        }

        /// <summary>
        /// 修改聊天消息读取状态
        /// </summary>
        /// <param name="receiveUserId">发送人</param>
        /// <param name="senderUserId">接收人</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public async Task<JsonResult> UpdateChatRecordStatus([FromBody]SysChatRecordDto model)
        {
            var res = await _sysChatRecordAppService.UpdateChatRecordStatusAsync(model);
            return Json(res);
        }
        #endregion

        #region 在线用户
        public async Task<JsonResult> GetAllClients()
        {
            var data = await _notificationManager.GetAllClients();
            return Json(data);
        }
        #endregion

    }
}