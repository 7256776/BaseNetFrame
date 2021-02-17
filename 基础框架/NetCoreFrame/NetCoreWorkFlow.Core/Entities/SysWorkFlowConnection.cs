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
    [Table("SYS_WORKFLOWCONNECTION")]
    public class SysWorkFlowConnection : Entity<Guid>
    {
        /// <summary>
        /// 流程对象ID 关联 SysWorkFlowSetting 主键ID
        /// </summary>
        [Required(ErrorMessage = "请输入节点ID")]
        [Column("WORKFLOWSETTINGID")]
        public Guid WorkFlowSettingID { get; set; }

        /// <summary>
        /// 来源节点ID 关联SysWorkFlowEndpoint Uid
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "请输入来源节点ID")]
        [Column("SOURCEID")]
        public string SourceId { get; set; }

        /// <summary>
        /// 目标节点ID 关联SysWorkFlowEndpoint Uid
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "请输入来目标节点ID")]
        [Column("TARGETID")]
        public string TargetId { get; set; }


    }
}
