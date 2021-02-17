using Abp.AutoMapper;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCoreWorkFlow.Application
{
    [AutoMap(typeof(SysWorkFlowConnection))]
    public class SysWorkFlowConnectionInput
    {
        /// <summary>
        /// 流程对象ID 关联 SysWorkFlowSetting 主键ID
        /// </summary>
        public Guid? WorkFlowSettingID { get; set; }

        /// <summary>
        /// 来源节点ID 关联SysWorkFlowEndpoint Uid
        /// </summary>
        [Required(ErrorMessage = "请输入来源节点ID")]
        public string SourceId { get; set; }

        /// <summary>
        /// 目标节点ID 关联SysWorkFlowEndpoint Uid
        /// </summary>
        [Required(ErrorMessage = "请输入来目标节点ID")]
        public string TargetId { get; set; }
    }
}
