using Abp.AutoMapper;
using NetCoreFrame.Core;
using System.Collections.Generic;

namespace NetCoreFrame.Application
{
    [AutoMap(typeof(SysRoleToUser))]
    public class SysRoleToUserInput
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
        public List<SysRoleToUserInput> RoleToUserList { get; set; }


    }
     
}
