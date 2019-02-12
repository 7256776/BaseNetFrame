namespace Frame.Core
{

    public class SysNotificationSubscriptionInfo
    {
      
        public SysNotificationSubscriptionInfo()
        {
        }

        /// <summary>
        /// 用户账号
        /// </summary>
        public virtual string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>		
        public virtual string UserNameCn { get; set; }

        /// <summary>
        /// 是否订阅
        /// </summary>
        public virtual bool IsSubscription { get; set; }

        /// <summary>
        /// 租户id
        /// </summary>
        public virtual int? TenantId { get; set; }
      
        /// <summary>
        /// 用户id
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// 用户头像url
        /// </summary>		
        public virtual string ImageUrl { get; set; }

        /// <summary>
        /// 通知名称
        /// </summary>
        public virtual string NotificationName { get; set; }
      
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


    }
}
