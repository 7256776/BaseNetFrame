using Abp.AutoMapper;
using Abp.Notifications;
using NetCoreFrame.Core;

namespace NetCoreFrame.Application
{
    /// <summary>
    /// 用户收到的消息对象
    /// </summary>
    [AutoMap(typeof(SysUserNotificationInfo))]
    public class SysUserNotification
    {
      
        public SysUserNotification()
        {
        }

        /// <summary>
        /// 用户id
        /// </summary>
        public virtual long? UserId { get; set; }
      
        /// <summary>
        /// 通知名称
        /// </summary>
        public virtual string NotificationName { get; set; }

        /// <summary>
        /// 读取状态
        /// </summary>
        public virtual UserNotificationState? State { get; set; }

    }
}
