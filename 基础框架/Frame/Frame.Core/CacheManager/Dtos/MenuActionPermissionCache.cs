namespace Frame.Core
{
    public class MenuActionPermissionCache
    {
        public MenuActionPermissionCache()
        {
        }

        /// <summary>
        /// 动作ID
        /// </summary>
        public long? ActionId { get; set; }

        /// <summary>
        /// 模块ID
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// 动作显示名称
        /// </summary>
        public string ActionDisplayName { get; set; }

        /// <summary>
        /// 模块实现名称
        /// </summary>
        public string MenuDisplayName { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 动作名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 授权名称
        /// </summary>
        public string PermissionName { get; set; }

        /// <summary>
        /// 是否模块对象 
        /// </summary>
        public bool IsMenu { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 授权模式:1=开放模式 2=登录模式 3=授权模式
        /// </summary>
        public string RequiresAuthModel { get; set; }


    }
}
