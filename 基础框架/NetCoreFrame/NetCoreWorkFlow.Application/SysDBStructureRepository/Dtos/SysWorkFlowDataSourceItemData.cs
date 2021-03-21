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
    public class SysWorkFlowDataSourceItemData 
    {

        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 主数据源对象ID
        /// </summary>
        public string DataSourceId { get; set; }

        /// <summary>
        /// 数据源明细名称
        /// </summary>
        public string FieldAliasName { get; set; }

        /// <summary>
        /// 数据源字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 数据源字段数据类型
        /// </summary>
        public string FieldDataType { get; set; }

        /// <summary>
        /// 数据类型是否可以修改
        /// </summary>
        public bool FieldDataDisabled { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
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
