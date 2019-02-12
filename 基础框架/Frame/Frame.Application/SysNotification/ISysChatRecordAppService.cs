using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Notifications;
using Frame.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frame.Application
{
    public interface ISysChatRecordAppService : IApplicationService
    {
        // <summary>
        /// 获取接收到的消息数量
        /// </summary>
        /// <param name="receiveUserId"></param>
        /// <returns></returns>
        Task<List<SysChatRecordSummary>> GetChatRecordSummaryAsync(UserNotificationState state = UserNotificationState.Unread);

        /// <summary>
        /// 获取来自指定发送人的所有消息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagingDto"></param>
        /// <returns></returns>
        PagedResultDto<SysChatRecord> GetChatRecordListAsync(SysCharPage model, PagingDto pagingDto);

        /// <summary>
        /// 获取发送人与接收人相互的所有消息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagingDto"></param>
        /// <returns></returns>
        PagedResultDto<SysChatRecord> GetChatRecordMutualListAsync(SysCharPage model, PagingDto pagingDto);

        /// <summary>
        /// 新增消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task InsertChatRecordAsync(SysCharSend model);

        /// <summary>
        /// 删除聊天消息
        /// </summary>
        /// <param name="receiveUserId">接收人</param>
        /// <param name="senderUserId">发送人</param>
        /// <returns></returns>
        Task<bool> DeleteChatRecordAsync(long senderUserId);

        /// <summary>
        /// 修改聊天消息读取状态
        /// </summary>
        /// <param name="receiveUserId">发送人</param>
        /// <param name="senderUserId">接收人</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        Task<bool> UpdateChatRecordStatusAsync( long senderUserId, UserNotificationState state);



    }
}
