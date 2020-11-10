using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using System;
using System.Linq;

namespace NetCoreFrame.Core
{
    public class SysApiResourceRepository : EfCoreRepositoryBase<NetCoreFrameDbContext, SysApiResource, Guid>, ISysApiResourceRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysApiResourceRepository(IDbContextProvider<NetCoreFrameDbContext> dbcontext) : base(dbcontext)
        {

        }

    

    }
}
