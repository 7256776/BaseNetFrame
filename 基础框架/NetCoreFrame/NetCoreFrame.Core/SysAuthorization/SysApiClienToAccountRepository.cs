using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreFrame.Core
{
    public class SysApiClienToAccountRepository : EfCoreRepositoryBase<NetCoreFrameDbContext, SysApiClienToAccount, Guid>, ISysApiClienToAccountRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysApiClienToAccountRepository(IDbContextProvider<NetCoreFrameDbContext> dbcontext) : base(dbcontext)
        {

        }

        /// <summary>
        /// 查询客户相关的账号
        /// </summary>
        /// <param name="apiClientId">资源主键ID</param>
        /// <returns></returns>
        public IQueryable<SysApiAccount> GetApiAccountByClient(Guid apiClientId)
        {
            var data = from sact in base.Context.SysApiClienToAccounts
                       join saa in base.Context.SysApiAccounts on sact.ApiAccountId equals saa.Id
                       where sact.ApiClientId == apiClientId
                       select saa;
            return data;
        }

    }
}
