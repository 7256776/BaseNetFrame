using Abp.AutoMapper;
using System;

namespace NetCoreWorkFlow.Application
{
    public class SysFlowBusinessModule
    {
        /// <summary>
        /// ID
        /// </summary>	
        public string MenuId { get; set; }

        /// <summary>
        /// 上级节点主键
        /// </summary>
        public virtual string ParentID { get; set; }

        /// <summary>
        /// 模块显示名称
        /// </summary>
        public virtual string MenuDisplayName { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public virtual string MenuName { get; set; }

        /// <summary>
        /// 图标样式
        /// </summary>
        public virtual string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int? OrderBy { get; set; }


    }
}
