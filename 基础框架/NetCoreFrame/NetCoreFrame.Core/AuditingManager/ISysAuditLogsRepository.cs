using Abp.Domain.Repositories;
using System.Linq;

namespace NetCoreFrame.Core
{
    public interface ISysAuditLogsRepository : IRepository<SysAuditLog, long>
    {
        /// <summary>
        /// 获取日志列表
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
