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

 
    }
}