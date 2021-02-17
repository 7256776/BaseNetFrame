using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Web.Models;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWorkFlow.Application
{
    public class SysWorkFlowRoleAppService : NetCoreWorkFlowApplicationBase, ISysWorkFlowRoleAppService
    {
        private readonly ISysWorkFlowRoleRepository _sysWorkFlowRoleRepository;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysWorkFlowRoleAppService(
            ISysWorkFlowRoleRepository sysWorkFlowRoleRepository
            )
        {
            _sysWorkFlowRoleRepository = sysWorkFlowRoleRepository;


        }

        /// <summary>
        /// 查询角色集合
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        public Task<List<SysWorkFlowRole>> GetWorkFlowRoleList()
        {
            var data = _sysWorkFlowRoleRepository.GetAllListAsync();
            return data;
        }

        /// <summary> 
        /// 查询角色对象       
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public Task<SysWorkFlowRole> GetWorkFlowRoleModel(string id)
        {
            var data = _sysWorkFlowRoleRepository.GetAsync(new Guid(id));
            return data;
        }
          
        /// <summary>
        /// 保存角色(新增,修改)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task<AjaxResponse> SaveWorkFlowRole(SysWorkFlowRoleInput model)
        {
            var repeatData =await _sysWorkFlowRoleRepository.GetAllListAsync(w => w.FlowRoleName == model.FlowRoleName && w.Id != model.Id);
            if (repeatData.Any())
            {
                throw new UserFriendlyException("角色名称重复", "您设置的角色名称" + model.FlowRoleName + "重复!");
            }
            if (model.Id == null)
            {
                SysWorkFlowRole modelInput = ObjectMapper.Map<SysWorkFlowRole>(model);
                await _sysWorkFlowRoleRepository.InsertAndGetIdAsync(modelInput);
            }
            else
            {
                //获取需要更新的数据
                SysWorkFlowRole data = await _sysWorkFlowRoleRepository.GetAsync(model.Id.Value);
                //映射需要修改的数据对象
                SysWorkFlowRole m = ObjectMapper.Map(model, data);
                //提交修改(实际上属于同一个工作单元执行修改可以忽略)
                await _sysWorkFlowRoleRepository.UpdateAsync(m);
            }

            return new AjaxResponse { Success = true };
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public Task DelWorkFlowRole(List<string> ids)
        {
            foreach (var idItem in ids)
            {
                Guid id = new Guid(idItem);
                _sysWorkFlowRoleRepository.DeleteAsync(id);
            }
            return Task.CompletedTask;
        }


    }
}
