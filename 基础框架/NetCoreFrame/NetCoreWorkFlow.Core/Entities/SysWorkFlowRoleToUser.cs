using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreWorkFlow.Core
{
    /// <summary>
    /// 审核角色
    /// </summary>
    [Table("SYS_WORKFLOWROLETOUSER")]
    public class SysWorkFlowRoleToUser : Entity<Guid>
    {
        /// <summary>
        /// 角色ID
        /// </summary>		
        [Column("FlowRoleID")]
        [Required(ErrorMessage = "请设置流程角色ID")]
        public virtual Guid FlowRoleID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>		
        [Column("USERID")]
        [Required(ErrorMessage = "请设置用户ID")]
        public virtual string UserID { get; set; }


    }
}
