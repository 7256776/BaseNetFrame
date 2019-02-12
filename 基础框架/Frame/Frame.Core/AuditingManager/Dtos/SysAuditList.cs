using System;

namespace Frame.Core
{
    /// <summary>
    /// ��־����
    /// </summary>
    public class SysAuditList 
    {
        public virtual int? Id { get; set; }

        /// <summary>
        /// �⻧id.
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// �û�id.
        /// </summary>
        public virtual long? UserId { get; set; }
        
        /// <summary>
        /// ����(��/�ӿ�)������
        /// </summary>
        public virtual string ServiceName { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public virtual string MethodName { get; set; }

        /// <summary>
        /// ����.
        /// </summary>
        public virtual string Parameters { get; set; }

        /// <summary>
        /// ����ִ�еĿ�ʼʱ��
        /// </summary>
        public virtual DateTime ExecutionTime { get; set; }
        
        /// <summary>
        /// ����ʱ��.
        /// </summary>
        public virtual int ExecutionDuration { get; set; }

        /// <summary>
        /// IP ��ַ
        /// </summary>
        public virtual string ClientIpAddress { get; set; }

        /// <summary>
        /// �ͻ�������.(һ����������)
        /// </summary>
        public virtual string ClientName { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        public virtual string BrowserInfo { get; set; }

        /// <summary>
        /// �쳣����.
        /// </summary>
        public virtual string Exception { get; set; }

        /// <summary>
        /// �û�id
        /// </summary>
        public virtual long? ImpersonatorUserId { get; set; }

        /// <summary>
        /// �⻧id
        /// </summary>
        public virtual int? ImpersonatorTenantId { get; set; }

        /// <summary>
        /// �Զ�������
        /// <see cref="AuditInfo.CustomData"/>.
        /// </summary>
        public virtual string CustomData { get; set; }

        /// <summary>
        /// �û�����
        /// </summary>
        public virtual string UserNameCn { get; set; }

        /// <summary>
        /// �û��˺�
        /// </summary>
        public virtual string UserCode { get; set; }


    }
}