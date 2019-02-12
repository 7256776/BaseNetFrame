using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace Frame.Core
{
    public interface ISysOrgRepository : IRepository<SysOrg, Guid>
    {
        /// <summary>
        /// 转换菜单列表为:
        /// [{
        ///     id:"1"
        ///     MenuName:"菜单名称"
        ///     ChildrenMenus:[
        ///         { id:"2", MenuName:"子菜单1"},
        ///         { id:"2", MenuName:"子菜单2"}
        ///     ]
        /// },..]
        /// </summary>
        /// <param name="dataAll"></param>
        /// <returns></returns>
        List<SysOrg> ConvertSysOrgByChildrenList(List<SysOrg> dataAll);

        /// <summary>
        /// 修改节点下所有子节点的OrgNode
        /// </summary>
        /// <param name="newOrgNode">节点新的orgNode</param>
        /// <param name="oldOrgNode">节点原来的orgNode</param>
        /// <returns></returns>
        void UpdateChildrensOrgNode(string newOrgNode, string oldOrgNode);
    }
}
