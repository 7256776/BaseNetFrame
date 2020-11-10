using Abp.Domain.Repositories;
using System;
using System.Linq;

namespace NetCoreFrame.Core
{
    public interface ISysApiResourceToClientRepository : IRepository<SysApiResourceToClient, Guid>
    {
        /// <summary>
        /// 查询资源相关的客户
        /// </summary>
        /// <param name="apiResourceId">资源主键ID</param>
        /// <returns></returns>
        IQueryable<SysApiClient> GetApiClientByResource(Guid apiResourceId);

    }
}
