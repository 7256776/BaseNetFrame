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
    public class SysWorkFlowTypeRepository : EfCoreRepositoryBase<NetCoreWorkFlowDbContext, SysWorkFlowType, Guid>, ISysWorkFlowTypeRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysWorkFlowTypeRepository(IDbContextProvider<NetCoreWorkFlowDbContext> dbcontext) : base(dbcontext)
        {

        }

       
    }
}
