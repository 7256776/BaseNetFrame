using Abp.AutoMapper;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCoreWorkFlow.Application
{
    public class SysWorkFlowDataSourceParam
    {
        /// <summary>
        /// 数据源类型
        /// </summary>
        public string DataSourceType { get; set; }

        /// <summary>
        /// 数据源名称
        /// </summary>
        public string DataSourceName { get; set; }


    }

}
