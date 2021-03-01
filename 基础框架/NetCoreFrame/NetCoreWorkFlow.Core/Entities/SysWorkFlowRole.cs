using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreWorkFlow.Core
{
    /// <summary>
    /// 审核角色
    /// </summary>
    [Table("Sys_WorkFlowRole")]
    public class SysWorkFlowRole : AuditedEntity<Guid>
    {
        /// <summary>
        /// 流程对象ID
        /// </summary>
        [Column("FlowRoleName")]
        [Description("流程角色名称")]
        [Required(ErrorMessage = "请输入流程角色名称")]
        [StringLength(50)]
        public string FlowRoleName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>		
        [Column("Description")]
        [Description("描述")]
        [StringLength(2000)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>		
        [Column("IsActive")]
        [Description("是否启用 0=否, 1=是")]
        [DefaultValue(true)]
        [Required]
        public virtual bool? IsActive { get; set; } = true;



    }
}
