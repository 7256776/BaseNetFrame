using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using System;

namespace NetCoreFrame.Core
{
    public class SysApiAccountRepository : EfCoreRepositoryBase<NetCoreFrameDbContext, SysApiAccount, Guid>, ISysApiAccountRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysApiAccountRepository(IDbContextProvider<NetCoreFrameDbContext> dbcontext) : base(dbcontext)
        {

        }

    }
}
