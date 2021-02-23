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
    public class SysWorkFlowTypeAppService : NetCoreWorkFlowApplicationBase, ISysWorkFlowTypeAppService
    {
        private readonly ISysWorkFlowTypeRepository _sysWorkFlowTypeRepository;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysWorkFlowTypeAppService(
            ISysWorkFlowTypeRepository sysWorkFlowTypeRepository
            )
        {
            _sysWorkFlowTypeRepository = sysWorkFlowTypeRepository;

        }


        /// <summary>
        /// 查询流程类型集合
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        public List<SysWorkFlowType> GetWorkFlowTypeList(SysWorkFlowTypeModel model)
        {
            var query = _sysWorkFlowTypeRepository.GetAll();
            if (!string.IsNullOrEmpty(model.FlowTypeName))
            {
                query = query.Where(w => w.FlowTypeName.Contains(model.FlowTypeName));
            }
         
            var data = query.ToList();
            return data;
        }

        /// <summary> 
        /// 查询角色对象       
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task<SysWorkFlowType> GetWorkFlowTypeModel(string id)
        {
            var data =await _sysWorkFlowTypeRepository.GetAsync(new Guid(id));
            return data;
        }

        /// <summary>
        /// 保存角色(新增,修改)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task SaveWorkFlowType(SysWorkFlowTypeInput model)
        {
            var repeatData = await _sysWorkFlowTypeRepository.GetAllListAsync(w => w.FlowTypeName == model.FlowTypeName && w.Id != model.Id);
            if (repeatData.Any())
            {
                throw new UserFriendlyException("类型名称重复", "您设置的类型名称" + model.FlowTypeName + "重复!");
            }
            if (model.Id == null)
            {
                SysWorkFlowType modelInput = ObjectMapper.Map<SysWorkFlowType>(model);
                await _sysWorkFlowTypeRepository.InsertAndGetIdAsync(modelInput);
            }
            else
            {
                SysWorkFlowType data = await _sysWorkFlowTypeRepository.GetAsync(model.Id.Value);
                SysWorkFlowType m = ObjectMapper.Map(model, data);
                await _sysWorkFlowTypeRepository.UpdateAsync(m);
            }
        }

        /// <summary>
        /// 删除类型
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public Task DelWorkFlowType(List<string> ids)
        {
            foreach (var idItem in ids)
            {
                Guid id = new Guid(idItem);
                _sysWorkFlowTypeRepository.DeleteAsync(id);
            }
            return Task.CompletedTask;
        }

      

    }
}
