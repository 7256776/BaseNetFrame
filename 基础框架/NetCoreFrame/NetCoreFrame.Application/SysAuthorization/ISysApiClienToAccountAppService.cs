using Abp.Application.Services;
using Abp.Web.Models;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.Application
{
    /// <summary>
    /// 授权账号
    /// </summary>
    public interface ISysApiClienToAccountAppService : IApplicationService
    {

        /// <summary>
        /// 查询客户相关的账号
        /// </summary>
        /// <param name="apiClientId">客户主键ID</param>
        /// <returns></returns>
        Task<List<SysApiAccountData>> GetApiAccountByClient(string apiClientId);

        /// <summary>
        /// 删除客户相关的账号
        /// </summary>
        /// <param name="list">客户To账号对象</param>
        /// <returns></returns>
        Task<bool> DelClienToAccount(List<SysApiClienToAccountInput> list);

    }
}
