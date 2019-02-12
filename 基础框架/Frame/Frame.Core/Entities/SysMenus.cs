using Abp.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frame.Core
{
    [Table("SYS_MENUS")]
    public class SysMenus : Entity<long>
    {

        /// <summary>
        /// 父节点ID
        /// </summary>	 
        [Column("PARENTID")]
        public virtual long? ParentID { get; set; }

        /// <summary>
        /// 菜单标题
        /// </summary>	 
        [Column("MENUDISPLAYNAME")]
        [StringLength(50)]
        public virtual string MenuDisplayName { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>	 
        [Column("MENUNAME")]
        [StringLength(50)]
        public virtual string MenuName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>	 
        [Column("DESCRIPTION")]
        [StringLength(1000)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 自定义数据
        /// </summary>	 
        [Column("CUSTOMDATA")]
        [StringLength(1000)]
        public virtual string CustomData { get; set; }

        /// <summary>
        /// 授权名称
        /// </summary>	 
        [Column("PERMISSIONNAME")]
        [StringLength(100)]
        public virtual string PermissionName { get; set; }

        /// <summary>
        /// 授权模式:1=开放模式 2=登录模式 3=授权模式
        /// </summary>	 
        [Column("REQUIRESAUTHMODEL")]
        [StringLength(10)]
        public virtual string RequiresAuthModel { get; set; }

        /// <summary>
        /// 模块地址
        /// </summary>	 
        [Column("URL")]
        [StringLength(100)]
        public virtual string Url { get; set; }

        /// <summary>
        /// 图标css
        /// </summary>	 
        [Column("ICON")]
        [StringLength(50)]
        public virtual string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>	 
        [Column("ORDERBY")]
        [DefaultValue(1)]
        public virtual int? OrderBy { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>	 
        [Column("ISACTIVE")]
        [DefaultValue(1)]
        public virtual bool? IsActive { get; set; }

        /// <summary>
        /// 当前菜单的动作集合
        /// </summary>
        [ForeignKey("MenuID")]
        public virtual ICollection<SysMenuAction> SysMenuActions { get; set; }

        /// <summary>
        /// 是否受权限控制
        /// (1=开放模式) =  false 
        /// (2=登录模式 3=授权模式) = true
        /// </summary>
        [NotMapped]
        public virtual bool IsRequiresAuth
        {
            get
            {
                if (RequiresAuthModel == "1")
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 当前菜单对象子节点菜单
        /// </summary>
        [NotMapped]
        public virtual List<SysMenus> ChildrenMenus { get; set; }

        /// <summary>
        /// 菜单等级 1 -  n
        /// </summary>
        [NotMapped]
        public virtual int? MenuNodeLevel { get; set; }

        /// <summary>
        /// 菜单节点编码 例如: 1.2.3
        /// </summary>
        [NotMapped]
        public virtual string MenuNode { get; set; }

        /// <summary>
        /// 是否叶子节点
        /// </summary>
        [NotMapped]
        public virtual bool IsLeaf { get; set; }

        /// <summary>
        /// 是否选中状态
        /// </summary>
        [NotMapped]
        public virtual bool IsCheck { get; set; }
    }
}
