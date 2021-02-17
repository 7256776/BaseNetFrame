using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreWorkFlow.Core
{
    /// <summary>
    /// 节点集合
    /// </summary>
    [Table("SYS_WORKFLOWENDPOINT")]
    public class SysWorkFlowEndpoint : Entity<Guid>
    {

        /// <summary>
        /// 流程对象ID 关联 SysWorkFlowSetting 主键ID
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "请输入节点ID")]
        [Column("WORKFLOWSETTINGID")]
        public Guid WorkFlowSettingID { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "请输入节点ID")]
        [Column("UID")]
        public string UID { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "请输入节点名称")]
        [Column("ENDPOINTTEXT")]
        public string EndpointText { get; set; }

        /// <summary>
        /// S=开始
        /// E=结束
        /// P=流程
        /// 节点类型
        /// </summary>
        [StringLength(20)]
        [Required(ErrorMessage = "请输入节点类型")]
        [Column("ENDPOINTTYPE")]
        public string EndpointType { get; set; }

        /// <summary>
        /// 坐标 Top 或 Y
        /// </summary>	 
        [Required]
        [Column("OFFSETTOP")]
        public int? OffsetTop { get; set; }

        /// <summary>
        /// 坐标 Top 或 Y
        /// </summary>	 
        [Required]
        [Column("OFFSETLEFT")]
        public int? OffsetLeft { get; set; }


    }
}
