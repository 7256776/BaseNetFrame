using Abp.Application.Services;
using NetCoreFrame.Application;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 授权客户
    /// </summary>
    public interface ISysApiClientAppService : IApplicationService
    {

        /// <summary>
        /// 获取授权客户集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<List<SysApiClientData>> GetSysApiClientList(SysApiClientData model);

        /// <summary>
        /// 获取授权客户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SysApiClientData> GetSysApiClient(string id);

        /// <summary>
        /// 保存授权客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> SaveSysApiClient(SysApiClientInput model);

        /// <summary>
        /// 删除授权客户
        /// </summary>
        /// <param name="ids"></param>
        Task<bool> DelSysApiClient(List<string> ids);

    }
}
