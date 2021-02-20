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
    public class SysWorkFlowRoleToUserRepository : EfCoreRepositoryBase<NetCoreWorkFlowDbContext, SysWorkFlowRoleToUser, Guid>, ISysWorkFlowRoleToUserRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysWorkFlowRoleToUserRepository(IDbContextProvider<NetCoreWorkFlowDbContext> dbcontext) : base(dbcontext)
        {

        }

       
    }
}
