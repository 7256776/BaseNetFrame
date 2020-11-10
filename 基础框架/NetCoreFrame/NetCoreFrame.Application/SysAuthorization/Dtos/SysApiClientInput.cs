using Abp.AutoMapper;
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
    [AutoMap(typeof(SysApiClient))]
    public class SysApiClientInput 
    {
        public SysApiClientInput()
        {
            IsActive = true;
        }

        /// <summary>
        /// ID
        /// </summary>	
        public Guid? Id { get; set; }

        /// <summary>
        /// 资源服务名称
        /// </summary>
        [Required(ErrorMessage = "请输入客户ID")]
        [StringLength(100,ErrorMessage = "客户ID长度超过100") ]
        public virtual string ClientId { get; set; }

        /// <summary>
        /// 资源服务显示名称
        /// </summary>		
        [Required(ErrorMessage = "请输入客户密钥")]
        [StringLength(100, ErrorMessage = "客户密钥长度超过100")]
        public virtual string ClientSecrets { get; set; }

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
        /// 资源ID
        /// </summary>	 
        public virtual string ApiResourceId { get; set; }

        /// <summary>
        /// 是否产生刷新token
        /// </summary>		
        [Required(ErrorMessage = "请设置是否产生刷新")]
        public virtual bool AllowOfflineAccess { get; set; } = true;

        /// <summary>
        /// 刷新token有效期 单位秒
        /// </summary>		
        public virtual int? AccessTokenLifetime { get; set; } = 0;

        /// <summary>
        /// 授权token有效期 单位秒
        /// </summary>		
        [Required(ErrorMessage = "请设置授权token有效期")]
        public virtual int? SlidingRefreshTokenLifetime { get; set; } = 0;

        /// <summary>
        /// 是否激活
        /// </summary>		
        [Required]
        public virtual bool IsActive { get; set; } = true;
         
         
    }
}
