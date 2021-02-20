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
    public class ViewSysFlowRoleToUserRepository : EfCoreRepositoryBase<NetCoreWorkFlowDbContext, ViewSysFlowRoleToUser, Guid>, IViewSysFlowRoleToUserRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public ViewSysFlowRoleToUserRepository(IDbContextProvider<NetCoreWorkFlowDbContext> dbcontext) : base(dbcontext)
        {

        }

       
    }
}
