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
    public class SysApiClienToAccountData  
    {
        public SysApiClienToAccountData()
        {
        }

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
        public virtual string ExtensionAccountData { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>		
        public virtual bool? IsActiveAccount { get; set; } = true;


        /// <summary>
        /// 客户ID
        /// </summary>
        public virtual string ClientId { get; set; }

        /// <summary>
        /// 客户秘钥
        /// </summary>		
        public virtual string ClientSecrets { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>	 
        public virtual string ExtensionClientData { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>		
        public virtual bool? IsActiveClient { get; set; } = true;

    }
}
