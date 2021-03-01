using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    /// <summary>
 	/// Sys_Org
 	/// </summary>
 	[Table("Sys_Org")]
    public class SysOrg : AuditedEntity<Guid>
    {
        public SysOrg()
        {    }

        /// <summary>
        /// 上级节点主键
        /// </summary>		
        [Column("ParentOrgID")]
        [Description("上级节点主键")]
        public virtual Guid? ParentOrgID { get; set; }

        /// <summary>
        /// 组织机构编码
        /// </summary>		
        [Column("OrgCode")]
        [Description("组织机构编码")]
        [StringLength(100)]
        [Required]
        public virtual string OrgCode { get; set; }

        /// <summary>
        /// 组织机构名称
        /// </summary>		
        [Column("OrgName")]
        [Description("组织机构名称")]
        [Required(ErrorMessage = "请输入组织机构名称")]
        [StringLength(100)]
        public virtual string OrgName { get; set; }

        /// <summary>
        /// 组织机构节点编码
        /// </summary>		
        [Column("OrgNode")]
        [Description("组织机构节点编码")]
        [Required(ErrorMessage = "请设置组织机构节点编码")]
        [StringLength(1000)]
        public virtual string OrgNode { get; set; }

        /// <summary>
        /// 组织机构类型 
        /// </summary>		
        [Column("OrgType")]
        [Description("组织机构类型")]
        [Required(ErrorMessage = "请输入组织机构类型")]
        [StringLength(20)]
        public virtual string OrgType { get; set; }

        /// <summary>
        /// 组织机构是否启用
        /// </summary>		
        [Column("IsActive")]
        [Required]
        public virtual bool IsActive { get; set; } = true;

        /// <summary>
        /// 描述
        /// </summary>		
        [Column("Description")]
        [Description("描述")]
        [StringLength(2000)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>	 
        [Column("OrderBy")]
        [Description("排序")]
        [DefaultValue(1)]
        public virtual int? OrderBy { get; set; }

        /// <summary>
        /// 当前组织节点对象子节点
        /// </summary>
        [NotMapped]
        public virtual List<SysOrg> ChildrenSysOrg { get; set; }

        /// <summary>
        /// 等级 1 -  n
        /// </summary>
        [NotMapped]
        public virtual int? SysOrgLevel { get; set; }
        
        /// <summary>
        /// 是否叶子节点
        /// </summary>
        [NotMapped]
        public virtual bool IsLeaf { get; set; }

        /// <summary>
        /// 是否选中状态
        /// </summary>
        [NotMapped]
        public virtual bool IsCheck { get; set; }
    }
}
