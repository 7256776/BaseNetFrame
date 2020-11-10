using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using NetCoreFrame.Application;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.UI;

namespace NetCoreFrame.Core
{
    /// <summary> 
    /// 授权账号
    /// </summary>
    public class SysApiClienToAccountAppService : NetCoreFrameApplicationBase, ISysApiClienToAccountAppService
    {
        private readonly ISysApiClienToAccountRepository _sysApiClienToAccountRepository;

        public SysApiClienToAccountAppService(
            ISysApiClienToAccountRepository sysApiClienToAccountRepository
        )
        {
            _sysApiClienToAccountRepository = sysApiClienToAccountRepository;
        }

        /// <summary>
        /// 查询客户相关的账号
        /// </summary>
        /// <param name="apiClientId">资源主键ID</param>
        /// <returns></returns>
        public Task<List<SysApiAccount>> GetApiAccountByClient(string apiClientId)
        {
            if (string.IsNullOrEmpty(apiClientId))
            {
                return Task.FromResult<List<SysApiAccount>>(null);
            }
            var queryable = _sysApiClienToAccountRepository.GetApiAccountByClient(Guid.Parse(apiClientId));
            return Task.FromResult(queryable.ToList());
        }





    }
}
