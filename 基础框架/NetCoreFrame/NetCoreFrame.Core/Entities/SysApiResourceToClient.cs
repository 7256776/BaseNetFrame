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
    /// Api授权客户对象
    /// </summary>
    [Table("Sys_ApiResourceToClient")]
    public class SysApiResourceToClient : Entity<Guid>
    {
        public SysApiResourceToClient()
        {
        }

        /// <summary>
        /// 服务主键
        /// </summary>
        [Column("APIRESOURCEID")]
        public virtual Guid ApiResourceId { get; set; }

        /// <summary>
        /// 服务客户主键
        /// </summary>		
        [Column("APICLIENTID")]
        public virtual Guid ApiClientId { get; set; }



    }
}
