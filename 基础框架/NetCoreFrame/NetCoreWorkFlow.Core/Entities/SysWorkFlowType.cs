using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreWorkFlow.Core
{
    /// <summary>
    /// 节点关系集合
    /// </summary>
    [Table("SYS_WORKFLOWTYPE")]
    public class SysWorkFlowType : AuditedEntity<Guid>
    {
       
        /// <summary>
        /// 来源节点ID 关联SysWorkFlowEndpoint Uid
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "流程类型")]
        [Column("FlowTypeName")]
        public string FlowTypeName { get; set; }

        /// <summary>
        /// 目标节点ID 关联SysWorkFlowEndpoint Uid
        /// </summary>
        [StringLength(2000)]
        [Required(ErrorMessage = "描述")]
        [Column("Description")]
        public string Description { get; set; }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly { get; set; }



    }
}
