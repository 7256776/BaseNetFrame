using Abp.Domain.Repositories;
using Abp.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frame.Core
{
    /// <summary>
    /// 通知基础信息仓储
    /// </summary>
    public interface ISysChatRecordRepository : IRepository<SysChatRecord, Guid>
    {
        /// <summary>
        /// 获取接收到的消息数量
        /// </summary>
        /// <param name="receiveUserId"></param>
        /// <returns></returns>
        Task<List<SysChatRecordSummary>> GetChatRecordSummaryAsync(long receiveUserId, UserNotificationState state = UserNotificationState.Unread);

        /// <summary>
        /// 新建聊天消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task InsertChatRecordAsync(SysChatRecord model);

        /// <summary>
        /// 删除聊天消息
        /// </summary>
        /// <param name="receiveUserId">接收人</param>
        /// <param name="senderUserId">发送人</param>
        /// <returns></returns>
        Task<int> DeleteChatRecordAsync(long receiveUserId, long senderUserId);

        /// <summary>
        /// 获取接收人所有的聊天消息
        /// </summary>
        /// <param name="receiveUserId"></param>
        /// <returns></returns>
        //Task<IQueryable<SysChatRecord>> GetChatRecordListAsync(long receiveUserId);

        /// <summary>
        /// 修改聊天消息读取状态
        /// </summary>
        /// <param name="receiveUserId">发送人</param>
        /// <param name="senderUserId">接收人</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        Task<int> UpdateChatRecordStatusAsync(long receiveUserId, long senderUserId, UserNotificationState state);


    }
}
