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
    public interface ISysWorkFlowRoleAppService : IApplicationService
    {

        /// <summary>
        /// 查询角色集合
        /// </summary>
        /// <returns></returns> 
        Task<List<SysWorkFlowRole>> GetWorkFlowRoleList();

        /// <summary> 
        /// 查询角色对象       
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        Task<SysWorkFlowRole> GetWorkFlowRoleModel(string id);

        /// <summary>
        /// 保存角色(新增,修改)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        Task<AjaxResponse> SaveWorkFlowRole(SysWorkFlowRoleInput model);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DelWorkFlowRole(List<string> ids);

        // <summary>
        /// 保存流程角色与用户关系
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> SaveWorkFlowRoleToUser(SysWorkFlowRoleToUserInput model);

        /// <summary>
        /// 删除流程角色与用户关系
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task DelWorkFlowRoleToUser(SysWorkFlowRoleToUserInput model);

    }
}
