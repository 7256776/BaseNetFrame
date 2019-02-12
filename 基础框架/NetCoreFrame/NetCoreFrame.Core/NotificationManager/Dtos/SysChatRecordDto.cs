using Abp.Domain.Entities.Auditing;
using Abp.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    public class SysChatRecordDto 
    {
      
        /// <summary>
        /// 发送人id
        /// </summary>
        public virtual long SenderUserId { get; set; }

        /// <summary>
        /// 接收人id
        /// </summary>
        public virtual long ReceiveUserId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual UserNotificationState ChatState { get; set; }

       

    }
}
