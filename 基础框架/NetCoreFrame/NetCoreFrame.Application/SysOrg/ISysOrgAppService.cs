using Abp.Application.Services;
using Abp.Web.Models;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.Application
{
    public interface ISysOrgAppService : IApplicationService
    {
        /// <summary>
        /// 获取组织结构
        /// </summary>
        /// <returns></returns>
        List<SysOrg> GetSysOrgList();

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        Task<AjaxResponse> SaveSysOrgModel(SysOrgInput model);

        /// <summary>
        /// 删除一个组织机构节点
        /// </summary>
        /// <param name="id"></param>
        void DelSysOrg(Guid id);

        /// <summary>
        /// 查找某个组织机构节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SysOrg GetSysOrgModel(Guid id);

        /// <summary>
        /// 判断OrgCode是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orgCode"></param>
        /// <returns>true: 已存在  false: 不存在</returns>
        bool CheckOrgCode(SysOrgData model);
    }
}
