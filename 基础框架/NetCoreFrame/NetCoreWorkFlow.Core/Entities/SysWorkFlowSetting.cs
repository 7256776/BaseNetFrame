using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreWorkFlow.Core
{
    /// <summary>
    /// 流程设置
    /// </summary>
    [Table("SYS_WORKFLOWSETTING")]
    public class SysWorkFlowSetting : AuditedEntity<Guid>
    {
        /// <summary>
        /// 流程名称
        /// </summary>
        [StringLength(100)]
        [Required(ErrorMessage = "请输入流程名称")]
        [Column("WORKFLOWNAME")]
        public string WorkFlowName { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(2000)]
        [Column("DESCRIPTION")]
        public string Description { get; set; }
    }
}
