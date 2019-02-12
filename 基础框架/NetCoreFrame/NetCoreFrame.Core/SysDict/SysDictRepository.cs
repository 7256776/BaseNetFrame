using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using System;

namespace NetCoreFrame.Core
{
    public class SysDictRepository : EfCoreRepositoryBase<NetCoreFrameDbContext, SysDict, Guid>, ISysDictRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysDictRepository(IDbContextProvider<NetCoreFrameDbContext> dbcontext) : base(dbcontext)
        {

        }

    }
}
