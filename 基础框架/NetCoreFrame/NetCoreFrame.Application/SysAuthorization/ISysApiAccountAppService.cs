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
    public interface ISysApiAccountAppService : IApplicationService
    {

        /// <summary>
        /// 获取授权账号集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<List<SysApiAccountData>> GetSysApiAccountList(SysApiAccountData model);

        /// <summary>
        /// 获取授权账号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SysApiAccountData> GetSysApiAccount(string id);

        /// <summary>
        /// 保存授权账号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> SaveSysApiAccount(SysApiAccountInput model);

        /// <summary>
        /// 删除授权账号
        /// </summary>
        /// <param name="ids"></param>
        Task<bool> DelSysApiAccount(List<string> ids);


    }
}
