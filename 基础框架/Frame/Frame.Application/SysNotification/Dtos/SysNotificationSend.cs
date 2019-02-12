using Abp.Notifications;
using System.Collections.Generic;

namespace Frame.Application
{
    /// <summary>
    /// 通知发送对象
    /// </summary>
    public class SysNotificationSend
    {
        /// <summary>
        /// 通知类型名称
        /// </summary>
        public virtual string NotificationName { get; set; }

        /// <summary>
        /// 通知标题
        /// </summary>
        public virtual string NotificationTitle { get; set; }

        /// <summary>
        /// 通知内容
        /// </summary>
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
