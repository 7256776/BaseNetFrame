using Abp.Application.Services;
using Abp.Web.Models;
using IdentityServer4.Models;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.Application
{
    /// <summary>
    /// IdentityServer授权服务业务提供
    /// </summary>
    public interface ISysIdentityServerCacheAppService : IApplicationService
    {
        #region 客户

        /// <summary>
        /// 获取所有客户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Client> GetClientCache();

        /// <summary>
        /// 移除所有客户
        /// </summary>
        /// <param name="userId"></param>
        void RemoveClientCache();

        #endregion

        #region 资源

        /// <summary>
        /// 获取所有资源
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<ApiResource> GetResourcesCache();

        /// <summary>
        /// 移除所有资源
        /// </summary>
        /// <param name="userId"></param>
        void RemoveResourcesCache();

        #endregion

        #region 账号

        /// <summary>
        /// 获取所有账号与客户关系
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<SysApiClienToAccountData> GetClientAndAccountCache();

        /// <summary>
        /// 移除所有账号与客户关系
        /// </summary>
        /// <param name="userId"></param>
        void RemoveClientAndAccountCache();

        #endregion
    }
}
