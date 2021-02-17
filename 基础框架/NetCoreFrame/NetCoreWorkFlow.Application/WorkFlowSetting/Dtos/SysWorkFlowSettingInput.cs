using Abp.AutoMapper;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;

namespace NetCoreWorkFlow.Application
{
    [AutoMap(typeof(SysWorkFlowSetting))]
    public class SysWorkFlowSettingInput
    {
        /// <summary>
        /// ID
        /// </summary>	
        public Guid? Id { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        public string WorkFlowName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 节点集合
        /// </summary>
        public List<SysWorkFlowEndpointInput> WorkFlowEndpointList { get; set; }

        /// <summary>
        /// 关系集合
        /// </summary>
        public List<SysWorkFlowConnectionInput> WorkFlowConnectionList { get; set; }

    }
}
