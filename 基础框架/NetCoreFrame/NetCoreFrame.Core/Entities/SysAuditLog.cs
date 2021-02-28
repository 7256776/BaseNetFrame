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
        [Column("TenantId")]
        [Description("�⻧id")]
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// �û�id.
        /// </summary>
        [Column("UserId")]
        [Description("�û�id")]
        public virtual long? UserId { get; set; }

        /// <summary>
        /// ����(��/�ӿ�)������
        /// </summary>
        [Column("ServiceName")]
        [Description("����(��/�ӿ�)������")]
        [StringLength(256)]
        public virtual string ServiceName { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column("MethodName")]
        [Description("��������")]
        [StringLength(256)]
        public virtual string MethodName { get; set; }

        /// <summary>
        /// ����.
        /// </summary>
        [Column("Parameters")]
        [Description("����")]
        [StringLength(1024)]
        public virtual string Parameters { get; set; }

        /// <summary>
        /// ����ִ�еĿ�ʼʱ��
        /// </summary>
        [Column("ExecutionTime")]
        [Description("����ִ�еĿ�ʼʱ��")]
        public virtual DateTime ExecutionTime { get; set; }

        /// <summary>
        /// ����ʱ��.
        /// </summary>
        [Column("ExecutionDuration")]
        [Description("����ʱ��")]
        public virtual int ExecutionDuration { get; set; }

        /// <summary>
        /// IP ��ַ
        /// </summary>
        [Column("ClientIpAddress")]
        [Description("IP ��ַ")]
        [StringLength(64)]
        public virtual string ClientIpAddress { get; set; }

        /// <summary>
        /// �ͻ�������.(һ����������)
        /// </summary>
        [Column("ClientName")]
        [Description("�ͻ�������.(һ����������)")]
        [StringLength(128)]
        public virtual string ClientName { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        [Column("BrowserInfo")]
        [Description("�����")]
        [StringLength(256)]
        public virtual string BrowserInfo { get; set; }

        /// <summary>
        /// �쳣����.
        /// </summary>
        [Column("Exception")]
        [Description("�쳣����")]
        [StringLength(2000)]
        public virtual string Exception { get; set; }

        /// <summary>
        /// �û�id
        /// <see cref="AuditInfo.ImpersonatorUserId"/>.
        /// </summary>
        [Column("ImpersonatorUserId")]
        [Description("�û�id")]
        public virtual long? ImpersonatorUserId { get; set; }

        /// <summary>
        /// �⻧id
        /// <see cref="AuditInfo.ImpersonatorTenantId"/>.
        /// </summary>
        [Column("ImpersonatorTenantId")]
        [Description("�⻧id")]
        public virtual int? ImpersonatorTenantId { get; set; }

        /// <summary>
        /// �Զ�������
        /// <see cref="AuditInfo.CustomData"/>.
        /// </summary>
        [Column("CustomData")]
        [Description("�Զ�������")]
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
                "��־: {0}.{1} ִ�����û� {2} ִ��ʱ�� {3} ms {4} IP ��ַ.",
                ServiceName, MethodName, UserId, ExecutionDuration, ClientIpAddress
                );
        }
    }


}