using Abp.AutoMapper;
using NetCoreFrame.Core;
using System.Collections.Generic;

namespace NetCoreFrame.Application
{
    [AutoMap(typeof(SysMenus))]
    public class MenusOut
    {

        /// <summary>
        /// ID
        /// </summary>	
        public long Id { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>	
        public long? ParentID { get; set; }

        /// <summary>
        /// 菜单标题
        /// </summary>	 
        public string MenuDisplayName { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>	 
        public string MenuName { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public virtual string Icon { get; set; }

        /// <summary>
        /// 模块类型
        /// 1=系统模块
        /// 2=业务模块
        /// </summary>	 
        public virtual string BusinessType { get; set; }

        /// <summary>
        /// 当前菜单对象子节点菜单
        /// </summary>
        public List<MenusOut> ChildrenMenus { get; set; }

        /// <summary>
        /// 当前菜单的动作集合
        /// </summary>
        public List<MenuAction> SysMenuActions { get; set; }

        /// <summary>
        /// 菜单等级 1 -  n
        /// </summary>
        public int? MenuNodeLevel { get; set; }

        /// <summary>
        /// 菜单节点编码 例如: 1.2.3
        /// </summary>
        public string MenuNode { get; set; }
         
        /// <summary>
        /// 是否展开状态
        /// </summary>
        public bool IsExpand { get; set; }

        /// <summary>
        /// 是否叶子节点
        /// </summary>
        public bool IsLeaf { get; set; }

        /// <summary>
        /// 是否选中状态
        /// </summary>
        public bool IsCheck { get; set; }

    }
}
