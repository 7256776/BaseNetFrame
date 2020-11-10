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
  [Table("Sys_ApiClienToAccount")]
    public class SysApiClienToAccount : Entity<Guid> 
    {
        public SysApiClienToAccount()
        {
        }

        /// <summary>
        /// 服务客户主键
        /// </summary>
        [Column("APIACCOUNTID")]
        public virtual Guid ApiAccountId { get; set; }

        /// <summary>
        /// 服务账号主键
        /// </summary>		
        [Column("APICLIENTID")]
        public virtual Guid ApiClientId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>	
        [Column("ACCOUNTSOURCE")]
        [StringLength(10)]
        public virtual string AccountSource { get; set; }

         
         
    }
}
