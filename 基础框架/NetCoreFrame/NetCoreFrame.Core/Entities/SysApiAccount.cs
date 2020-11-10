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
  [Table("SYS_APIACCOUNT")]
    public class SysApiAccount : AuditedEntity<Guid> 
    {
        public SysApiAccount()
        {
            IsActive = true;
        }

        /// <summary>
        /// 用户账号
        /// </summary>
        [Required(ErrorMessage = "请输入账号名称")]
        [Column("UserName")]
        [StringLength(100)]
        public virtual string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>		
        [Required(ErrorMessage = "请输入账号密码")]
        [Column("Password")]
        [StringLength(100)]
        public virtual string Password { get; set; }

        /// <summary>
        /// 描述
        /// </summary>	
        [Column("DESCRIPTION")]
        [StringLength(1000)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>	
        [Column("ExtensionData")]
        [StringLength(4000)]
        public virtual string ExtensionData { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>		
        [Column("ISACTIVE")]
        [Required]
        public virtual bool IsActive { get; set; } = true;
         
         
    }
}
