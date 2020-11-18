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
        public Task<List<SysApiAccountData>> GetApiAccountByClient(string apiClientId)
        {
            if (string.IsNullOrEmpty(apiClientId))
            {
                return Task.FromResult<List<SysApiAccountData>>(null);
            }
            var queryable = _sysApiClienToAccountRepository.GetApiAccountByClient(Guid.Parse(apiClientId));
            List<SysApiAccountData> m = ObjectMapper.Map<List<SysApiAccountData>>(queryable.ToList());
            return Task.FromResult(m);
        }

        /// <summary>
        /// 删除客户相关的账号
        /// </summary>
        /// <param name="list">客户To账号对象</param>
        /// <returns></returns>
        public Task<bool> DelClienToAccount(List<SysApiClienToAccountInput> list)
        {
            if (list == null || !list.Any())
            {
                throw new UserFriendlyException("授权账号删除", "请选择客户相关的授权账号。");
            }
            foreach (var item in list)
            {
                _sysApiClienToAccountRepository.Delete(w => w.ApiAccountId == item.ApiAccountId && w.ApiClientId == item.ApiClientId);
            }
            return Task.FromResult(true);

        }




    }
}
