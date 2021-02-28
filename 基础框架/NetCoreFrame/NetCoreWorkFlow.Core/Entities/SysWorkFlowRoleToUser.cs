using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreWorkFlow.Core
{
    /// <summary>
    /// 审核角色
    /// </summary>
    [Table("Sys_WorkFlowRoleToUser")]
    public class SysWorkFlowRoleToUser : Entity<Guid>
    {
        /// <summary>
        /// 角色ID Sys_WorkFlowRole
        /// </summary>		
        [Column("FlowRoleID")]
        [Description("流程角色ID 关联 Sys_WorkFlowRole主键ID")]
        [Required(ErrorMessage = "请设置流程角色ID")]
        public virtual Guid FlowRoleID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>		
        [Column("UserID")]
        [Description("用户ID")]
        [Required(ErrorMessage = "请设置用户ID")]
        public virtual string UserID { get; set; }


    }
}
