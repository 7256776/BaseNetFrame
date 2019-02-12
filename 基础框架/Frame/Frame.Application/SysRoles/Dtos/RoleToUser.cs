using Abp.AutoMapper;
using Frame.Core;
using System.Collections.Generic;

namespace Frame.Application
{
    [AutoMap(typeof(SysRoleToUser))]
    public class RoleToUser
    { 
        /// <summary>
        /// 用户ID主键
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 角色ID主键
        /// </summary>
        public long RoleID { get; set; }

        /// <summary>
        /// 用户ID主键集合
        /// </summary>
        public List<RoleToUser> RoleToUserList { get; set; }


    }

    /// <summary>
    /// 
    /// </summary>
    public class RoleUser
    {
        /// <summary>
        /// 用户id主键
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
    }

    /// <summary>
    /// 用作角色用户授权功能的返回对象
    /// </summary>
    public class RoleToUserReturn
    {
        /// <summary>
        /// 待授权用户(通常是查询所有用户)
        /// </summary>
        public List<UserOut> RoleNotUser { get; set; }

        /// <summary>
        /// 角色名称(已授权用户对象)
        /// </summary>
        public List<RoleUser> RoleInUser { get; set; }
    }





}
