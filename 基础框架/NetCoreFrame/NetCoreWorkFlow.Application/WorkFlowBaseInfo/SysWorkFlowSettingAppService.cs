using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Uow;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWorkFlow.Application
{
    public class SysWorkFlowSettingAppService : NetCoreWorkFlowApplicationBase, ISysWorkFlowSettingAppService
    {
        private readonly ISysWorkFlowSettingRepository _sysWorkFlowSettingRepository;
        private readonly ISysWorkFlowEndpointRepository _sysWorkFlowEndpointRepository;
        private readonly ISysWorkFlowConnectionRepository _sysWorkFlowConnectionRepository;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysWorkFlowSettingAppService(
            ISysWorkFlowSettingRepository sysWorkFlowSettingRepository,
            ISysWorkFlowEndpointRepository sysWorkFlowEndpointRepository,
            ISysWorkFlowConnectionRepository sysWorkFlowConnectionRepository
            )
        {
            _sysWorkFlowSettingRepository = sysWorkFlowSettingRepository;
            _sysWorkFlowEndpointRepository = sysWorkFlowEndpointRepository;
            _sysWorkFlowConnectionRepository = sysWorkFlowConnectionRepository;


        }

        /// <summary>
        /// 获取流程对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task<SysWorkFlowSettingData> GetWorkFlowSetting(string id)
        {
            SysWorkFlowSetting data = await _sysWorkFlowSettingRepository.GetAsync(new Guid(id));

            SysWorkFlowSettingData model = ObjectMapper.Map<SysWorkFlowSettingData>(data);

            model.WorkFlowConnectionList = await _sysWorkFlowConnectionRepository.GetAllListAsync(w => w.WorkFlowSettingID == new Guid(id));
            model.WorkFlowEndpointList = await _sysWorkFlowEndpointRepository.GetAllListAsync(w => w.WorkFlowSettingID == new Guid(id));

            return model;
        }

        /// <summary>
        /// 查询流程集合
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task<List<SysWorkFlowSettingData>> GetWorkFlowSettingList()
        {
            List<SysWorkFlowSetting> data = await _sysWorkFlowSettingRepository.GetAllListAsync();

            List<SysWorkFlowSettingData> list = ObjectMapper.Map<List<SysWorkFlowSettingData>>(data);

            return list;
        }

        /// <summary>
        /// 新增流程配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task InserWorkFlowSetting(SysWorkFlowSettingInput model)
        {
            SysWorkFlowSetting data = ObjectMapper.Map<SysWorkFlowSetting>(model);
            await _sysWorkFlowSettingRepository.InsertAsync(data);
            model.Id = data.Id;
            //
            List<SysWorkFlowConnection> connectionList = ObjectMapper.Map<List<SysWorkFlowConnection>>(model.WorkFlowConnectionList);
            foreach (var connection in connectionList)
            {
                connection.WorkFlowSettingID = data.Id;
                await _sysWorkFlowConnectionRepository.InsertAsync(connection);
            }

            //
            List<SysWorkFlowEndpoint> EndpointList = ObjectMapper.Map<List<SysWorkFlowEndpoint>>(model.WorkFlowEndpointList);
            foreach (var endpoint in EndpointList)
            {
                endpoint.WorkFlowSettingID = data.Id;
                await _sysWorkFlowEndpointRepository.InsertAsync(endpoint);
            }
        }

        /// <summary>
        /// 新增流程配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task UpdataWorkFlowSetting(SysWorkFlowSettingInput model)
        {
            SysWorkFlowSetting data = _sysWorkFlowSettingRepository.Get(model.Id.Value);
            data = ObjectMapper.Map(model, data);
            //
            await _sysWorkFlowSettingRepository.UpdateAsync(data);
            //
            await _sysWorkFlowConnectionRepository.DeleteAsync(d => d.WorkFlowSettingID == model.Id.Value);
            //
            List<SysWorkFlowConnection> connectionList = ObjectMapper.Map<List<SysWorkFlowConnection>>(model.WorkFlowConnectionList);
            foreach (var connection in connectionList)
            {
                connection.WorkFlowSettingID = data.Id;
                await _sysWorkFlowConnectionRepository.InsertAsync(connection);
            }
            //
            await _sysWorkFlowEndpointRepository.DeleteAsync(d => d.WorkFlowSettingID == model.Id.Value);
            //
            List<SysWorkFlowEndpoint> EndpointList = ObjectMapper.Map<List<SysWorkFlowEndpoint>>(model.WorkFlowEndpointList);
            foreach (var endpoint in EndpointList)
            {
                endpoint.WorkFlowSettingID = data.Id;
                await _sysWorkFlowEndpointRepository.InsertAsync(endpoint);
            }
        }

        /// <summary>
        /// 删除流程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task DeleteWorkFlowSetting(string id)
        {
            //
            await _sysWorkFlowSettingRepository.DeleteAsync(new Guid(id));
            //
            await _sysWorkFlowConnectionRepository.DeleteAsync(d => d.WorkFlowSettingID == new Guid(id));
            //
            await _sysWorkFlowEndpointRepository.DeleteAsync(d => d.WorkFlowSettingID == new Guid(id));
        }


    }
}
