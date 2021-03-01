using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreWorkFlow.Core
{
    /// <summary>
    /// 流程设置
    /// </summary>
    [Table("Sys_WorkFlowSetting")]
    public class SysWorkFlowSetting : AuditedEntity<Guid>
    {
        /// <summary>
        /// 流程名称
        /// </summary>
        [Column("WorkFlowName")]
        [Description("流程名称")]
        [StringLength(100)]
        [Required(ErrorMessage = "流程名称")]
        public string WorkFlowName { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        [Column("Description")]
        [Description("描述")]
        [StringLength(2000)]
        public string Description { get; set; }
    }
}
