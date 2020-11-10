using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

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

    }
}
