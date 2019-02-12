using Abp.Domain.Uow;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using System.Linq;

namespace Frame.Core
{
    public class SysAuditLogsRepository : EfRepositoryBase<DataDbContext, SysAuditLog, long>, ISysAuditLogsRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysAuditLogsRepository(IDbContextProvider<DataDbContext> dbcontext) : base(dbcontext)
        {

        }

        /// <summary>
        /// 获取审计日志集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<SysAuditList> GetAuditLogList()
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                return from log in Context.SysAuditLogs
                       join user in Context.UserInfos on log.UserId equals user.Id into lu
                       from x in lu.DefaultIfEmpty()
                       select new SysAuditList
                       {
                           TenantId = log.TenantId,
                           UserId = log.UserId,
                           ServiceName = log.ServiceName,
                           MethodName = log.MethodName,
                           Parameters = log.Parameters,
                           ExecutionTime = log.ExecutionTime,
                           ExecutionDuration = log.ExecutionDuration,
                           ClientIpAddress = log.ClientIpAddress,
                           ClientName = log.ClientName,
                           BrowserInfo = log.BrowserInfo,
                           Exception = log.Exception,
                           ImpersonatorUserId = log.ImpersonatorUserId,
                           ImpersonatorTenantId = log.ImpersonatorTenantId,
                           CustomData = log.CustomData,
                           UserNameCn = x.UserNameCn,
                           UserCode = x.UserCode
                       };
            }
        }

        /// <summary>
        /// 清空日志
        /// </summary>
        /// <returns></returns>
        public int CleanAuditLog()
        {
            string sql = " DELETE FROM SYS_AUDITLOGS  ";
            return base.Context.Database.ExecuteSqlCommand(sql);
        }

    }
}
