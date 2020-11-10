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

namespace NetCoreFrame.Core
{
    /// <summary> 
    /// 授权账号
    /// </summary>
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
        public Task<List<SysApiClient>> GetApiClientByResource(string apiResourceId)
        {
            if (string.IsNullOrEmpty(apiResourceId))
            {
                return Task.FromResult<List<SysApiClient>>(null);
            }
            var queryable = _sysApiResourceToClientRepository.GetApiClientByResource(Guid.Parse(apiResourceId));
            return Task.FromResult(queryable.ToList());
        }





    }
}
