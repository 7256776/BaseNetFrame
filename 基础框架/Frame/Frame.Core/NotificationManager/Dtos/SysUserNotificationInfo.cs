using Abp.Notifications;
using System;

namespace Frame.Core
{
    /// <summary>
    /// 用户收到的消息对象
    /// </summary>
    public class SysUserNotificationInfo
    {
      
        public SysUserNotificationInfo()
        {
        }

        /// <summary>
        /// 用户账号
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public virtual string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>		
        public virtual string UserNameCn { get; set; }

        /// <summary>
        /// 发送通知图标类型
        /// </summary>		
        public virtual NotificationSeverity Severity { get; set; }

        /// <summary>
        /// 发送通知的用户id
        /// </summary>		
        public virtual long? CreatorUserId { get; set; }

        /// <summary>
        /// 发送通知的日期
        /// </summary>		
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 租户id
        /// </summary>
        public virtual int? TenantId { get; set; }
      
        /// <summary>
        /// 用户id
        /// </summary>
        public virtual long? UserId { get; set; }
      
        /// <summary>
        /// 通知名称
        /// </summary>
        public virtual string NotificationName { get; set; }

        /// <summary>
        /// 数据对象
        /// </summary>
        public virtual string Data { get; set; }

        /// <summary>
        /// 数据对象名称
        /// </summary>
        public virtual string DataTypeName { get; set; }

        /// <summary>
        /// 对象名称
        /// </summary>
        public virtual string EntityTypeName { get; set; }
     
        /// <summary>
        /// 对象命名空间全称
        /// </summary>
        public virtual string EntityTypeAssemblyQualifiedName { get; set; }
      
        /// <summary>
        /// 对象id
        /// </summary>
        public virtual string EntityId { get; set; }

        /// <summary>
        /// 读取状态
        /// </summary>
        public virtual UserNotificationState? State { get; set; }

    }
}
