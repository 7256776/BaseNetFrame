using Abp.AutoMapper;
using NetCoreFrame.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace NetCoreFrame.Application
{
    [AutoMap(typeof(SysNotificationInfo))]
    public class SysNotificationInfoInput
    {

        public virtual Guid? Id { get; set; }

        /// <summary>
        /// 通知类型名称
        /// </summary>
        [Required(ErrorMessage = "请输入通知类型名称")]
        public virtual string NotificationName { get; set; }

        /// <summary>
        /// 通知显示名称
        /// </summary>
        [Required(ErrorMessage = "请输入通知显示名称")]
        public virtual string NotificationDisplayName { get; set; }

        /// <summary>
        /// 通知描述
        /// </summary>
        public virtual string NotificationDescribe { get; set; }

        /// <summary>
        /// 通知类型
        /// </summary>
        public virtual string NotificationType { get; set; }
      

    }
}
