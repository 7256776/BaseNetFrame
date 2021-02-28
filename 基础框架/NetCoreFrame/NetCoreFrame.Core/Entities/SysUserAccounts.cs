using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    /// <summary>
 	/// 用户信息表对象
 	/// </summary>
 	[Table("Sys_UserAccounts")]
    public class SysUserAccounts : FullAuditedEntity<long> 
    {
        protected SysUserAccounts()
        {
            IsActive = true;
        }

        /// <summary>
        /// 用户账号
        /// </summary>
        [Column("UserCode")]
        [Description("用户账号")]
        [Required(ErrorMessage = "请输入用户账号")]
        [StringLength(100)] 
        public virtual string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>		
        [Column("UserNameCn")]
        [Description("用户名称")]
        [Required(ErrorMessage = "请输入用户名称")]
        [StringLength(100)]
        public virtual string UserNameCn { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>	
        [Column("Password")]
        [Description("用户密码")]
        [Required(ErrorMessage = "请输入用户密码")]
        [StringLength(200)]
        public virtual string Password { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>		
        [Column("TenantId")]
        [Description("租户ID")]
        public virtual long? TenantId { get; set; }

        /// <summary>
        /// 用户头像url
        /// </summary>		
        [Column("ImageUrl")]
        [Description("用户头像url")]
        [StringLength(100)]
        public virtual string ImageUrl { get; set; }

        /// <summary>
        /// 性别
        /// </summary>		
        [Column("Sex")]
        [Description("性别")]
        [StringLength(20)]
        public virtual string Sex { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>		
        [Column("EmailAddress")]
        [Description("邮箱地址")]
        [StringLength(100)]
        public virtual string EmailAddress { get; set; }

        /// <summary>
        /// 电话
        /// </summary>		
        [Column("PhoneNumber")]
        [Description("电话")]
        [StringLength(20)]
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// 最后登录日期
        /// </summary>		
        [Column("LastLoginTime")]
        [Description("最后登录日期")]
        public virtual DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 描述
        /// </summary>		
        [Column("Description")]
        [Description("描述")]
        [StringLength(2000)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>		
        [Column("OrgCode")]
        [Description("所属部门")]
        [StringLength(50)]
        public string OrgCode { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>		
        [Column("IsActive")]
        [Description("是否激活")]
        [Required]
        public virtual bool IsActive { get; set; } = true;

        /// <summary>
        /// 是否超级管理员
        /// </summary>		
        [Column("IsAdmin")]
        [Description("是否超级管理员 0=否 1=是")]
        [Required]
        [DefaultValue(0)]
        public virtual bool IsAdmin { get; set; } = false;

        /// <summary>
        /// 当前用户角色集合
        /// </summary>
        [ForeignKey("UserID")]
        public virtual ICollection<SysRoleToUser> SysRoleToUserList { get; set; }
         
    }
}
