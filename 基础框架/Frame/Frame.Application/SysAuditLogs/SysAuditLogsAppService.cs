using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Frame.Core;
using System;
using System.Linq;

namespace Frame.Application
{
    [AbpAuthorize]
    [Audited]
    public class SysAuditLogsAppService : FrameExtApplicationService, ISysAuditLogsAppService
    {
        private readonly IRepository<SysAuditLog, long> _auditLogRepository;
        private readonly ISysAuditLogsRepository _sysAuditLogsRepository; 


        public SysAuditLogsAppService(
           IRepository<SysAuditLog, long> auditLogRepository,
           ISysAuditLogsRepository sysAuditLogsRepository
            )
        {
            _sysAuditLogsRepository = sysAuditLogsRepository;
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagingDto"></param>
        /// <returns></returns>
        [DisableAuditing]
        //[RemoteService(isEnabled: false)]
        public PagedResultDto<SysAuditList> GetAuditLogList(SysAuditInput model)
        {
            DateTime? beg = null;
            DateTime? end = null;
            string serviceName = model.ServiceName == null ? "" : model.ServiceName;
            string userName = model.UserName == null ? "" : model.UserName;
            string methodName = model.MethodName == null ? "" : model.MethodName;
            if (model.ExecutionTime != null)
            {
                beg = model.ExecutionTime[0];
                end = model.ExecutionTime[1];
            }

            var data = _sysAuditLogsRepository.GetAuditLogList().Where(w =>
                                (w.UserCode.Contains(userName) || w.UserNameCn.Contains(userName) || userName=="") &&
                                w.ServiceName.Contains(serviceName) &&
                                w.MethodName.Contains(methodName) &&
                                ((w.ExecutionTime >= beg.Value && w.ExecutionTime <= end.Value) || beg == null || end == null)
                             );


            return data.OrderByDescending(o => o.ExecutionTime).GetPagingData(model.PagingModel);
        }

        /// <summary>
        /// 清空所有日志
        /// </summary>
        /// <returns></returns>
        public int DelAuditLog()
        {
            return _sysAuditLogsRepository.CleanAuditLog();
        }




        }
}
