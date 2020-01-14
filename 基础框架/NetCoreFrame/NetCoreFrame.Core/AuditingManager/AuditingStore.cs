using Abp.Auditing;
using Abp.Dependency;
using Abp.Domain.Repositories;
using System.Threading.Tasks;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// �����־
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public class AuditingStore : IAuditingStore, ITransientDependency
    {
        private readonly IRepository<SysAuditLog, long> _auditLogRepository;

        /// <summary>
        /// ������־�ִ����� <see cref="AuditingStore"/>.
        /// </summary>
        public AuditingStore(IRepository<SysAuditLog, long> auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public void Save(AuditInfo auditInfo)
        {
            _auditLogRepository.Insert(SysAuditLog.CreateFromAuditInfo(auditInfo));
        }

        /// <summary>
        /// ������־��Ϣ
        /// </summary>
        /// <param name="auditInfo"></param>
        /// <returns></returns>
        public virtual Task SaveAsync(AuditInfo auditInfo)
        {
            return _auditLogRepository.InsertAsync(SysAuditLog.CreateFromAuditInfo(auditInfo));
        }

     
    }
}