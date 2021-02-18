using Abp.AutoMapper;
using System;

namespace NetCoreWorkFlow.Application
{
    public class SysFlowUserSearch
    {

        /// <summary>
        /// 用户账号
        /// </summary>
        public virtual string UserCodeOrName { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>		
        public virtual string OrgCode { get; set; }

        /// <summary>
        /// 所属部门节点编码
        /// </summary>		
        public virtual string OrgNode { get; set; }

        /// <summary>
        /// 是否包含子节点机构
        /// </summary>		
        public virtual bool IsInclude { get; set; }
        

    }
}
