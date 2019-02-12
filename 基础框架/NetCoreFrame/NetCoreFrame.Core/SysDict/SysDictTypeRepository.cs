using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using System;

namespace NetCoreFrame.Core
{
    public class SysDictTypeRepository : EfCoreRepositoryBase<NetCoreFrameDbContext, SysDictType, Guid>, ISysDictTypeRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysDictTypeRepository(IDbContextProvider<NetCoreFrameDbContext> dbcontext) : base(dbcontext)
        {

        }
    }
}
