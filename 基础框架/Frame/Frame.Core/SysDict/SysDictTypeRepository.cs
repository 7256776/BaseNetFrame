using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using System;

namespace Frame.Core
{
    public class SysDictTypeRepository : EfRepositoryBase<DataDbContext, SysDictType, Guid>, ISysDictTypeRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysDictTypeRepository(IDbContextProvider<DataDbContext> dbcontext) : base(dbcontext)
        {

        }
    }
}
