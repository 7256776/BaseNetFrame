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
 	[Table("SYS_APIRESOURCE")]
    public class SysApiResource : AuditedEntity<Guid> 
    {
        public SysApiResource()
        {
            IsActive = true;
        }

        /// <summary>
        /// 资源服务名称
        /// </summary>
        [Required(ErrorMessage = "请输入资源服务名称")]
        [Column("RESOURCENAME")]
        [StringLength(100)]
        public virtual string ResourceName { get; set; }

        /// <summary>
        /// 资源服务显示名称
        /// </summary>		
        [Required(ErrorMessage = "请输入资源服务显示名称")]
        [Column("RESOURCEDISPLAYNAME")]
        [StringLength(100)]
        public virtual string ResourceDisplayName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>	
        [Column("DESCRIPTION")]
        [StringLength(1000)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>	
        [Column("ExtensionData")]
        [StringLength(4000)]
        public virtual string ExtensionData { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>		
        [Column("ISACTIVE")]
        [Required]
        public virtual bool IsActive { get; set; } = true;

       

    }
}
