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
    [AutoMap(typeof(SysApiAccount))]
    public class SysApiAccountData
    {
        public SysApiAccountData()
        {
        }

        /// <summary>
        /// ID
        /// </summary>	
        public Guid? Id { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>		
        public virtual string Password { get; set; }

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
        public virtual bool IsActive { get; set; } = true;

        /// <summary>
        /// 账号关联客户
        /// </summary>	
        public virtual Guid? ApiClientId { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>		
        public virtual DateTime? CreationTime { get; set; } 

    }
}
