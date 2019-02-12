using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using System;

namespace Frame.Core
{
    public class SysDictRepository : EfRepositoryBase<DataDbContext, SysDict, Guid>, ISysDictRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysDictRepository(IDbContextProvider<DataDbContext> dbcontext) : base(dbcontext)
        {

        }

    }
}
