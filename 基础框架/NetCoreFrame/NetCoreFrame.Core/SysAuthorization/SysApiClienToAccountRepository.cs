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

        /// <summary>
        /// 查询账号与客户关系
        /// 目前对应关系是一对多
        /// 一个授权账号 对应一个 授权客户
        /// 一个授权客户 对应多个 授权账号
        /// </summary>
        /// <param name="apiClientId">资源主键ID</param>
        /// <returns></returns>
        public IQueryable<SysApiClienToAccountData> GetApiClientAndAccount()
        {
            var data = from sacta in base.Context.SysApiClienToAccounts
                       join sac in base.Context.SysApiClients on sacta.ApiClientId equals sac.Id
                       join saa in base.Context.SysApiAccounts on sacta.ApiAccountId equals saa.Id
                       select new SysApiClienToAccountData
                       {
                           //
                           UserName = saa.UserName,
                           Password = saa.Password,
                           Description = saa.Description,
                           ExtensionAccountData = saa.ExtensionData,
                           IsActiveAccount = saa.IsActive,
                           //
                           ClientId = sac.ClientId,
                           ClientSecrets = sac.ClientSecrets,
                           ExtensionClientData = sac.ExtensionData,
                           IsActiveClient = sac.IsActive,
                       };
            return data;
        }



    }
}
