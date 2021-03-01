using Abp.Domain.Entities.Auditing;
using Abp.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    [Table("Sys_ChatRecord")]
    public class SysChatRecord : CreationAuditedEntity<Guid>
    {
      
        public SysChatRecord() { }

        /// <summary>
        /// 发送内容
        /// </summary>
        [Column("ChatDetailed")]
        [Description("发送内容")]
        [StringLength(1000)]
        public virtual string ChatDetailed { get; set; }

        /// <summary>
        /// 发送人id
        /// </summary>
        [Column("SenderUserId")]
        [Description("发送人id")]
        public virtual long SenderUserId { get; set; }

        /// <summary>
        /// 接收人id
        /// </summary>
        [Column("ReceiveUserId")]
        [Description("接收人id")]
        public virtual long ReceiveUserId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Column("ChatState")]
        [Description("状态 0=未读 1=已读")]
        public virtual UserNotificationState ChatState { get; set; }

        /// <summary>
        /// 1=发送 2=接收
        /// </summary>
        [Column("SendOrReceive")]
        [Description("1=发送 2=接收")]
        public virtual int SendOrReceive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditInfo"></param>
        /// <returns></returns>
        public static List<SysChatRecord> CreateSysChatRecord(SysChatRecord auditInfo)
        {
            List<SysChatRecord> list = new List<SysChatRecord>();
            //用于接收方获取的数据
            list.Add(new SysChatRecord()
            {
                ChatDetailed = auditInfo.ChatDetailed,
                ReceiveUserId = auditInfo.ReceiveUserId,
                SenderUserId = auditInfo.SenderUserId,
                ChatState = UserNotificationState.Unread,
                SendOrReceive = 2
            });
            //用户发送方获取的数据
            list.Add(new SysChatRecord()
            {
                ChatDetailed = auditInfo.ChatDetailed,
                ReceiveUserId = auditInfo.ReceiveUserId,
                SenderUserId = auditInfo.SenderUserId,
                ChatState = UserNotificationState.Read,
                SendOrReceive = 1
            });

            return list;
        }


    }
}
