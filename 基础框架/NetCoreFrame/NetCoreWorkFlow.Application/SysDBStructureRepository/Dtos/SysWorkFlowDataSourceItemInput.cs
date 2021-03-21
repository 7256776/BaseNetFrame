using Abp.AutoMapper;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCoreWorkFlow.Application
{

    /// <summary>
    /// 流程数据源明细
    /// </summary>
    [AutoMap(typeof(SysWorkFlowDataSourceItem))]
    public class SysWorkFlowDataSourceItemInput
    {

        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 主数据源对象ID
        /// </summary>
        [Required(ErrorMessage = "请设置数据源主表ID")]
        public string DataSourceId { get; set; }

        /// <summary>
        /// 数据源明细名称
        /// </summary>
        [Required(ErrorMessage = "请设置数据源别名")]
        public string FieldAliasName { get; set; }

        /// <summary>
        /// 数据源字段名称
        /// </summary>
        [Required(ErrorMessage = "请设置数据源字段名称")]
        public string FieldName { get; set; }

        /// <summary>
        /// 数据源名称
        /// </summary>
        [Required(ErrorMessage = "请输入数据源字段数据类型")]
        public string FieldDataType { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required(ErrorMessage = "是否启用")]
        public bool IsActive { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 数据状态
        /// ADD, EDIT, NONE
        /// </summary>
        public string States { get; set; }

    }

}
