using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreWorkFlow.Core
{
    public class SysWorkFlowDataSourceRepository : EfCoreRepositoryBase<NetCoreWorkFlowDbContext, SysWorkFlowDataSource, Guid>, ISysWorkFlowDataSourceRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysWorkFlowDataSourceRepository(IDbContextProvider<NetCoreWorkFlowDbContext> dbcontext) : base(dbcontext)
        {

        }

       
    }
}
