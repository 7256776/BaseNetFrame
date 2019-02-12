using Abp.Application.Services;
using Abp.Application.Services.Dto;
using NetCoreFrame.Core;

namespace NetCoreFrame.Application
{

    public interface ISysAuditLogsAppService : IApplicationService
    {
        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagingDto"></param>
        /// <returns></returns>
        PagedResultDto<SysAuditList> GetAuditLogList(RequestParam<dynamic> requestParam);

        /// <summary>
        /// 清空所有日志
        /// </summary>
        /// <returns></returns>
        int DelAuditLog();




    }
}
