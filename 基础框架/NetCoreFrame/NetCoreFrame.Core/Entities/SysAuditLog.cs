using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Extensions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// Used to store audit logs.
    /// </summary>
    [Table("SYS_AUDITLOGS")]
    public class SysAuditLog : Entity<long>
    {
        #region �����ַ�������
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
        /// �⻧id.
        /// </summary>
        [Column("TENANTID")]
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// �û�id.
        /// </summary>
        [Column("USERID")]
        public virtual long? UserId { get; set; }

        /// <summary>
        /// ����(��/�ӿ�)������
        /// </summary>
        [Column("SERVICENAME")]
        public virtual string ServiceName { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column("METHODNAME")]
        public virtual string MethodName { get; set; }

        /// <summary>
        /// ����.
        /// </summary>
        [Column("PARAMETERS")]
        public virtual string Parameters { get; set; }

        /// <summary>
        /// ����ִ�еĿ�ʼʱ��
        /// </summary>
        [Column("EXECUTIONTIME")]
        public virtual DateTime ExecutionTime { get; set; }

        /// <summary>
        /// ����ʱ��.
        /// </summary>
        [Column("EXECUTIONDURATION")]
        public virtual int ExecutionDuration { get; set; }

        /// <summary>
        /// IP ��ַ
        /// </summary>
        [Column("CLIENTIPADDRESS")]
        public virtual string ClientIpAddress { get; set; }

        /// <summary>
        /// �ͻ�������.(һ����������)
        /// </summary>
        [Column("CLIENTNAME")]
        public virtual string ClientName { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        [Column("BROWSERINFO")]
        public virtual string BrowserInfo { get; set; }

        /// <summary>
        /// �쳣����.
        /// </summary>
        [Column("EXCEPTION")]
        public virtual string Exception { get; set; }

        /// <summary>
        /// �û�id
        /// <see cref="AuditInfo.ImpersonatorUserId"/>.
        /// </summary>
        [Column("IMPERSONATORUSERID")]
        public virtual long? ImpersonatorUserId { get; set; }

        /// <summary>
        /// �⻧id
        /// <see cref="AuditInfo.ImpersonatorTenantId"/>.
        /// </summary>
        [Column("IMPERSONATORTENANTID")]
        public virtual int? ImpersonatorTenantId { get; set; }

        /// <summary>
        /// �Զ�������
        /// <see cref="AuditInfo.CustomData"/>.
        /// </summary>
        [Column("CUSTOMDATA")]
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
                "��־: {0}.{1} ִ�����û� {2} ִ��ʱ�� {3} ms {4} IP ��ַ.",
                ServiceName, MethodName, UserId, ExecutionDuration, ClientIpAddress
                );
        }
    }


}