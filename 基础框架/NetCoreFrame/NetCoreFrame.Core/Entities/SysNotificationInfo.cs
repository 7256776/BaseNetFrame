using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    [Table("SYS_NOTIFICATIONINFO")]
    public class SysNotificationInfo : CreationAuditedEntity<Guid>
    {

        public SysNotificationInfo() { }

        /// <summary>
        /// 通知显示名称
        /// </summary>
        [Column("NOTIFICATIONDISPLAYNAME")]
        [StringLength(100)]
        public virtual string NotificationDisplayName { get; set; }

        /// <summary>
        /// 通知类型名称
        /// </summary>
        [Column("NOTIFICATIONNAME")]
        [StringLength(100)]
        public virtual string NotificationName { get; set; }

        /// <summary>
        /// 通知描述
        /// </summary>
        [Column("NOTIFICATIONDESCRIBE")]
        [StringLength(100)]
        public virtual string NotificationDescribe { get; set; }

        /// <summary>
        /// 通知类型
        /// </summary>
        [Column("NOTIFICATIONTYPE")]
        [StringLength(50)]
        public virtual string NotificationType { get; set; }

    }
}
