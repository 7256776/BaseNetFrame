using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Domain.Repositories;
using NetCoreFrame.Core;
using System;
using System.Linq;

namespace NetCoreFrame.Application
{
    [AbpAuthorize]
    [Audited]
    public class SysAuditLogsAppService : NetCoreFrameApplicationBase, ISysAuditLogsAppService
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
        /// 查询日志
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagingDto"></param>
        /// <returns></returns>
        [DisableAuditing] 
        public PagedResultDto<SysAuditList> GetAuditLogList(RequestParam<dynamic> requestParam)
        {
            var model = requestParam.ToEntityObject<SysAuditInput>();

            DateTime? beg = null;
            DateTime? end = null;
            string serviceName = model.ServiceName == null ? "" : model.ServiceName.Trim();
            string userName = model.UserName == null ? "" : model.UserName.Trim();
            string methodName = model.MethodName == null ? "" : model.MethodName.Trim();
            if (model.ExecutionTime != null && model.ExecutionTime.Any())
            {
                beg = System.TimeZoneInfo.ConvertTime(model.ExecutionTime[0], System.TimeZoneInfo.Local);
                end = System.TimeZoneInfo.ConvertTime(model.ExecutionTime[1].AddDays(1), System.TimeZoneInfo.Local);
            }
            //此处如果beg与end未设置日期,查询条件将不会添加日期区间
            var data = _sysAuditLogsRepository.GetAuditLogList().Where(w =>
                                (w.UserCode.Contains(userName) || w.UserNameCn.Contains(userName) || userName == "") &&
                                w.ServiceName.Contains(serviceName) &&
                                w.MethodName.Contains(methodName) &&
                                ((w.ExecutionTime >= beg && w.ExecutionTime <= end) || beg == null || end == null)
                             );

            return data.OrderByDescending(o => o.ExecutionTime).GetPagingData(requestParam.PagingDto);
        }

        /// <summary>
        /// 清空所有日志
        /// </summary>
        /// <returns></returns>
        //[RemoteService(true)] 设置是否生成该服务为接口
        public int DelAuditLog()
        {
            return _sysAuditLogsRepository.CleanAuditLog();
        }




    }
}
