using Abp.Notifications;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCoreFrame.Application
{
    /// <summary>
    /// 通知发送对象
    /// </summary>
    public class SysNotificationSend
    {
        /// <summary>
        /// 通知类型名称
        /// </summary>
        [Required(ErrorMessage = "请输入通知类型名称")]
        [StringLength(50, ErrorMessage = "通知类型名称长度超过50")]
        public virtual string NotificationName { get; set; }

        /// <summary>
        /// 通知标题
        /// </summary>
        public virtual string NotificationTitle { get; set; }

        /// <summary>
        /// 通知内容
        /// </summary>
        [Required(ErrorMessage = "请输入通知内容")]
        public virtual string NotificationContent { get; set; }

        /// <summary>
        /// 通知分类
        /// </summary>
        public virtual NotificationSeverity Severity { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public virtual long[] Recipient { get; set; }
    
        /// <summary>
        /// 通知类型列表
        /// </summary>
        public virtual List<string> NotificationNameList { get; set; }
    }


}
