using Abp.Application.Services;
using Abp.Domain.Repositories;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWorkFlow.Application
{
    public interface ISysWorkFlowDataSourceAppService : IApplicationService
    {

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        Task<SysWorkFlowDataSource> GetWorkFlowDataSource(string id);

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <returns></returns> 
        Task<List<SysWorkFlowDataSource>> GetWorkFlowDataSourceList(SysWorkFlowDataSourceParam model);


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns> 
        Task DelWorkFlowDataSource(List<string> ids);

        // <summary>
        /// 保存对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        Task SaveWorkFlowDataSource(SysWorkFlowDataSourceInput model);

    }
}
