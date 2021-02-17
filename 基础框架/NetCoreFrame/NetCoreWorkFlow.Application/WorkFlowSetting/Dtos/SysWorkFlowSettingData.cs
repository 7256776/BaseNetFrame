using Abp.AutoMapper;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;

namespace NetCoreWorkFlow.Application
{
    [AutoMap(typeof(SysWorkFlowSetting))]
    public class SysWorkFlowSettingData
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
        /// 最后更新日期
        /// </summary>
        public string CreationTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 最后更新日期
        /// </summary>
        public string LastModificationTime { get; set; }

        /// <summary>
        /// 最后更新人
        /// </summary>
        public string LastModifierUserId { get; set; }

        /// <summary>
        /// 节点集合
        /// </summary>
        public List<SysWorkFlowEndpoint> WorkFlowEndpointList { get; set; }

        /// <summary>
        /// 关系集合
        /// </summary>
        public List<SysWorkFlowConnection> WorkFlowConnectionList { get; set; }


    }
}
