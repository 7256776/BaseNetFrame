using Abp.Domain.Entities.Auditing;
using Abp.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    public class UserNotificationInfoDto
    {
      
        /// <summary>
        /// 用户id
        /// </summary>
        public virtual Guid Id { get; set; }
       
        /// <summary>
        /// 读取状态
        /// </summary>
        public virtual UserNotificationState State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? CreationTime { get; set; }


    }
}
