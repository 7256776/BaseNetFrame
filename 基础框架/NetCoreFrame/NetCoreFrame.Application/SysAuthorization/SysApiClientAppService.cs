using Abp.UI;
using NetCoreFrame.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 授权客户
    /// </summary>
    public class SysApiClientAppService : NetCoreFrameApplicationBase, ISysApiClientAppService
    {
        private readonly ISysApiClientRepository _sysApiClientRepository;
        private readonly ISysApiResourceToClientRepository _sysApiResourceToClientRepository;

        public SysApiClientAppService(
            ISysApiClientRepository sysApiClientRepository,
            ISysApiResourceToClientRepository sysApiResourceToClientRepository
        )
        {
            _sysApiClientRepository = sysApiClientRepository;
            _sysApiResourceToClientRepository = sysApiResourceToClientRepository;
        }

        /// <summary>
        /// 获取授权客户集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<List<SysApiClient>> GetSysApiClientList(SysApiClientData model)
        {
            var queryable = _sysApiClientRepository.GetAll();
            if (!string.IsNullOrEmpty(model.ClientId))
            {
                queryable = queryable.Where(w => w.ClientId.Contains(model.ClientId));
            }
            if (model.IsActive != null)
            {
                queryable = queryable.Where(w => w.IsActive == model.IsActive);
            }
            return Task.FromResult(queryable.ToList());
        }

        /// <summary>
        /// 获取授权客户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<SysApiClientData> GetSysApiClient(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Task.FromResult<SysApiClientData>(null);
            }
            var data = _sysApiClientRepository.Get(Guid.Parse(id));
            SysApiClientData model = ObjectMapper.Map<SysApiClientData>(data);

            //获取关联的客户
            var linkData = _sysApiResourceToClientRepository.GetAllList(w => w.ApiClientId == Guid.Parse(id));
            if (linkData.Any())
            {
                model.ApiResourceId = linkData[0].ApiResourceId;
            }
            return Task.FromResult(model);
        }

        /// <summary>
        /// 保存授权客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> SaveSysApiClient(SysApiClientInput model)
        {
            var isRepeat = _sysApiClientRepository.GetAllList(w => w.ClientId == model.ClientId && w.Id != model.Id).Any();
            if (isRepeat)
            {
                throw new UserFriendlyException("授权客户重复", "您设置的授权客户" + model.ClientId + "重复!");
            }
            Guid id;
            if (model.Id == null)
            {
                SysApiClient modelInput = ObjectMapper.Map<SysApiClient>(model);
                id = await _sysApiClientRepository.InsertAndGetIdAsync(modelInput);
            }
            else
            {
                SysApiClient data = _sysApiClientRepository.Get(model.Id.Value);
                SysApiClient m = ObjectMapper.Map(model, data);
                await _sysApiClientRepository.UpdateAsync(m);
                id = m.Id;
            }
            //保存客户与资源关系
            _sysApiResourceToClientRepository.Delete(w => w.ApiClientId == id);
            if (!string.IsNullOrEmpty(model.ApiResourceId))
            {
                _sysApiResourceToClientRepository.Insert(new SysApiResourceToClient() { ApiClientId = id, ApiResourceId = Guid.Parse(model.ApiResourceId) });
            }
            return true;
        }

        /// <summary>
        /// 删除授权客户
        /// </summary>
        /// <param name="ids"></param>
        public Task<bool> DelSysApiClient(List<string> ids)
        {
            foreach (var id in ids)
            {
                if (string.IsNullOrEmpty(id))
                {
                    continue;
                }
                _sysApiClientRepository.Delete(Guid.Parse(id));
                //删除该客户相关的所有资源(目前对应关系是 一个客户仅对应一个资源, 因此删除仅会删除一条关系数据)
                //注: 可扩展一对多
                _sysApiResourceToClientRepository.Delete(w => w.ApiClientId == Guid.Parse(id));
            }
            return Task.FromResult(true);
        }

     
    }
}
