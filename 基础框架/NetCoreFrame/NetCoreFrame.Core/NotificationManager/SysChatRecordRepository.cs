using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Notifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreFrame.Core
{
    public class SysChatRecordRepository : EfCoreRepositoryBase<NetCoreFrameDbContext, SysChatRecord, Guid>, ISysChatRecordRepository
    {

        private readonly IRepository<SysChatRecord, Guid> _sysChatRecordRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysChatRecordRepository(
            IDbContextProvider<NetCoreFrameDbContext> dbcontext,
            IRepository<SysChatRecord, Guid> sysChatRecordRepository) : base(dbcontext)
        {
            _sysChatRecordRepository = sysChatRecordRepository;
        }

        /// <summary>
        /// 获取接收到的消息数量
        /// </summary>
        /// <param name="receiveUserId"></param>
        /// <returns></returns>
        public async Task<List<SysChatRecordSummary>> GetChatRecordSummaryAsync(long receiveUserId, UserNotificationState state = UserNotificationState.Unread)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var query = from chat in base.Context.SysChatRecords
                            from us in Context.UserInfos
                            where chat.SenderUserId == us.Id && chat.ReceiveUserId == receiveUserId && chat.SendOrReceive == 2 && chat.ChatState == state
                            group chat by new
                            {
                                UserNameCn = us.UserNameCn,
                                SenderUserId = us.Id
                            } into lb
                            select new SysChatRecordSummary
                            {
                                UserNameCn = lb.Key.UserNameCn,
                                SenderUserId = lb.Key.SenderUserId,
                                ChatCount = lb.Count()
                            };
                return await Task.FromResult(query.ToList());
            }
        }

        /// <summary>
        /// 新建聊天消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task InsertChatRecordAsync(SysChatRecord model)
        {
            //
            return _sysChatRecordRepository.InsertAsync(model);
        }

        /// <summary>
        /// 删除聊天消息
        /// </summary>
        /// <param name="receiveUserId">接收人</param>
        /// <param name="senderUserId">发送人</param>
        /// <returns></returns>
        public Task<int> DeleteChatRecordAsync(long receiveUserId, long senderUserId)
        {
            #region 这种不兼容其他数据库啊
            //StringBuilder sqlStr = new StringBuilder();
            //sqlStr.Append("DELETE FROM   Sys_ChatRecord ");
            //sqlStr.Append("WHERE ");
            //sqlStr.Append("(SenderUserId = @senderUserId  AND ReceiveUserId = @receiveUserId AND SendOrReceive = '2') ");
            //sqlStr.Append(" OR  ");
            //sqlStr.Append("(SenderUserId = @receiveUserId  AND ReceiveUserId = @senderUserId AND SendOrReceive = '1') ");
            //object[] parameters = { new SqlParameter("@senderUserId", senderUserId), new SqlParameter("@receiveUserId", receiveUserId), };
            //return base.Context.Database.ExecuteSqlCommandAsync(sqlStr.ToString(), parameters);
            #endregion


            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("DELETE FROM   Sys_ChatRecord ");
            sqlStr.Append("WHERE ");
            sqlStr.Append($"(SenderUserId =  '{senderUserId}'  AND ReceiveUserId = '{receiveUserId}' AND SendOrReceive = '2') ");
            sqlStr.Append(" OR  ");
            sqlStr.Append($"(SenderUserId = '{receiveUserId}'    AND ReceiveUserId = '{senderUserId}' AND SendOrReceive = '1') ");

            return base.Context.Database.ExecuteSqlCommandAsync(sqlStr.ToString());
            //return Task.FromResult(1);
        }

        /// <summary>
        /// 修改聊天消息读取状态
        /// </summary>
        /// <param name="receiveUserId">发送人</param>
        /// <param name="senderUserId">接收人</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public Task<int> UpdateChatRecordStatusAsync(SysChatRecordDto model)
        {
            int s = Convert.ToInt32(model.ChatState);
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append($"UPDATE SYS_CHATRECORD SET ChatState = '{s}' WHERE ReceiveUserId = '{model.ReceiveUserId}' AND SenderUserId =  '{model.SenderUserId}' AND ChatState != '{s}'");
            return base.Context.Database.ExecuteSqlCommandAsync(sqlStr.ToString());
        }

    }
}
