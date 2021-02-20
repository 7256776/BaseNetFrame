using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Application
{
    /// <summary>
 	/// Api授权资源对象
 	/// </summary>
    [AutoMap(typeof(SysApiResource))]
    public class SysApiResourceInput  
    {
        public SysApiResourceInput()
        {
        }

        /// <summary>
        /// ID
        /// </summary>	
        public Guid? Id { get; set; }

        /// <summary>
        /// 资源服务名称
        /// </summary>
        [Required(ErrorMessage = "请输入资源服务名称")]
        [StringLength(100, ErrorMessage = "资源服务名称长度超过100")]
        public virtual string ResourceName { get; set; }

        /// <summary>
        /// 资源服务显示名称
        /// </summary>		
        [Required(ErrorMessage = "请输入资源服务显示名称")]
        [StringLength(100, ErrorMessage = "资源服务显示名称长度超过100")]
        public virtual string ResourceDisplayName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>	
        [StringLength(1000)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>	
        [StringLength(4000)]
        public virtual string ExtensionData { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>		
        [Required]
        public virtual bool IsActive { get; set; } = true;
         
         
    }
}
