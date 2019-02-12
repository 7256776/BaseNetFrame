using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
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
        [Required(ErrorMessage = "请设置角色ID")]
        public virtual long RoleID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>		
        [Column("USERID")]
        [Required(ErrorMessage = "请设置用户ID")]
        public virtual long UserID { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        [NotMapped]
        public virtual string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [NotMapped]
        public virtual string UserName { get; set; }
    }
}
