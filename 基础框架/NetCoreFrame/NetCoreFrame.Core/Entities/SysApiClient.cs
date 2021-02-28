using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{

    /// <summary>
    /// Api授权客户对象
    /// </summary>
    [Table("Sys_ApiClient")]
    public class SysApiClient : AuditedEntity<Guid>
    {
        public SysApiClient()
        {
            IsActive = true;
        }

        /// <summary>
        /// 客户ID
        /// </summary>
        [Column("ClientId")]
        [Description("客户ID")]
        [Required(ErrorMessage = "请输入客户ID")]
        [StringLength(100)]
        public virtual string ClientId { get; set; }

        /// <summary>
        /// 客户秘钥
        /// </summary>		
        [Required(ErrorMessage = "请输入客户密钥")]
        [Column("ClientSecrets")]
        [Description("客户密钥")]
        [StringLength(100)]
        public virtual string ClientSecrets { get; set; }

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
        /// 是否使用刷新token
        /// </summary>		
        [Column("AllowOfflineAccess")]
        [Description("是否使用刷新token")]
        [Required]
        public virtual bool AllowOfflineAccess { get; set; } = true;

        /// <summary>
        /// 授权token有效期 单位小时
        /// </summary>		
        [Column("AccessTokenLifetime")]
        [Description("授权token有效期 单位小时")]
        [Required]
        [DefaultValue(0)]
        public virtual int? AccessTokenLifetime { get; set; } = 0;

        /// <summary>
        /// 刷新token有效期 单位小时
        /// </summary>		
        [Column("SlidingRefreshTokenLifetime")]
        [Description("刷新token有效期 单位小时")]
        [DefaultValue(0)]
        public virtual int? SlidingRefreshTokenLifetime { get; set; } = 0;

        /// <summary>
        /// 是否激活
        /// </summary>		
        [Column("IsActive")]
        [Description("是否激活")]
        [Required]
        public virtual bool IsActive { get; set; } = true;


    }
}
