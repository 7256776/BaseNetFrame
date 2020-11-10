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
    [Table("SYS_APICLIENT")]
    public class SysApiClient : AuditedEntity<Guid>
    {
        public SysApiClient()
        {
            IsActive = true;
        }

        /// <summary>
        /// 资源服务名称
        /// </summary>
        [Required(ErrorMessage = "请输入客户ID")]
        [Column("CLIENTID")]
        [StringLength(100)]
        public virtual string ClientId { get; set; }

        /// <summary>
        /// 资源服务显示名称
        /// </summary>		
        [Required(ErrorMessage = "请输入客户密钥")]
        [Column("CLIENTSECRETS")]
        [StringLength(100)]
        public virtual string ClientSecrets { get; set; }

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
        /// 是否使用刷新token
        /// </summary>		
        [Column("ALLOWOFFLINEACCESS")]
        [Required]
        public virtual bool AllowOfflineAccess { get; set; } = true;

        /// <summary>
        /// 刷新token有效期 单位秒
        /// </summary>		
        [Column("ACCESSTOKENLIFETIME")]
        [Required]
        public virtual int? AccessTokenLifetime { get; set; } = 0;

        /// <summary>
        /// 授权token有效期 单位秒
        /// </summary>		
        [Column("SLIDINGREFRESHTOKENLIFETIME")]
        [Required]
        public virtual int? SlidingRefreshTokenLifetime { get; set; } = 0;

        /// <summary>
        /// 是否激活
        /// </summary>		
        [Column("ISACTIVE")]
        [Required]
        public virtual bool IsActive { get; set; } = true;

        /// <summary>
        /// 客户相关账号对象集合
        /// </summary>
        [NotMapped]
        public virtual List<SysApiAccount> SysApiAccountList { get; set; }
    }
}
