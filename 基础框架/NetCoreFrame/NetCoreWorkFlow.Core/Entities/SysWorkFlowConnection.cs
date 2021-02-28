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
    [Table("Sys_WorkFlowConnection")]
    public class SysWorkFlowConnection : Entity<Guid>
    {
        /// <summary>
        /// 流程对象ID 关联 SysWorkFlowSetting 主键ID
        /// </summary>
        [Column("WorkFlowSettingID")]
        [Description("流程对象ID 关联 SysWorkFlowSetting 主键ID")]
        [Required(ErrorMessage = "请输入节点ID")]
        public Guid WorkFlowSettingID { get; set; }

        /// <summary>
        /// 来源节点ID 关联SysWorkFlowEndpoint Uid
        /// </summary>
        [Column("SourceId")]
        [Description("来源节点ID 关联SysWorkFlowEndpoint Uid")]
        [Required(ErrorMessage = "请输入来源节点ID")]
        [StringLength(50)]
        public string SourceId { get; set; }

        /// <summary>
        /// 目标节点ID 关联SysWorkFlowEndpoint Uid
        /// </summary>
        [Column("TargetId")]
        [Description("目标节点ID 关联SysWorkFlowEndpoint Uid")]
        [Required(ErrorMessage = "请输入来目标节点ID")]
        [StringLength(50)]
        public string TargetId { get; set; }


    }
}
