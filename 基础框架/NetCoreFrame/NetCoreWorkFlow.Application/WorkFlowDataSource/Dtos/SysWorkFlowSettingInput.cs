using Abp.AutoMapper;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCoreWorkFlow.Application
{
    [AutoMap(typeof(SysWorkFlowDataSource))]
    public class SysWorkFlowDataSourceInput
    {
        /// <summary>
        /// ID
        /// </summary>	
        public Guid? Id { get; set; }

        /// <summary>
        /// 数据源类型
        /// </summary>
        [StringLength(20)]
        [Required(ErrorMessage = "请输入数据源类型")]
        public string DataSourceType { get; set; }

        /// <summary>
        /// 数据源名称
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "请输入数据源名称")]
        public string DataSourceName { get; set; }

        /// <summary>
        /// 数据源获取方式 (预留)
        /// </summary>
        [StringLength(20)]
        //[Required(ErrorMessage = "请输入数据源获取方式")]
        public string DataSourceWay { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required(ErrorMessage = "是否启用")]
        public bool IsActive { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(2000)]
        public string Description { get; set; }

    }


}
