using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreWorkFlow.Core
{
    /// <summary>
    /// 节点集合
    /// </summary>
    [Table("Sys_WorkFlowEndpoint")]
    public class SysWorkFlowEndpoint : Entity<Guid>
    {

        /// <summary>
        /// 流程对象ID 关联 SysWorkFlowSetting 主键ID
        /// </summary>
        [Column("WorkFlowSettingID")]
        [Description("流程对象ID 关联 SysWorkFlowSetting 主键ID")]
        [Required(ErrorMessage = "请输入节点ID")]
        [StringLength(50)]
        public Guid WorkFlowSettingID { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        [Column("UID")]
        [Description("节点ID")]
        [Required(ErrorMessage = "请输入节点ID")]
        [StringLength(50)]
        public string UID { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        [Column("EndpointText")]
        [Description("节点名称")]
        [Required(ErrorMessage = "请输入节点名称")]
        [StringLength(50)]
        public string EndpointText { get; set; }

        /// <summary>
        /// 节点类型
        /// S=开始
        /// E=结束
        /// P=流程
        /// </summary>
        [Column("EndpointType")]
        [Description("节点类型 S=开始, E=结束, P=流程")]
        [Required(ErrorMessage = "请输入节点类型")]
        [StringLength(20)]
        public string EndpointType { get; set; }

        /// <summary>
        /// 坐标 Top 或 Y
        /// </summary>	 
        [Column("OffsetTop")]
        [Description("坐标 Top 或 Y")]
        [Required]
        public int? OffsetTop { get; set; }

        /// <summary>
        /// 坐标 Left 或 X
        /// </summary>	 
        [Column("OffsetLeft")]
        [Description("坐标 LefOffsetLeftt 或 X")]
        [Required]
        public int? OffsetLeft { get; set; }


    }
}
