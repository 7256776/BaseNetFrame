using Abp.Auditing;
using Abp.Dependency;
using Abp.Domain.Repositories;
using System.Threading.Tasks;

namespace Frame.Core
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

        public virtual Task SaveAsync(AuditInfo auditInfo)
        {
            return _auditLogRepository.InsertAsync(SysAuditLog.CreateFromAuditInfo(auditInfo));
        }

     
    }
}