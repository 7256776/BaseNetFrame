using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frame.Core
{
    /// <summary>
    /// Sys_Roles
    /// </summary>
    [Table("SYS_ROLETOUSER")]
    public class SysRoleToUser : Entity<long>
    {
        /// <summary>
        /// 角色ID
        /// </summary>		
        [Column("ROLEID")]
        [Required]
        public virtual long RoleID { get; set; }

        /// <summary>
        /// 模块ID
        /// </summary>		
        [Column("USERID")]
        [Required]
        public virtual long UserID { get; set; }

    }
}
