﻿using Abp.AutoMapper;
using Frame.Core;
using System.ComponentModel.DataAnnotations;

namespace Frame.Application
{
    [AutoMap(typeof(SysMenuAction))]
    public class MenuAction
    {

        /// <summary>
        /// 主键
        /// </summary>	 
        public long? ID { get; set; }

        /// <summary>
        /// 模块主键
        /// </summary>	 
        public long? MenuID { get; set; }

        /// <summary>
        /// 动作标题
        /// </summary>	 
        [Required(ErrorMessage = "必填")]
        public string ActionDisplayName { get; set; }

        /// <summary>
        /// 动作名称
        /// </summary>	 
        [Required(ErrorMessage = "必填")]
        public string ActionName { get; set; }

        /// <summary>
        /// 授权名称
        /// </summary>	 
        public string PermissionName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>	 
        public string Description { get; set; }

        /// <summary>
        /// 授权模式:1=开放模式 2=登录模式 3=登录模式
        /// </summary>	 
        public string RequiresAuthModel { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>	 
        public bool? IsActive { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public  bool IsCheck { get; set; }

    }
}
