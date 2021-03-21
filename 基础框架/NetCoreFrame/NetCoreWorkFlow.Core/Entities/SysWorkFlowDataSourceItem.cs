using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreWorkFlow.Core
{
    /// <summary>
    /// 流程数据源明细
    /// </summary>
    [Table("Sys_WorkFlowDataSourceItem")]
    public class SysWorkFlowDataSourceItem : Entity<Guid>
    {
        /// <summary>
        /// 主数据源对象ID
        /// </summary>
        [Column("DataSourceId")]
        [Description("关联Sys_WorkFlowDataSource表Id")]
        [Required(ErrorMessage = "请设置数据源主表ID")]
        [StringLength(50)]
        public Guid DataSourceId { get; set; }

        /// <summary>
        /// 数据源明细名称
        /// </summary>
        [Column("FieldAliasName")]
        [Description("数据源字段别名")]
        [Required(ErrorMessage = "请设置数据源明细名称")]
        [StringLength(50)]
        public string FieldAliasName { get; set; }

        /// <summary>
        /// 数据源字段名称
        /// </summary>
        [Column("FieldName")]
        [Description("数据源字段名称")]
        [Required(ErrorMessage = "请设置数据源字段名称")]
        [StringLength(50)]
        public string FieldName { get; set; }

        /// <summary>
        /// 数据源名称
        /// </summary>
        [Column("FieldDataType")]
        [Description("数据源字段数据类型")]
        [Required(ErrorMessage = "请输入数据类型")]
        [StringLength(50)]
        public string FieldDataType { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Column("IsActive")]
        [Description("是否启用")]
        [Required(ErrorMessage = "是否启用")]
        public bool? IsActive { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Column("Description")]
        [Description("描述")]
        [StringLength(2000)]
        public string Description { get; set; }

    }

}
