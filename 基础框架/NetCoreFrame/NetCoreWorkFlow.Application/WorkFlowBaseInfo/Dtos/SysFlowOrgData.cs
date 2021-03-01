using Abp.AutoMapper;
using System;
using System.Collections.Generic;

namespace NetCoreWorkFlow.Application
{
    /// <summary>
    /// 组织机构
    /// </summary>
    public class SysFlowOrgData
    {
        /// <summary>
        /// ID
        /// </summary>	
        public string OrgId { get; set; }

        /// <summary>
        /// 上级节点主键
        /// </summary>
        public virtual string ParentOrgID { get; set; }

        /// <summary>
        /// 组织机构编码
        /// </summary>
        public virtual string OrgCode { get; set; }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        public virtual string OrgName { get; set; }

        /// <summary>
        /// 组织机构节点
        /// </summary>
        public virtual string OrgNode { get; set; }

        /// <summary>
        /// 组织机构类型 
        /// 公司 = 1;  部门 = 2
        /// </summary>
        public virtual string OrgType { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int? OrderBy { get; set; }

        /// <summary>
        /// 当前组织节点对象子节点 
        /// </summary>
        public virtual List<SysFlowOrgData> ChildrenOrg { get; set; }

        /// <summary>
        /// 等级 1 -  n
        /// </summary>
        public virtual int? SysOrgLevel { get; set; }

        /// <summary>
        /// 是否叶子节点
        /// </summary>
        public virtual bool IsLeaf { get; set; }

        /// <summary>
        /// 是否选中状态
        /// </summary>
        public virtual bool IsCheck { get; set; }

    }
}
