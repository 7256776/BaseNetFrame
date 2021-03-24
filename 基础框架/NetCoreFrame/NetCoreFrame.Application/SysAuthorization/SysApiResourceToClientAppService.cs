using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using NetCoreFrame.Application;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.UI;
using Abp.Auditing;
using Abp.Authorization;

namespace NetCoreFrame.Core
{
    /// <summary> 
    /// 资源授权的客户
    /// </summary>
    [Audited]
    [AbpAuthorize]
    public class SysApiResourceToClientAppService : NetCoreFrameApplicationBase, ISysApiResourceToClientAppService
    {
        private readonly ISysApiResourceToClientRepository _sysApiResourceToClientRepository;

        public SysApiResourceToClientAppService(
            ISysApiResourceToClientRepository sysApiResourceToClientRepository
        )
        {
            _sysApiResourceToClientRepository = sysApiResourceToClientRepository;
        }

        /// <summary>
        /// 查询资源相关的客户
        /// </summary>
        /// <param name="apiResourceId">资源主键ID</param>
        /// <returns></returns>
        public Task<List<SysApiClientData>> GetApiClientByResource(string apiResourceId)
        {
            if (string.IsNullOrEmpty(apiResourceId))
            {
                return Task.FromResult<List<SysApiClientData>>(null);
            }
            var queryable = _sysApiResourceToClientRepository.GetApiClientByResource(Guid.Parse(apiResourceId));
            List<SysApiClientData> result = ObjectMapper.Map<List<SysApiClientData>>(queryable.ToList());
            return Task.FromResult(result);
        }

        /// <summary>
        /// 删除资源相关的客户
        /// </summary>
        /// <param name="list">资源To客户对象</param>
        /// <returns></returns>
        public Task<bool> DelResourceToClient(List<SysApiResourceToClientInput> list)
        {
            if (list==null || !list.Any())
            {
                throw new UserFriendlyException("授权客户删除", "请选择资源相关的授权客户。");
            }
            foreach (var item in list)
            {
                _sysApiResourceToClientRepository.Delete(w => w.ApiResourceId == item.ApiResourceId && w.ApiClientId == item.ApiClientId);
            }
            return Task.FromResult(true);

        }





    }
}
