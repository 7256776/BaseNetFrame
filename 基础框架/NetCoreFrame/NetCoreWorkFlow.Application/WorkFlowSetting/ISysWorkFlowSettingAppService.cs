using Abp.Application.Services;
using Abp.Domain.Repositories;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWorkFlow.Application
{
    public interface ISysWorkFlowSettingAppService : IApplicationService
    {
        /// <summary>
        /// 获取流程对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SysWorkFlowSettingData> GetWorkFlowSetting(string id);

        /// <summary>
        /// 查询流程集合
        /// </summary>
        /// <returns></returns>
        Task<List<SysWorkFlowSettingData>> GetWorkFlowSettingList();

        /// <summary>
        /// 新增流程配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task InserWorkFlowSetting(SysWorkFlowSettingInput model);

        /// <summary>
        /// 新增流程配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task UpdataWorkFlowSetting(SysWorkFlowSettingInput model);

        /// <summary>
        /// 删除流程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteWorkFlowSetting(string id);

    }
}
