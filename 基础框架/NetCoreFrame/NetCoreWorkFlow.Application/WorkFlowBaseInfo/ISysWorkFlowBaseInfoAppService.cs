using Abp.Application.Services;
using Abp.Domain.Repositories;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWorkFlow.Application
{
    public interface ISysWorkFlowBaseInfoAppService : IApplicationService
    {
        /// <summary>
        /// 查询所有组织机构
        /// </summary>
        /// <returns></returns>
        List<SysFlowOrgData> GetFlowOrgAll();

        /// <summary>
        /// 查询所有业务流程
        /// </summary>
        /// <returns></returns>
        List<SysFlowBusinessModule> GetFlowBusinessModuleAll();

        /// <summary>
        /// 分页查询账号信息,并可根据参数进行筛选
        /// </summary>
        /// <param name="flowPagingDto"></param>
        /// <returns></returns>
        FlowPagingResult<SysFlowAccounts> GetFlowAccountsPaging(FlowPagingParam<SysFlowAccountsSearch> flowPagingDto);



    }
}
