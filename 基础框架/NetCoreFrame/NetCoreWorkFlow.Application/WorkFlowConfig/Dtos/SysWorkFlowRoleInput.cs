using Abp.AutoMapper;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCoreWorkFlow.Application
{
    [AutoMap(typeof(SysWorkFlowRole))]
    public class SysWorkFlowRoleInput
    {
        /// <summary>
        /// 主键ID 
        /// </summary> 
        public Guid? Id { get; set; }

        /// <summary>
        /// 流程对象ID 关联 SysWorkFlowSetting 主键ID
        /// </summary>
        [Required(ErrorMessage = "请输入流程角色名称")]
        public string FlowRoleName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>		
        public virtual string Description { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>		
        [Required]
        public virtual bool? IsActive { get; set; } = true;

    }
}
