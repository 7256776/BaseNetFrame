using Abp.AutoMapper;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCoreWorkFlow.Application
{
    [AutoMap(typeof(SysWorkFlowEndpoint))]
    public class SysWorkFlowEndpointInput
    {
        /// <summary>
        /// 流程对象ID 关联 SysWorkFlowSetting 主键ID
        /// </summary>
        [StringLength(50)]
        public Guid? WorkFlowSettingID { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "请输入节点UID")]
        public string UID { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "请输入节点名称")]
        public string EndpointText { get; set; }

        /// <summary>
        /// S=开始
        /// E=结束
        /// P=流程
        /// 节点类型
        /// </summary>
        [Required(ErrorMessage = "请输入节点类型")]
        public string EndpointType { get; set; }

        /// <summary>
        /// 坐标 Top 或 Y
        /// </summary>	 
        public int OffsetTop { get; set; }

        /// <summary>
        /// 坐标 Top 或 Y
        /// </summary>	 
        public int OffsetLeft { get; set; }

    }

}
