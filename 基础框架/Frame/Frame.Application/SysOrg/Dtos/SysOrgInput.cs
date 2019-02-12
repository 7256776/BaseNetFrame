using Abp.AutoMapper;
using Frame.Core;
using System;

namespace Frame.Application
{
    [AutoMap(typeof(SysOrg))]
    public class SysOrgInput
    {
        /// <summary>
        /// ID
        /// </summary>	
        public Guid? Id { get; set; }

        /// <summary>
        /// 上级节点主键
        /// </summary>
        public virtual Guid? ParentOrgID { get; set; }

        /// <summary>
        /// 组织机构编码
        /// </summary>
        public virtual string OrgCode { get; set; }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        public virtual string OrgName { get; set; }

        /// <summary>
        /// 组织机构类型 
        /// </summary>
        public virtual string OrgType { get; set; }

        /// <summary>
        /// 组织机构是否启用
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }
    }
}
