using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Extensions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// Used to store audit logs.
    /// </summary>
    [Table("Sys_AuditLog")]
    public class SysAuditLog : Entity<long>
    {
        #region 设置字符串长度
        /// <summary>
        /// Maximum length of <see cref="ServiceName"/> property.
        /// </summary>
        public static int MaxServiceNameLength = 256;

        /// <summary>
        /// Maximum length of <see cref="MethodName"/> property.
        /// </summary>
        public static int MaxMethodNameLength = 256;

        /// <summary>
        /// Maximum length of <see cref="Parameters"/> property.
        /// </summary>
        public static int MaxParametersLength = 1024;

        /// <summary>
        /// Maximum length of <see cref="ClientIpAddress"/> property.
        /// </summary>
        public static int MaxClientIpAddressLength = 64;

        /// <summary>
        /// Maximum length of <see cref="ClientName"/> property.
        /// </summary>
        public static int MaxClientNameLength = 128;

        /// <summary>
        /// Maximum length of <see cref="BrowserInfo"/> property.
        /// </summary>
        public static int MaxBrowserInfoLength = 256;

        /// <summary>
        /// Maximum length of <see cref="Exception"/> property.
        /// </summary>
        public static int MaxExceptionLength = 2000;

        /// <summary>
        /// Maximum length of <see cref="CustomData"/> property.
        /// </summary>
        public static int MaxCustomDataLength = 2000;
        #endregion

        /// <summary>
        /// 租户id.
        /// </summary>
        [Column("TenantId")]
        [Description("租户id")]
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// 用户id.
        /// </summary>
        [Column("UserId")]
        [Description("用户id")]
        public virtual long? UserId { get; set; }

        /// <summary>
        /// 服务(类/接口)的名字
        /// </summary>
        [Column("ServiceName")]
        [Description("服务(类/接口)的名字")]
        [StringLength(256)]
        public virtual string ServiceName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        [Column("MethodName")]
        [Description("方法名称")]
        [StringLength(256)]
        public virtual string MethodName { get; set; }

        /// <summary>
        /// 参数.
        /// </summary>
        [Column("Parameters")]
        [Description("参数")]
        [StringLength(1024)]
        public virtual string Parameters { get; set; }

        /// <summary>
        /// 方法执行的开始时间
        /// </summary>
        [Column("ExecutionTime")]
        [Description("方法执行的开始时间")]
        public virtual DateTime ExecutionTime { get; set; }

        /// <summary>
        /// 调用时长.
        /// </summary>
        [Column("ExecutionDuration")]
        [Description("调用时长")]
        public virtual int ExecutionDuration { get; set; }

        /// <summary>
        /// IP 地址
        /// </summary>
        [Column("ClientIpAddress")]
        [Description("IP 地址")]
        [StringLength(64)]
        public virtual string ClientIpAddress { get; set; }

        /// <summary>
        /// 客户端名称.(一般计算机名称)
        /// </summary>
        [Column("ClientName")]
        [Description("客户端名称.(一般计算机名称)")]
        [StringLength(128)]
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 浏览器
        /// </summary>
        [Column("BrowserInfo")]
        [Description("浏览器")]
        [StringLength(256)]
        public virtual string BrowserInfo { get; set; }

        /// <summary>
        /// 异常对象.
        /// </summary>
        [Column("Exception")]
        [Description("异常对象")]
        [StringLength(2000)]
        public virtual string Exception { get; set; }

        /// <summary>
        /// 用户id
        /// <see cref="AuditInfo.ImpersonatorUserId"/>.
        /// </summary>
        [Column("ImpersonatorUserId")]
        [Description("用户id")]
        public virtual long? ImpersonatorUserId { get; set; }

        /// <summary>
        /// 租户id
        /// <see cref="AuditInfo.ImpersonatorTenantId"/>.
        /// </summary>
        [Column("ImpersonatorTenantId")]
        [Description("租户id")]
        public virtual int? ImpersonatorTenantId { get; set; }

        /// <summary>
        /// 自定义数据
        /// <see cref="AuditInfo.CustomData"/>.
        /// </summary>
        [Column("CustomData")]
        [Description("自定义数据")]
        [StringLength(2000)]
        public virtual string CustomData { get; set; }

        /// <summary>
        /// Creates a new CreateFromAuditInfo from given <see cref="auditInfo"/>.
        /// </summary>
        /// <param name="auditInfo">Source <see cref="AuditInfo"/> object</param>
        /// <returns>The <see cref="AuditLog"/> object that is created using <see cref="auditInfo"/></returns>
        public static SysAuditLog CreateFromAuditInfo(AuditInfo auditInfo)
        {
            var exceptionMessage = auditInfo.Exception != null ? auditInfo.Exception.ToString() : null;
            return new SysAuditLog
            {
                TenantId = auditInfo.TenantId,
                UserId = auditInfo.UserId,
                ServiceName = auditInfo.ServiceName.TruncateWithPostfix(MaxServiceNameLength),
                MethodName = auditInfo.MethodName.TruncateWithPostfix(MaxMethodNameLength),
                Parameters = auditInfo.Parameters.TruncateWithPostfix(MaxParametersLength),
                ExecutionTime = auditInfo.ExecutionTime,
                ExecutionDuration = auditInfo.ExecutionDuration,
                ClientIpAddress = auditInfo.ClientIpAddress.TruncateWithPostfix(MaxClientIpAddressLength),
                ClientName = auditInfo.ClientName.TruncateWithPostfix(MaxClientNameLength),
                BrowserInfo = auditInfo.BrowserInfo.TruncateWithPostfix(MaxBrowserInfoLength),
                Exception = exceptionMessage.TruncateWithPostfix(MaxExceptionLength),
                ImpersonatorUserId = auditInfo.ImpersonatorUserId,
                ImpersonatorTenantId = auditInfo.ImpersonatorTenantId,
                CustomData = auditInfo.CustomData.TruncateWithPostfix(MaxCustomDataLength)
            };
        }

        public override string ToString()
        {
            return string.Format(
                "日志: {0}.{1} 执行由用户 {2} 执行时长 {3} ms {4} IP 地址.",
                ServiceName, MethodName, UserId, ExecutionDuration, ClientIpAddress
                );
        }
    }


}