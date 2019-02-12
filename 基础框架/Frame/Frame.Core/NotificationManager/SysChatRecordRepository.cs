using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Abp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Core
{
    public class SysChatRecordRepository : EfRepositoryBase<DataDbContext, SysChatRecord, Guid>, ISysChatRecordRepository
    {

        private readonly IRepository<SysChatRecord, Guid> _sysChatRecordRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysChatRecordRepository(
            IDbContextProvider<DataDbContext> dbcontext,
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
        /// 获取接收人所有的聊天消息
        /// </summary>
        /// <param name="receiveUserId"></param>
        /// <returns></returns>
        //public async Task<IQueryable<SysChatRecord>> GetChatRecordListAsync(long receiveUserId)
        //{
        //    using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
        //    {
        //        var query = from chat in base.Context.SysChatRecords
        //                    from us in Context.UserInfos
        //                    from ur in Context.UserInfos
        //                    where chat.SenderUserId == us.Id && chat.ReceiveUserId == ur.Id && chat.ReceiveUserId == receiveUserId
        //                    select chat;
        //        return await Task.FromResult(query);
        //    }
        //}

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
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("DELETE FROM   Sys_ChatRecord ");
            sqlStr.Append("WHERE ");
            sqlStr.Append("(SenderUserId =  '" + senderUserId + "'  AND ReceiveUserId = '" + receiveUserId + "' AND SendOrReceive = '2') ");
            sqlStr.Append(" OR  ");
            sqlStr.Append("(SenderUserId = '" + receiveUserId + "'    AND ReceiveUserId = '" + senderUserId + "' AND SendOrReceive = '1') ");

            return base.Context.Database.ExecuteSqlCommandAsync(sqlStr.ToString());
        }

        /// <summary>
        /// 修改聊天消息读取状态
        /// </summary>
        /// <param name="receiveUserId">发送人</param>
        /// <param name="senderUserId">接收人</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public Task<int> UpdateChatRecordStatusAsync(long receiveUserId, long senderUserId, UserNotificationState state)
        {
            int s = Convert.ToInt32(state);
            string sql = "UPDATE SYS_CHATRECORD SET ChatState = '"+ s + "' WHERE ReceiveUserId = '" + receiveUserId + "' AND SenderUserId =  '" + senderUserId + "' AND ChatState != '" + s + "'";

            return base.Context.Database.ExecuteSqlCommandAsync(sql);
        }

    }
}
