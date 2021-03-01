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
  [Table("Sys_ApiAccount")]
    public class SysApiAccount : AuditedEntity<Guid> 
    {
        public SysApiAccount()
        {
            IsActive = true;
        }

        /// <summary>
        /// 用户账号
        /// </summary>
        [Column("UserName")]
        [Description("用户账号")]
        [Required(ErrorMessage = "请输入账号名称")]
        [StringLength(100)] 
        public virtual string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>		
        [Column("Password")]
        [Description("用户密码")]
        [Required(ErrorMessage = "请输入账号密码")]
        [StringLength(100)]
        public virtual string Password { get; set; }

        /// <summary>
        /// 描述
        /// </summary>	
        [Column("Description")]
        [Description("描述")]
        [StringLength(2000)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>	
        [Column("ExtensionData")]
        [Description("扩展数据")]
        public virtual string ExtensionData { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>		
        [Column("IsActive")]
        [Description("是否启用")]
        [Required]
        public virtual bool IsActive { get; set; } = true;
         
         
    }
}
