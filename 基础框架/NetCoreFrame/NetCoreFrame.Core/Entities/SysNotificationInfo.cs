using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    [Table("Sys_NotificationInfo")]
    public class SysNotificationInfo : CreationAuditedEntity<Guid>
    {

        public SysNotificationInfo() { }

        /// <summary>
        /// 通知显示名称
        /// </summary>
        [Column("NotificationDisplayName")]
        [Description("通知显示名称")]
        [StringLength(100)]
        public virtual string NotificationDisplayName { get; set; }

        /// <summary>
        /// 通知类型名称
        /// </summary>
        [Column("NotificationName")]
        [Description("通知类型名称")]
        [StringLength(100)]
        public virtual string NotificationName { get; set; }

        /// <summary>
        /// 通知描述
        /// </summary>
        [Column("NotificationDescribe")]
        [Description("通知描述")]
        [StringLength(200)]
        public virtual string NotificationDescribe { get; set; }

        /// <summary>
        /// 通知类型
        /// </summary>
        [Column("NotificationType")]
        [Description("通知类型")]
        [StringLength(50)]
        public virtual string NotificationType { get; set; }

    }
}
