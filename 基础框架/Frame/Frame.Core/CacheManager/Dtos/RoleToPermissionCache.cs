namespace Frame.Core
{
    public class RoleToPermissionCache
    {
        public RoleToPermissionCache()
        {
        }
        /// <summary>
        /// 角色id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 模块id
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 模块或动作显示名称
        /// </summary>
        public string HandleDisplayName { get; set; }

        /// <summary>
        /// 模块或动作名称
        /// </summary>
        public string HandleName { get; set; }

        /// <summary>
        /// 模块或动作授权名称
        /// </summary>
        public string PermissionName { get; set; }

        /// <summary>
        /// 授权模式:1=开放模式 2=登录模式 3=登录模式
        /// </summary>
        public string RequiresAuthModel { get; set; }


    }
}
