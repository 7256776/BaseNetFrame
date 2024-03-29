﻿using Abp.AutoMapper;
using NetCoreFrame.Core;

namespace NetCoreFrame.Application
{
    [AutoMap(typeof(SysRoles))]
    public class SysRoleData
    {
        /// <summary>
        /// 角色ID主键
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public  string RoleName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>		
        public  string Description { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>		
        public  int? TenantId { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>		
        public bool IsActive { get; set; }

    }


}
