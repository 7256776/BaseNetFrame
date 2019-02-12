using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    /// <summary>
 	/// Sys_Org
 	/// </summary>
 	[Table("SYS_ORG")]
    public class SysOrg : Entity<Guid>
    {
        public SysOrg()
        {    }

        /// <summary>
        /// 上级节点主键
        /// </summary>		
        [Column("PARENTORGID")]
        public virtual Guid? ParentOrgID { get; set; }

        /// <summary>
        /// 组织机构编码
        /// </summary>		
        [Column("ORGCODE")]
        [StringLength(100)]
        [Required]
        public virtual string OrgCode { get; set; }

        /// <summary>
        /// 组织机构名称
        /// </summary>		
        [Column("ORGNAME")]
        [StringLength(100)]
        [Required(ErrorMessage = "请输入组织机构名称")]
        public virtual string OrgName { get; set; }

        /// <summary>
        /// 组织机构节点编码
        /// </summary>		
        [Column("ORGNODE")]
        [StringLength(1000)]
        [Required(ErrorMessage = "请设置组织机构节点编码")]
        public virtual string OrgNode { get; set; }


        /// <summary>
        /// 组织机构类型 
        /// </summary>		
        [Column("ORGTYPE")]
        [StringLength(20)]
        [Required(ErrorMessage = "请输入组织机构类型")]
        public virtual string OrgType { get; set; }


        /// <summary>
        /// 组织机构是否启用
        /// </summary>		
        [Column("ISACTIVE")]
        [Required]
        public virtual bool IsActive { get; set; } = true;

        /// <summary>
        /// 描述
        /// </summary>		
        [Column("DESCRIPTION")]
        [StringLength(1000)]
        public virtual string Description { get; set; }

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
