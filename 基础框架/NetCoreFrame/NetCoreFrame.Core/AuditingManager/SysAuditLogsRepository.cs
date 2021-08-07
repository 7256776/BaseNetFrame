using Abp.Domain.Uow;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace NetCoreFrame.Core
{
    public class SysAuditLogsRepository : EfCoreRepositoryBase <NetCoreFrameDbContext, SysAuditLog, long>, ISysAuditLogsRepository
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysAuditLogsRepository(IDbContextProvider<NetCoreFrameDbContext> dbcontext) : base(dbcontext)
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
        /// 清空日志调用时需要关闭相关操作的日志记录功能避免锁表导致超时 [DisableAuditing]
        /// </summary>
        /// <returns></returns>
        public int CleanAuditLog()
        {
            //string sql = " DELETE FROM SYS_AUDITLOGS  ";
            //该方式清空日志表速度快但是返回是 -1
            string sql = " TRUNCATE TABLE SYS_AUDITLOG  ";
            int result= base.Context.Database.ExecuteSqlCommand(sql);
            return result;
        }

    }
}
