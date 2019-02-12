using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frame.Core
{
    /// <summary>
 	/// Sys_Roles
 	/// </summary>
 	[Table("SYS_ROLETOMENUACTION")]
    public class SysRoleToMenuAction : Entity<long>
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
        [Column("MENUID")]
        [Required]
        public virtual long MenuID { get; set; }

        /// <summary>
        /// 动作ID
        /// </summary>		
        [Column("MENUACTIONID")]
        public virtual long? MenuActionID { get; set; }

        /// <summary>
        /// 是否属于模块授权
        /// true 模块授权
        /// false 动作授权
        /// </summary>		
        [Column("ISMENU")]
        public virtual bool IsMenu { get; set; }

      

    }


}
