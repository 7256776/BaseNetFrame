using Abp.Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// Sys_Roles
    /// </summary>
    [Table("Sys_RoleToUser")]
    public class SysRoleToUser : Entity<long>
    {
        /// <summary>
        /// 角色ID
        /// </summary>		
        [Column("RoleID")]
        [Description("角色ID")]
        [Required(ErrorMessage = "请设置角色ID")]
        public virtual long RoleID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>		
        [Column("UserID")]
        [Description("用户ID")]
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
