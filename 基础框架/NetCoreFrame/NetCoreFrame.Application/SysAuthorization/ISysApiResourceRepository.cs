using Abp.Application.Services;
using NetCoreFrame.Application;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace NetCoreFrame.Core
{
    public interface ISysApiResourceAppService : IApplicationService
    {
        /// <summary>
        /// 获取授权资源集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<List<SysApiResourceData>> GetSysApiResourceList(SysApiResourceData model);

        /// <summary>
        /// 获取授权资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SysApiResourceData> GetSysApiResource(string id);

        /// <summary>
        /// 保存授权资源
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> SaveSysApiResource(SysApiResourceInput model);

        /// <summary>
        /// 删除授权资源
        /// </summary>
        /// <param name="ids"></param>
        Task<bool> DelSysApiResource(List<string> ids);

    }
}
