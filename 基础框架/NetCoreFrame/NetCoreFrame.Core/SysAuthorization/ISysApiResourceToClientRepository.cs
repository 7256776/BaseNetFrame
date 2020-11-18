using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        /// <summary>
        /// 查询客户与资源关系
        /// 目前对应关系是一对多
        /// 一个授权客户 对应一个 授权资源
        /// 一个授权资源 对应多个 授权客户
        /// </summary>
        /// <param name="apiClientId">资源主键ID</param>
        /// <returns></returns>
        IQueryable<SysApiClientToResourceData> GetApiClientAndResource();

    }
}
