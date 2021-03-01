using Abp.Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    /// <summary>
 	/// Sys_Roles
 	/// </summary>
 	[Table("Sys_RoleToMenuAction")]
    public class SysRoleToMenuAction : Entity<long>
    {
        /// <summary>
        /// 角色ID
        /// </summary>		
        [Column("RoleID")]
        [Description("角色ID")]
        [Required(ErrorMessage = "请设置角色ID")]
        public virtual long RoleID { get; set; }

        /// <summary>
        /// 模块ID
        /// </summary>		
        [Column("MenuID")]
        [Description("模块ID")]
        [Required(ErrorMessage = "请设置模块ID")]
        public virtual long MenuID { get; set; }

        /// <summary>
        /// 动作ID
        /// </summary>		
        [Column("MenuActionID")]
        [Description("动作ID")]
        public virtual long? MenuActionID { get; set; }

        /// <summary>
        /// 是否属于模块授权
        /// true 模块授权
        /// false 动作授权
        /// </summary>		
        [Column("IsMenu")]
        [Description("是否属于模块授权 true=模块授权 false=动作授权")]
        public virtual bool IsMenu { get; set; }

      

    }


}
