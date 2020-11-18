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
    /// Api授权客户对象
    /// </summary>
    [AutoMap(typeof(SysApiClient))]
    public class SysApiClientData
    {
        public SysApiClientData()
        {
        }

        /// <summary>
        /// ID
        /// </summary>	
        public Guid? Id { get; set; }

        /// <summary>
        /// 资源服务名称
        /// </summary>
        public virtual string ClientId { get; set; }

        /// <summary>
        /// 资源服务显示名称
        /// </summary>		
        public virtual string ClientSecrets { get; set; }

        /// <summary>
        /// 描述
        /// </summary>	 
        public virtual string Description { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>	 
        public virtual string ExtensionData { get; set; }

        /// <summary>
        /// 关联资源
        /// </summary>	 
        public virtual Guid? ApiResourceId { get; set; }

        /// <summary>
        /// 是否使用刷新token
        /// </summary>		
        public virtual bool AllowOfflineAccess { get; set; } = true;

        /// <summary>
        /// 刷新token有效期 单位秒
        /// </summary>		
        public virtual int? AccessTokenLifetime { get; set; } = 0;

        /// <summary>
        /// 授权token有效期 单位秒
        /// </summary>		
        public virtual int? SlidingRefreshTokenLifetime { get; set; } = 0;
         
        /// <summary>
        /// 是否激活
        /// </summary>		
        public virtual bool? IsActive { get; set; }

        /// <summary>
        /// 客户相关账号对象集合
        /// </summary>
        public virtual List<SysApiAccountData> SysApiAccountList { get; set; }



    }
}
