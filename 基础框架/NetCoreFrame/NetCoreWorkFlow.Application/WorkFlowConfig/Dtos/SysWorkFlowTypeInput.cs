using Abp.AutoMapper;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCoreWorkFlow.Application
{
    [AutoMap(typeof(SysWorkFlowType))]
    public class SysWorkFlowTypeInput
    {
        /// <summary>
        /// 主键ID 
        /// </summary> 
        public Guid? Id { get; set; }

        /// <summary>
        /// 流程类型
        /// </summary>
        [Required(ErrorMessage = "请输入流程类型")]
        public string FlowTypeName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 风格样式
        /// </summary>
        public string FlowTypeColor { get; set; }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
        public bool IsActive { get; set; }


    }

}
