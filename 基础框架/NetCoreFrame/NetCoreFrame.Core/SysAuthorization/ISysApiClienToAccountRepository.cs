using Abp.Domain.Repositories;
using System;
using System.Linq;

namespace NetCoreFrame.Core
{
    public interface ISysApiClienToAccountRepository : IRepository<SysApiClienToAccount, Guid>
    {
        /// <summary>
        /// 查询客户相关的账号
        /// </summary>
        /// <param name="apiClientId">资源主键ID</param>
        /// <returns></returns>
        IQueryable<SysApiAccount> GetApiAccountByClient(Guid apiClientId);


    }
}
