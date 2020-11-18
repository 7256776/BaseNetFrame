using Abp.Auditing;
using Abp.Authorization;
using Abp.UI;
using NetCoreFrame.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 授权资源
    /// </summary>
    [Audited]
    [AbpAuthorize]
    public class SysApiResourceAppService : NetCoreFrameApplicationBase, ISysApiResourceAppService
    {
        private readonly ISysApiResourceRepository _sysApiResourceRepository;
        private readonly ISysApiResourceToClientRepository _sysApiResourceToClientRepository;
        private readonly ISysIdentityServerCacheAppService _sysIdentityServerCacheAppService;


        public SysApiResourceAppService(
            ISysApiResourceToClientRepository sysApiResourceToClientRepository,
            ISysApiResourceRepository sysApiResourceRepository,
            ISysIdentityServerCacheAppService sysIdentityServerCacheAppService
        )
        {
            _sysApiResourceToClientRepository = sysApiResourceToClientRepository;
            _sysApiResourceRepository = sysApiResourceRepository;
            _sysIdentityServerCacheAppService = sysIdentityServerCacheAppService;
        }

        /// <summary>
        /// 获取授权资源集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<List<SysApiResourceData>> GetSysApiResourceList(SysApiResourceData model)
        {
            var queryable = _sysApiResourceRepository.GetAll();
            if (!string.IsNullOrEmpty(model.ResourceName))
            {
                queryable = queryable.Where(w => w.ResourceName.Contains(model.ResourceName));
            }
            if (model.IsActive != null)
            {
                queryable = queryable.Where(w => w.IsActive == model.IsActive);
            }
            List<SysApiResourceData> list = ObjectMapper.Map<List<SysApiResourceData>>(queryable.ToList());
            return Task.FromResult(list);
        }

        /// <summary>
        /// 获取授权资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<SysApiResourceData> GetSysApiResource(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Task.FromResult<SysApiResourceData>(null);
            }
            var model = _sysApiResourceRepository.Get(Guid.Parse(id));
            SysApiResourceData m = ObjectMapper.Map<SysApiResourceData>(model);
            return Task.FromResult(m);
        }

        /// <summary>
        /// 保存授权资源
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<bool> SaveSysApiResource(SysApiResourceInput model)
        {
            var isRepeat = _sysApiResourceRepository.GetAllList(w => w.ResourceName == model.ResourceName && w.Id != model.Id).Any();
            if (isRepeat)
            {
                throw new UserFriendlyException("授权资源重复", "您设置的授权资源" + model.ResourceName + "重复!");
            }
            if (model.Id == null)
            {
                SysApiResource modelInput = ObjectMapper.Map<SysApiResource>(model);
                _sysApiResourceRepository.InsertAndGetIdAsync(modelInput);
            }
            else
            {
                SysApiResource data = _sysApiResourceRepository.Get(model.Id.Value);
                SysApiResource m = ObjectMapper.Map(model, data);
                _sysApiResourceRepository.UpdateAsync(m);
            }
            //移除缓存
            _sysIdentityServerCacheAppService.RemoveResourcesCache();
            _sysIdentityServerCacheAppService.RemoveClientAndAccountCache();
            _sysIdentityServerCacheAppService.RemoveClientCache();
            return Task.FromResult(true);
        }

        /// <summary>
        /// 删除授权资源
        /// </summary>
        /// <param name="ids"></param>
        public Task<bool> DelSysApiResource(List<string> ids)
        {
            #region 验证关联的客户
            List<Guid> gids = ObjectMapper.Map<List<Guid>>(ids);
            var resultData = _sysApiResourceToClientRepository.GetAllList(w => gids.Contains(w.ApiResourceId));
            if (resultData.Any())
            {
                throw new UserFriendlyException("授权资源删除", "请先移除相关客户再删除授权资源。");
            }
            #endregion
            #region 
            foreach (var id in ids)
            {
                if (string.IsNullOrEmpty(id))
                {
                    continue;
                }
                _sysApiResourceRepository.Delete(Guid.Parse(id));
            }
            #endregion
            //移除缓存
            _sysIdentityServerCacheAppService.RemoveResourcesCache();
            _sysIdentityServerCacheAppService.RemoveClientAndAccountCache();
            _sysIdentityServerCacheAppService.RemoveClientCache();
            return Task.FromResult(true);
        }


    }
}
