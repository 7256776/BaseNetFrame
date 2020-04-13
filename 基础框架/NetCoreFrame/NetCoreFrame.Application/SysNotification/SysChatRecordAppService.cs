using Abp;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Notifications;
using NetCoreFrame.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Application
{
    [AbpAuthorize]
    [Audited]
    public class SysChatRecordAppService : NetCoreFrameApplicationBase, ISysChatRecordAppService
    {
        private readonly ICacheManagerExtens _cacheManagerExtens;
        private readonly ISysChatRecordRepository _sysChatRecordRepository;


        public SysChatRecordAppService(
            ISysChatRecordRepository sysChatRecordRepository,
            ICacheManagerExtens cacheManagerExtens
            )
        {
            _sysChatRecordRepository = sysChatRecordRepository;
            _cacheManagerExtens = cacheManagerExtens;
        }

        /// <summary>
        /// 获取接收到的消息数量
        /// </summary>
        /// <param name="receiveUserId"></param>
        /// <returns></returns>
        public async Task<List<SysChatRecordSummary>> GetChatRecordSummaryAsync(UserNotificationState state = UserNotificationState.Unread)
        {
            long receiveUserId = AbpSession.UserId.Value;
            return await _sysChatRecordRepository.GetChatRecordSummaryAsync(receiveUserId, state);
        }

        /// <summary>
        /// 获取来自指定发送人的所有消息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagingDto"></param>
        /// <returns></returns>
        public PagedResultDto<SysChatRecord> GetChatRecordListAsync(RequestParam<SysCharPage> requestParam)
        {
            Check.NotNull(requestParam.Params, nameof(requestParam.Params));

            var data = _sysChatRecordRepository
                .GetAll()
                .Where(w =>
                            (w.ReceiveUserId == requestParam.Params.ReceiveUserId && w.SenderUserId == requestParam.Params.SenderUserId && w.SendOrReceive == 2) &&
                            (w.ChatDetailed.Contains(requestParam.Params.ChatDetailed) || requestParam.Params.ChatDetailed == null));
            return data.OrderBy(o => o.ChatState).ThenByDescending(o => o.CreationTime).GetPagingData(requestParam.PagingDto);
        }

        /// <summary>
        /// 获取发送人与接收人相互的所有消息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagingDto"></param>
        /// <returns></returns>
        public PagedResultDto<SysChatRecord> GetChatRecordMutualListAsync(RequestParam<SysCharPage> requestParam)
        {
            Check.NotNull(requestParam.Params, nameof(requestParam.Params));
            var model = requestParam.Params;
            var data = _sysChatRecordRepository.GetAll()
                .Where(w =>
                            ((w.ReceiveUserId == model.ReceiveUserId && w.SenderUserId == model.SenderUserId && w.SendOrReceive == 2) ||
                            (w.ReceiveUserId == model.SenderUserId && w.SenderUserId == model.ReceiveUserId && w.SendOrReceive == 1)) &&
                            (w.ChatDetailed.Contains(model.ChatDetailed) || model.ChatDetailed == null));
            return data.OrderByDescending(o => o.CreationTime).ThenBy(o => o.ChatState).GetPagingData(requestParam.PagingDto);
        }

        /// <summary>
        /// 新增消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task InsertChatRecordAsync(SysCharSend model)
        {
            SysChatRecord sysChatRecord = ObjectMapper.Map<SysChatRecord>(model); 

            sysChatRecord.SenderUserId = AbpSession.UserId.Value;
            List<SysChatRecord> dataList = SysChatRecord.CreateSysChatRecord(sysChatRecord);
            foreach (var item in dataList)
            {
                await _sysChatRecordRepository.InsertChatRecordAsync(item);
            }

        }

        /// <summary>
        /// 删除聊天消息
        /// </summary>
        /// <param name="senderUserId">发送人</param>
        /// <returns></returns>
        public async Task<bool> DeleteChatRecordAsync( long senderUserId)
        {
            //当前登录用户
            long receiveUserId = AbpSession.UserId.Value;
            var res= await _sysChatRecordRepository.DeleteChatRecordAsync(receiveUserId, senderUserId);
            return res > 0 ? true : false;
        }

        /// <summary>
        /// 修改聊天消息读取状态
        /// </summary>
        /// <param name="receiveUserId">接收人</param>
        /// <param name="senderUserId">发送人</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public async Task<bool> UpdateChatRecordStatusAsync(SysChatRecordDto model)
        {
            model.ReceiveUserId = AbpSession.UserId.Value;
            var res = await _sysChatRecordRepository.UpdateChatRecordStatusAsync(model);
            return res > 0 ? true : false;
        }


    }
}
