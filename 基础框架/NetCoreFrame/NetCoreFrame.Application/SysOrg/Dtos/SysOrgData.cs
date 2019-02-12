using Abp.AutoMapper;
using NetCoreFrame.Core;
using System;

namespace NetCoreFrame.Application
{
    [AutoMap(
        typeof(SysOrg),
        typeof(SysOrgInput)
        )]
    public class SysOrgData
    {
        /// <summary>
        /// ID
        /// </summary>	
        public Guid? Id { get; set; }

        /// <summary>
        /// 组织机构编码
        /// </summary>
        public virtual string OrgCode { get; set; }

     
    }
}
