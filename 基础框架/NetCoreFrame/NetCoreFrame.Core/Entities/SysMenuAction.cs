using Abp.Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    //Sys_MenuAction
    [Table("Sys_MenuAction")]
    public class SysMenuAction : Entity<long>
    {
        /// <summary>
        /// 模块主键
        /// </summary>	 
        [Column("MenuID")]
        [Description("模块主键")]
        [Required(ErrorMessage = "未设置模块主键")]
        public virtual long MenuID { get; set; }

        /// <summary>
        /// 菜单标题
        /// </summary>	 
        [Column("ActionDisplayName")]
        [Description("菜单标题")]
        [StringLength(50)]
        public virtual string ActionDisplayName { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>	 
        [Column("ActionName")]
        [Description("菜单名称")]
        [StringLength(50)]
        public virtual string ActionName { get; set; }

        /// <summary>
        /// 授权名称
        /// </summary>	 
        [Column("PermissionName")]
        [Description("授权名称")]
        [StringLength(100)]
        public virtual string PermissionName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>	 
        [Column("Description")]
        [Description("描述")]
        [StringLength(2000)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 授权模式:1=开放模式 2=登录模式 3=登录模式
        /// </summary>	 
        [Column("RequiresAuthModel")]
        [Description("授权模式:1=开放模式 2=登录模式 3=登录模式")]
        [StringLength(10)]
        public virtual string RequiresAuthModel { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>	 
        [Column("IsActive")]
        [DefaultValue(1)]
        public virtual bool? IsActive { get; set; }

        [NotMapped]
        public virtual bool IsCheck { get; set; }

    }
}
