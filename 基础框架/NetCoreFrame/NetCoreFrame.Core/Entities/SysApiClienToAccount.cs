using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{ 
  /// <summary>
  /// Api授权客户与账号关系对象
  /// </summary>
  [Table("Sys_ApiClientoaccount")]
    public class SysApiClienToAccount : Entity<Guid> 
    {
        public SysApiClienToAccount()
        {
        }

        /// <summary>
        /// 授权账号ID主键
        /// </summary>
        [Column("ApiAccountId")]
        [Description("授权账号ID主键 关联 Sys_ApiAccount")]
        public virtual Guid ApiAccountId { get; set; }

        /// <summary>
        /// 授权客户ID主键
        /// </summary>		
        [Column("ApiClientId")]
        [Description("授权客户ID主键 关联 Sys_ApiClient")]
        public virtual Guid ApiClientId { get; set; }

        /// <summary>
        /// 账号来源(用于扩展)
        /// 1=来自系统账号管理
        /// 2=来自服务授权账号管理
        /// </summary>	
        [Column("AccountSource")]
        [Description("账号来源(用于扩展) 1=来自系统账号管理 2=来自服务授权账号管理")]
        [StringLength(10)]
        public virtual string AccountSource { get; set; }

         
         
    }
}
