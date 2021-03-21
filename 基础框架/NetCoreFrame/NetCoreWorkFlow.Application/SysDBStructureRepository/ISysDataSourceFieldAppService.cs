using Abp.Application.Services;
using Abp.Dapper.Repositories;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreWorkFlow.Application
{
    public interface ISysDataSourceFieldAppService : IApplicationService
    {
        /// <summary>
        /// 获取表字段集合
        /// </summary>
        /// <param name="dataSourceId"></param>
        /// <returns></returns>
        List<SysWorkFlowDataSourceItemData> GetDataStructure(string dataSourceId);

        /// <summary>
        /// 保存数据源字段
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task SaveWorkFlowDataSourceItem(List<SysWorkFlowDataSourceItemInput> list);

    }
}
