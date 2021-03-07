using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreWorkFlow.Core
{
    /// <summary>
    /// 流程数据源
    /// </summary>
    [Table("Sys_WorkFlowDataSource")]
    public class SysWorkFlowDataSource : AuditedEntity<Guid>
    {
        /// <summary>
        /// 数据源类型
        /// </summary>
        [Column("DataSourceType")]
        [Description("数据源类型")]
        [Required(ErrorMessage = "请输入数据源类型")]
        [StringLength(50)]
        public string DataSourceType { get; set; }

        /// <summary>
        /// 数据源名称
        /// </summary>
        [Column("DataSourceName")]
        [Description("数据源名称")]
        [Required(ErrorMessage = "请输入数据源名称")]
        [StringLength(50)]
        public string DataSourceName { get; set; }

        /// <summary>
        /// 数据源获取方式 (预留)
        /// </summary>
        [Column("DataSourceWay")]
        [Description("数据源获取方式 (预留)")]
        //[Required(ErrorMessage = "请输入数据源获取方式")]
        [StringLength(20)]
        public string DataSourceWay { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Column("IsActive")]
        [Description("是否启用")]
        [Required(ErrorMessage = "是否启用")]
        public bool IsActive { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Column("Description")]
        [Description("描述")]
        [StringLength(2000)]
        public string Description { get; set; }

    }
}
