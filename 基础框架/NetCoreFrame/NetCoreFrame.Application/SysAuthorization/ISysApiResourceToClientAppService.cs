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
    public interface ISysApiResourceToClientAppService : IApplicationService
    {

        /// <summary>
        /// 查询资源相关的客户
        /// </summary>
        /// <param name="apiResourceId">资源主键ID</param>
        /// <returns></returns>
        Task<List<SysApiClient>> GetApiClientByResource(string apiResourceId);



    }
}
