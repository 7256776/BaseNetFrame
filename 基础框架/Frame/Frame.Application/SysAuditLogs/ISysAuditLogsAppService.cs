using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Frame.Core;

namespace Frame.Application
{

    public interface ISysAuditLogsAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagingDto"></param>
        /// <returns></returns>
        PagedResultDto<SysAuditList> GetAuditLogList(SysAuditInput model);

        /// <summary>
        /// 清空所有日志
        /// </summary>
        /// <returns></returns>
        int DelAuditLog();




    }
}
