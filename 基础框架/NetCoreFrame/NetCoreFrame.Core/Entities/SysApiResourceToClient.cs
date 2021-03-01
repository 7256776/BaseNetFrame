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
    /// Api资源授权客户对象
    /// </summary>
    [Table("Sys_ApiResourceToClient")]
    public class SysApiResourceToClient : Entity<Guid>
    {
        public SysApiResourceToClient()
        {
        }

        /// <summary>
        /// 资源ID主键
        /// </summary>
        [Column("ApiResourceId")]
        [Description("资源ID主键 关联 Sys_ApiResource")]
        public virtual Guid ApiResourceId { get; set; }

        /// <summary>
        /// 授权客户ID主键
        /// </summary>		
        [Column("ApiClientId")]
        [Description("授权客户ID主键 关联 Sys_ApiClient")]
        public virtual Guid ApiClientId { get; set; }

         
    }
}
