using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// Api授权客户与资源关系对象
    /// </summary>
    public class SysApiClientToResourceData
    {
        public SysApiClientToResourceData()
        {
        }

        /// <summary>
        /// 客户ID名称
        /// </summary>
        public virtual string ClientId { get; set; }

        /// <summary>
        /// 资源服务显示名称
        /// </summary>		
        public virtual string ClientSecrets { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>	 
        public virtual string ExtensionData { get; set; }

        /// <summary>
        /// 是否使用刷新token
        /// </summary>		
        public virtual bool? AllowOfflineAccess { get; set; } = true;

        /// <summary>
        /// 刷新token有效期 单位小时
        /// </summary>		
        public virtual int? AccessTokenLifetime { get; set; } = 0;

        /// <summary>
        /// 授权token有效期 单位小时
        /// </summary>		
        public virtual int? SlidingRefreshTokenLifetime { get; set; } = 0;

        /// <summary>
        /// 客户是否激活
        /// </summary>		
        public virtual bool? IsActiveClient { get; set; }

        /// <summary>
        /// 资源别名
        /// </summary>	 
        public virtual string ResourceDisplayName { get; set; }


        /// <summary>
        /// 资源名称
        /// </summary>	 
        public virtual string ResourceName { get; set; }

        /// <summary>
        /// 资源是否激活
        /// </summary>		
        public virtual bool? IsActiveResource { get; set; }

    

    }
}
