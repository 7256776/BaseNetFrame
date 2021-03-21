using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreWorkFlow.Core
{
    /// <summary>
    /// 节点关系集合
    /// </summary>
    [Table("Sys_WorkFlowType")]
    public class SysWorkFlowType : AuditedEntity<Guid>
    {

        /// <summary>
        /// 流程类型
        /// </summary>
        [Column("FlowTypeName")]
        [Description("流程类型名称")]
        [Required(ErrorMessage = "请输入流程类型")]
        [StringLength(50)]
        public string FlowTypeName { get; set; }

        /// <summary>
        /// 风格样式
        /// </summary>
        [Column("FlowTypeColor")]
        [Description("风格样式")]
        [StringLength(50)]
        public string FlowTypeColor { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Column("Description")]
        [Description("描述")]
        [StringLength(2000)]
        public string Description { get; set; }

        /// <summary>
        /// 是否只读
        /// </summary>
        [Column("IsReadOnly")]
        [Description("是否只读")]
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// 启用状态 0=false 1=true
        /// </summary>
        [Column("IsActive")]
        [Description("启用状态 0=false 1=true")]
        public bool IsActive { get; set; }



    }
}
