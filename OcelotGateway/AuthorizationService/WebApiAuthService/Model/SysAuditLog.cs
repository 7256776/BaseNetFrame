using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAuthService
{
    /// <summary>
    /// Used to store audit logs.
    /// </summary>
    [Table("SYS_AUDITLOGS")]
    public class SysAuditLog
    {

        /// <summary>
        /// </summary>
        [Key]
        [Column("ID")]
        public virtual long? Id { get; set; }

        /// <summary>
        /// 租户id.
        /// </summary>
        [Column("TENANTID")]
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// 用户id.
        /// </summary>
        [Column("USERID")]
        public virtual long? UserId { get; set; }

        /// <summary>
        /// 服务(类/接口)的名字
        /// </summary>
        [Column("SERVICENAME")]
        public virtual string ServiceName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        [Column("METHODNAME")]
        public virtual string MethodName { get; set; }

        /// <summary>
        /// 参数.
        /// </summary>
        [Column("PARAMETERS")]
        public virtual string Parameters { get; set; }

        /// <summary>
        /// 方法执行的开始时间
        /// </summary>
        [Column("EXECUTIONTIME")]
        public virtual DateTime ExecutionTime { get; set; }

        /// <summary>
        /// 调用时长.
        /// </summary>
        [Column("EXECUTIONDURATION")]
        public virtual int ExecutionDuration { get; set; }

        /// <summary>
        /// IP 地址
        /// </summary>
        [Column("CLIENTIPADDRESS")]
        public virtual string ClientIpAddress { get; set; }

        /// <summary>
        /// 客户端名称.(一般计算机名称)
        /// </summary>
        [Column("CLIENTNAME")]
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 浏览器
        /// </summary>
        [Column("BROWSERINFO")]
        public virtual string BrowserInfo { get; set; }

        /// <summary>
        /// 异常对象.
        /// </summary>
        [Column("EXCEPTION")]
        public virtual string Exception { get; set; }

        /// <summary>
        /// 用户id
        /// <see cref="AuditInfo.ImpersonatorUserId"/>.
        /// </summary>
        [Column("IMPERSONATORUSERID")]
        public virtual long? ImpersonatorUserId { get; set; }

        /// <summary>
        /// 租户id
        /// <see cref="AuditInfo.ImpersonatorTenantId"/>.
        /// </summary>
        [Column("IMPERSONATORTENANTID")]
        public virtual int? ImpersonatorTenantId { get; set; }

        /// <summary>
        /// 自定义数据
        /// <see cref="AuditInfo.CustomData"/>.
        /// </summary>
        [Column("CUSTOMDATA")]
        public virtual string CustomData { get; set; }

 
    }
}