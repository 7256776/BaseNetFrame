using Abp.AutoMapper;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCoreWorkFlow.Application
{
    [AutoMap(typeof(SysWorkFlowType))]
    public class SysWorkFlowTypeModel
    {
        /// <summary>
        /// 流程类型
        /// </summary>
        public string FlowTypeName { get; set; }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool? IsReadOnly { get; set; }



    }

}
