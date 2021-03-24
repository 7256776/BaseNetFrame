using Abp.AutoMapper;
using NetCoreFrame.Core;
using System.Collections.Generic;

namespace NetCoreFrame.Application
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMap(typeof(SysRoleToUser))]
    public class SysRoleUserData
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

}
