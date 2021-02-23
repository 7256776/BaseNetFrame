using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Web.Models;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWorkFlow.Application
{
    public interface ISysWorkFlowTypeAppService : IApplicationService
    {

        /// <summary>
        /// 查询流程类型集合
        /// </summary>
        /// <returns></returns> 
        List<SysWorkFlowType> GetWorkFlowTypeList(SysWorkFlowTypeModel model);

        /// <summary> 
        /// 查询角色对象       
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        Task<SysWorkFlowType> GetWorkFlowTypeModel(string id);

        /// <summary>
        /// 保存角色(新增,修改)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        Task SaveWorkFlowType(SysWorkFlowTypeInput model);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DelWorkFlowType(List<string> ids);

    }
}
