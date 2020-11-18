using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Core
{
    public class SysApiResourceToClientRepository : EfCoreRepositoryBase<NetCoreFrameDbContext, SysApiResourceToClient, Guid>, ISysApiResourceToClientRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysApiResourceToClientRepository(IDbContextProvider<NetCoreFrameDbContext> dbcontext) : base(dbcontext)
        {

        }

        /// <summary>
        /// 查询资源相关的客户
        /// </summary>
        /// <param name="apiResourceId">资源主键ID</param>
        /// <returns></returns>
        public IQueryable<SysApiClient> GetApiClientByResource(Guid apiResourceId)
        {
            var data = from sartc in base.Context.SysApiResourceToClients
                       join sac in base.Context.SysApiClients on sartc.ApiClientId equals sac.Id
                       where sartc.ApiResourceId == apiResourceId
                       select sac;
            return data;
        }

        /// <summary>
        /// 查询客户与资源关系
        /// 目前对应关系是一对多
        /// 一个授权客户 对应一个 授权资源
        /// 一个授权资源 对应多个 授权客户
        /// </summary>
        /// <param name="apiClientId">资源主键ID</param>
        /// <returns></returns>
        public IQueryable<SysApiClientToResourceData> GetApiClientAndResource()
        {
            var data = from sartc in base.Context.SysApiResourceToClients
                       join sar in base.Context.SysApiResources on sartc.ApiResourceId equals sar.Id
                       join sac in base.Context.SysApiClients on sartc.ApiClientId equals sac.Id
                       select new SysApiClientToResourceData
                       {
                           //
                           ClientId = sac.ClientId,
                           ClientSecrets = sac.ClientSecrets,
                           AccessTokenLifetime = sac.AccessTokenLifetime,
                           AllowOfflineAccess = sac.AllowOfflineAccess,
                           SlidingRefreshTokenLifetime = sac.SlidingRefreshTokenLifetime,
                           ExtensionData = sac.ExtensionData,
                           IsActiveClient = sac.IsActive,
                           //
                           ResourceDisplayName = sar.ResourceDisplayName,
                           ResourceName = sar.ResourceName,
                           IsActiveResource = sar.IsActive
                       };
            return data;
        }


    }
}
