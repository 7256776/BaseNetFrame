using Abp.Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    //Sys_MenuAction
    [Table("SYS_MENUACTION")]
    public class SysMenuAction : Entity<long>
    {
        /// <summary>
        /// 模块主键
        /// </summary>	 
        [Column("MENUID")]
        [Required(ErrorMessage = "未设置模块主键")]
        public virtual long MenuID { get; set; }

        /// <summary>
        /// 菜单标题
        /// </summary>	 
        [Column("ACTIONDISPLAYNAME")]
        [StringLength(50)]
        public virtual string ActionDisplayName { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>	 
        [Column("ACTIONNAME")]
        [StringLength(50)]
        public virtual string ActionName { get; set; }

        /// <summary>
        /// 授权名称
        /// </summary>	 
        [Column("PERMISSIONNAME")]
        [StringLength(100)]
        public virtual string PermissionName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>	 
        [Column("DESCRIPTION")]
        [StringLength(1000)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 授权模式:1=开放模式 2=登录模式 3=登录模式
        /// </summary>	 
        [Column("REQUIRESAUTHMODEL")]
        [StringLength(10)]
        public virtual string RequiresAuthModel { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>	 
        [Column("ISACTIVE")]
        [DefaultValue(1)]
        public virtual bool? IsActive { get; set; }

        [NotMapped]
        public virtual bool IsCheck { get; set; }

    }
}
