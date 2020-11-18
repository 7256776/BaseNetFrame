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
    public class SysApiResourceData
    {
        public SysApiResourceData()
        {
        }

        /// <summary>
        /// ID
        /// </summary>	
        public Guid? Id { get; set; }

        /// <summary>
        /// 资源服务名称
        /// </summary>
        public virtual string ResourceName { get; set; }

        /// <summary>
        /// 资源服务显示名称
        /// </summary>		
        public virtual string ResourceDisplayName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>	
        public virtual string Description { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>	
        public virtual string ExtensionData { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>		
        public virtual bool? IsActive { get; set; }

        /// <summary>
        /// 资源相关客户对象集合
        /// </summary>
        public virtual List<SysApiClient> SysApiClientList { get; set; }

    }
}
