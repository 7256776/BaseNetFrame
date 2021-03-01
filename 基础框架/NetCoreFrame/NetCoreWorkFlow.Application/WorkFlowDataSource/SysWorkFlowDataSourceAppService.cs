using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NetCoreWorkFlow.Application
{
    public class SysWorkFlowDataSourceAppService : NetCoreWorkFlowApplicationBase, ISysWorkFlowDataSourceAppService
    {
        private readonly ISysWorkFlowDataSourceRepository _sysWorkFlowDataSourceRepository;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysWorkFlowDataSourceAppService(ISysWorkFlowDataSourceRepository sysWorkFlowDataSourceRepository)
        {
            _sysWorkFlowDataSourceRepository = sysWorkFlowDataSourceRepository;

        }



        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task<SysWorkFlowDataSource> GetWorkFlowDataSource(string id)
        {
            SysWorkFlowDataSource data = await _sysWorkFlowDataSourceRepository.GetAsync(new Guid(id));
            return data;
        }

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        public List<SysWorkFlowDataSource> GetWorkFlowDataSourceList(SysWorkFlowDataSourceParam model)
        {
            var data = _sysWorkFlowDataSourceRepository.GetAll();
            if (!string.IsNullOrEmpty(model.DataSourceType))
            {
                data = data.Where(w => w.DataSourceType == model.DataSourceType);
            }
            if (!string.IsNullOrEmpty(model.DataSourceName))
            {
                data = data.Where(w => w.DataSourceType.Contains(model.DataSourceName));
            }
            return data.ToList();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public Task DelWorkFlowDataSource(List<string> ids)
        {
            foreach (var idItem in ids)
            {
                Guid id = new Guid(idItem);
                _sysWorkFlowDataSourceRepository.DeleteAsync(id);
            }
            return Task.CompletedTask;
        }

        // <summary>
        /// 保存对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task SaveWorkFlowDataSource(SysWorkFlowDataSourceInput model)
        {
            var repeatData = await _sysWorkFlowDataSourceRepository.GetAllListAsync(w => w.DataSourceName == model.DataSourceName && w.Id != model.Id);
            if (repeatData.Any())
            {
                throw new UserFriendlyException("数据源名称重复", "您设置的名称" + model.DataSourceName + "重复!");
            }
            if (model.Id == null)
            {
                SysWorkFlowDataSource modelInput = ObjectMapper.Map<SysWorkFlowDataSource>(model);
                await _sysWorkFlowDataSourceRepository.InsertAndGetIdAsync(modelInput);
            }
            else
            {
                //获取需要更新的数据
                SysWorkFlowDataSource data = await _sysWorkFlowDataSourceRepository.GetAsync(model.Id.Value);
                //映射需要修改的数据对象
                SysWorkFlowDataSource m = ObjectMapper.Map(model, data);
                //提交修改(实际上属于同一个工作单元执行修改可以忽略)
                await _sysWorkFlowDataSourceRepository.UpdateAsync(m);
            }
        }




    }
}
