using Abp.AutoMapper;
using NetCoreFrame.Core;
using System.Collections.Generic;

namespace NetCoreFrame.Application
{

    /// <summary>
    /// 用作角色用户授权功能的返回对象
    /// </summary>
    public class SysRoleToUserData
    {
        /// <summary>
        /// 待授权用户(通常是查询所有用户)
        /// </summary>
        public List<UserInfoData> RoleNotUser { get; set; }

        /// <summary>
        /// 角色名称(已授权用户对象)
        /// </summary>
        public List<SysRoleUserData> RoleInUser { get; set; }

        /// <summary>
        /// 角色关联的用户
        /// </summary>
        public List<SysRoleToUser> SysRoleToUser { get; set; }

    }
}
