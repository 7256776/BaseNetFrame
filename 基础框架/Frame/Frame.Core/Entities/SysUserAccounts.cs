using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frame.Core
{
    /// <summary>
 	/// 用户信息表对象
 	/// </summary>
 	[Table("SYS_USERACCOUNTS")]
    public class SysUserAccounts : FullAuditedEntity<long> 
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        [Required]
        [Column("USERCODE")]
        [StringLength(100)]
        public virtual string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>		
        [Required]
        [Column("USERNAMECN")]
        [StringLength(100)]
        public virtual string UserNameCn { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>	
        [Required]
        [Column("PASSWORD")]
        [StringLength(200)]
        public virtual string Password { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>		
        [Column("TENANTID")]
        public virtual long? TenantId { get; set; }

        /// <summary>
        /// 用户头像url
        /// </summary>		
        [Column("IMAGEURL")]
        [StringLength(100)]
        public virtual string ImageUrl { get; set; }

        /// <summary>
        /// 性别
        /// </summary>		
        [Column("SEX")]
        [StringLength(20)]
        public virtual string Sex { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>		
        [Column("EMAILADDRESS")]
        [StringLength(100)]
        public virtual string EmailAddress { get; set; }

        /// <summary>
        /// 电话
        /// </summary>		
        [Column("PHONENUMBER")]
        [StringLength(20)]
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// 最后登录日期
        /// </summary>		
        [Column("LASTLOGINTIME")]
        public virtual DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 描述
        /// </summary>		
        [Column("DESCRIPTION")]
        [StringLength(1000)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>		
        [Column("ORGCODE")]
        [StringLength(50)]
        public string OrgCode { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>		
        [Column("ISACTIVE")]
        [Required]
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 是否超级管理员
        /// </summary>		
        [Column("ISADMIN")]
        [Required]
        [DefaultValue(0)]
        public virtual bool IsAdmin { get; set; }

        /// <summary>
        /// 当前用户角色集合
        /// </summary>
        [ForeignKey("UserID")]
        public virtual ICollection<SysRoleToUser> SysRoleToUserList { get; set; }
         
    }
}
