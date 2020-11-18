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
        /// <param name="apiClientId">客户主键ID</param>
        /// <returns></returns>
        IQueryable<SysApiAccount> GetApiAccountByClient(Guid apiClientId);

        /// <summary>
        /// 查询账号与客户关系
        /// 目前对应关系是一对多
        /// 一个授权账号 对应一个 授权客户
        /// 一个授权客户 对应多个 授权账号
        /// </summary>
        /// <param name="apiClientId">资源主键ID</param>
        /// <returns></returns>
        IQueryable<SysApiClienToAccountData> GetApiClientAndAccount();

    }
}
