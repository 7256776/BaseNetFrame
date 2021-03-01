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
    [Table("SYS_WORKFLOWDATASOURCE")]
    public class SysWorkFlowDataSource : AuditedEntity<Guid>
    {
        /// <summary>
        /// 数据源类型
        /// </summary>
        [StringLength(20)]
        [Required(ErrorMessage = "请输入数据源类型")]
        [Column("DATASOURCETYPE")]
        public string DataSourceType { get; set; }

        /// <summary>
        /// 数据源名称
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "请输入数据源名称")]
        [Column("DataSourceName")]
        public string DataSourceName { get; set; }

        /// <summary>
        /// 数据源获取方式 (预留)
        /// </summary>
        [StringLength(20)]
        //[Required(ErrorMessage = "请输入数据源获取方式")]
        [Column("DataSourceWay")]
        public string DataSourceWay { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required(ErrorMessage = "是否启用")]
        [Column("IsActive")]
        public bool IsActive { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(2000)]
        [Column("Description")]
        public string Description { get; set; }

    }
}
