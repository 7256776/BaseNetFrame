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

        public SysApiResourceAppService(
            ISysApiResourceRepository sysApiResourceRepository
        )
        {
            _sysApiResourceRepository = sysApiResourceRepository;
        }

        /// <summary>
        /// 获取授权资源集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<List<SysApiResource>> GetSysApiResourceList(SysApiResourceData model)
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
            return Task.FromResult(queryable.ToList());
        }

        /// <summary>
        /// 获取授权资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<SysApiResource> GetSysApiResource(string id)
        { 
            if (string.IsNullOrEmpty(id))
            {
                return Task.FromResult<SysApiResource>(null);
            }
            var model = _sysApiResourceRepository.Get(Guid.Parse(id));
            return Task.FromResult(model);
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
            return Task.FromResult(true);
        }

        /// <summary>
        /// 删除授权资源
        /// </summary>
        /// <param name="ids"></param>
        public Task<bool> DelSysApiResource(List<string> ids)
        {
            foreach (var id in ids)
            {
                if (string.IsNullOrEmpty(id))
                {
                    continue;
                }
                _sysApiResourceRepository.Delete(Guid.Parse(id));
            }
            return Task.FromResult(true);
        }


    }
}
