using Abp.Domain.Entities.Auditing;
using Abp.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    [Table("SYS_CHATRECORD")]
    public class SysChatRecord : CreationAuditedEntity<Guid>
    {
      
        public SysChatRecord() { }

        /// <summary>
        /// 发送内容
        /// </summary>
        [Column("CHATDETAILED")]
        [StringLength(1000)]
        public virtual string ChatDetailed { get; set; }

        /// <summary>
        /// 发送人id
        /// </summary>
        [Column("SENDERUSERID")]
        public virtual long SenderUserId { get; set; }

        /// <summary>
        /// 接收人id
        /// </summary>
        [Column("RECEIVEUSERID")]
        public virtual long ReceiveUserId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Column("CHATSTATE")]
        public virtual UserNotificationState ChatState { get; set; }

        /// <summary>
        /// 1=发送 2=接收
        /// </summary>
        [Column("SENDORRECEIVE")]
        public virtual int SendOrReceive { get; set; }

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
