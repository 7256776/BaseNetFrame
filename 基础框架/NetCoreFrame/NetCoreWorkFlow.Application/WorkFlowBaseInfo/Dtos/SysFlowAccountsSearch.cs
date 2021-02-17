using Abp.AutoMapper;
using System;

namespace NetCoreWorkFlow.Application
{
    public class SysFlowAccountsSearch
    {

        /// <summary>
        /// 用户账号
        /// </summary>
        public virtual string UserCodeOrName { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>		
        public string OrgCode { get; set; }

    }
}
