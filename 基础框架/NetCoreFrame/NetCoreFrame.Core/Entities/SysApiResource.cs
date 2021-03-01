using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    /// <summary>
 	/// Api授权资源对象
 	/// </summary>
 	[Table("Sys_ApiResource")]
    public class SysApiResource : AuditedEntity<Guid> 
    {
        public SysApiResource()
        {
            IsActive = true;
        }

        /// <summary>
        /// 资源服务名称
        /// </summary>
        [Column("ResourceName")]
        [Description("资源服务名称")]
        [Required(ErrorMessage = "请输入资源服务名称")]
        [StringLength(100)]
        public virtual string ResourceName { get; set; }

        /// <summary>
        /// 资源服务显示名称
        /// </summary>		
        [Column("ResourceDisplayName")]
        [Description("资源服务显示名称")]
        [Required(ErrorMessage = "请输入资源服务显示名称")]
        [StringLength(100)]
        public virtual string ResourceDisplayName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>	
        [Column("Description")]
        [Description("描述")]
        [StringLength(2000)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>	
        [Column("ExtensionData")]
        [Description("扩展数据")]
        public virtual string ExtensionData { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>		
        [Column("IsActive")]
        [Description("是否激活")]
        [Required]
        public virtual bool IsActive { get; set; } = true;

       

    }
}
