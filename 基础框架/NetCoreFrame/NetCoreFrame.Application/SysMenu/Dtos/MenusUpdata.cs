﻿using Abp.AutoMapper;
using NetCoreFrame.Core;
using System.ComponentModel.DataAnnotations;

namespace NetCoreFrame.Application
{
    [AutoMap(typeof(SysMenus),typeof(MenusInput))]
    public class MenusUpdata
    {
        /// <summary>
        /// ID
        /// </summary>	
        public long? Id { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>	
        public long? ParentID { get; set; }

        /// <summary>
        /// 菜单标题
        /// </summary>	 
        [Required(ErrorMessage = "请输入菜单标题")]
        public string MenuDisplayName { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>	 
        [Required(ErrorMessage = "请输入菜单名称")]
        public string MenuName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>	 
        public string Description { get; set; }

        /// <summary>
        /// 自定义数据
        /// </summary>	 
        public string CustomData { get; set; }

        /// <summary>
        /// 授权名称
        /// </summary>	 
        public string PermissionName { get; set; }

        /// <summary>
        /// 授权模式:1=开放模式 2=登录模式 3=登录模式
        /// </summary>	 
        [Required(ErrorMessage = "请输入授权模式")]
        public string RequiresAuthModel { get; set; }

        /// <summary>
        /// 模块地址
        /// </summary>	 
        public string Url { get; set; }

        /// <summary>
        /// 图标css
        /// </summary>	 
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>	 
        public int? OrderBy { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>	 
        [Required(ErrorMessage = "必填")]
        public bool IsActive { get; set; }



    }
}
