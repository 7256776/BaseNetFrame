using Abp.Domain.Repositories;
using System.Linq;

namespace Frame.Core
{
    public interface ISysAuditLogsRepository : IRepository<SysAuditLog, long>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable<SysAuditList> GetAuditLogList();

        /// <summary>
        /// 清空日志
        /// </summary>
        /// <returns></returns>
        int CleanAuditLog();


    }
}
