using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using System;

namespace NetCoreFrame.Core
{
    public class SysApiClientRepository : EfCoreRepositoryBase<NetCoreFrameDbContext, SysApiClient, Guid>, ISysApiClientRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysApiClientRepository(IDbContextProvider<NetCoreFrameDbContext> dbcontext) : base(dbcontext)
        {

        }

    }
}
