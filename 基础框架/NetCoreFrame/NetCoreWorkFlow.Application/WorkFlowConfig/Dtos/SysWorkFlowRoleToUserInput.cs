using Abp.AutoMapper;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;

namespace NetCoreWorkFlow.Application
{
    [AutoMap(typeof(SysWorkFlowRoleToUser))]
    public class SysWorkFlowRoleToUserInput
    {
        /// <summary>
        /// 流程角色ID
        /// </summary>
        public virtual Guid FlowRoleID { get; set; }

        /// <summary>
        /// 用户集合
        /// </summary>
        public List<string> UserList { get; set; }



    }
}
