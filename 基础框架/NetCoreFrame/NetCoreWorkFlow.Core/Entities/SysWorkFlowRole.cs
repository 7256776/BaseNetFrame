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
    [Table("SYS_WORKFLOWROLE")]
    public class SysWorkFlowRole : AuditedEntity<Guid>
    {
        /// <summary>
        /// 流程对象ID 关联 SysWorkFlowSetting 主键ID
        /// </summary>
        [Required(ErrorMessage = "请输入流程角色名称")]
        [StringLength(50)]
        [Column("FLOWROLENAME")]
        public string FlowRoleName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>		
        [Column("DESCRIPTION")]
        [StringLength(1000)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>		
        [Column("ISACTIVE")]
        [DefaultValue(true)]
        [Required]
        public virtual bool? IsActive { get; set; } = true;



    }
}
