using Abp.Auditing;
using Abp.Dependency;
using Abp.Domain.Repositories;
using System.Threading.Tasks;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 审计日志
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public class AuditingStore : IAuditingStore, ITransientDependency
    {
        private readonly IRepository<SysAuditLog, long> _auditLogRepository;

        /// <summary>
        /// 创建日志仓储对象 <see cref="AuditingStore"/>.
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
        /// 保存日志信息
        /// </summary>
        /// <param name="auditInfo"></param>
        /// <returns></returns>
        public virtual Task SaveAsync(AuditInfo auditInfo)
        {
            return _auditLogRepository.InsertAsync(SysAuditLog.CreateFromAuditInfo(auditInfo));
        }

     
    }
}