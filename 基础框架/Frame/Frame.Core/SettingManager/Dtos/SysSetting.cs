using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frame.Core
{
    /// <summary>
    /// Represents a setting for a tenant or user.
    /// </summary>
    [Table("SYS_SETTINGS")]
    public class SysSetting : AuditedEntity<Guid>//, IMayHaveTenant
    {
        /// <summary>
        /// 租户id
        /// </summary>
        [Column("TENANTID")]
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [Column("USERID")]
        public virtual long? UserId { get; set; }

        /// <summary>
        /// 设置的名称.
        /// </summary>
        [Required]
        [MaxLength(256)]
        [Column("NAME")]
        public virtual string Name { get; set; }

        /// <summary>
        /// 设置的值
        /// </summary>
        [Column("VALUE")]
        public virtual string Value { get; set; }

        /// <summary>
        /// Creates a new <see cref="Setting"/> object.
        /// </summary>
        public SysSetting()
        {

        }

        /// <summary>
        /// Creates a new <see cref="Setting"/> object.
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <param name="userId">用户id</param>
        /// <param name="name">设置的名称</param>
        /// <param name="value">设置的值</param>
        public SysSetting(int? tenantId, long? userId, string name, string value)
        {
            TenantId = tenantId;
            UserId = userId;
            Name = name;
            Value = value;
        }
    }
}
