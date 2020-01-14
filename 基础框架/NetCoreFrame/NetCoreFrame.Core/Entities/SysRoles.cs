using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    /// <summary>
 	/// Sys_Roles
 	/// </summary>
 	[Table("SYS_ROLES")]
    public class SysRoles : AuditedEntity<long>
    {
        public SysRoles()
        {
            IsActive = true;
        }

        /// <summary>
        /// 租户ID
        /// </summary>		
        [Column("TENANTID")]
        public virtual long? TenantId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>		
        [Column("DESCRIPTION")]
        [StringLength(1000)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>		
        [Column("ROLENAME")]
        [Required(ErrorMessage = "请输入角色名称")]
        [StringLength(50)]
        public virtual string RoleName { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>		
        [Column("ISACTIVE")]
        [DefaultValue(true)]
        [Required]
        public virtual bool? IsActive { get; set; } = true;

        /// <summary>
        /// 当前用户角色集合
        /// </summary>
        [ForeignKey("RoleID")]
        public virtual ICollection<SysRoleToUser> SysRoleToUserList { get; set; }

        

    } 
}
