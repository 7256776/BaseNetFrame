using Abp.Domain.Uow;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetCoreFrame.Core
{

    public class SysOrgRepository : EfCoreRepositoryBase<NetCoreFrameDbContext, SysOrg, Guid>, ISysOrgRepository
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysOrgRepository(IDbContextProvider<NetCoreFrameDbContext> dbcontext, IUnitOfWorkManager unitOfWorkManager) :
            base(dbcontext)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 转换机构列表为:
        /// [{
        ///     id:"1"
        ///     OrgName:"机构名称"
        ///     ChildrenSysOrg:[
        ///         { id:"2", OrgName:"子菜单1"},
        ///         { id:"2", OrgName:"子菜单2"}
        ///     ]
        /// },..]
        /// </summary>
        /// <param name="dataAll"></param>
        /// <returns></returns>
        public List<SysOrg> ConvertSysOrgByChildrenList(List<SysOrg> dataAll)
        {
            return ConvertMenusList(dataAll);
        }

        /// <summary>
        /// 转换根节点
        /// </summary>
        /// <param name="dataAll"></param>
        /// <param name="resList"></param>
        /// <returns></returns>
        private List<SysOrg> ConvertMenusList(List<SysOrg> dataAll, List<SysOrg> resList = null)
        {
            int i = 1;
            //获取所有父节点
            var parentData = dataAll.Where(p => p.ParentOrgID == null);
            //获取所有子节点
            foreach (var m in parentData)
            {
                if (resList != null)
                {
                    resList.Add(m);
                }
                m.SysOrgLevel = i;
                m.ChildrenSysOrg = dataAll.Where(p => p.ParentOrgID == m.Id).ToList();
                if (m.ChildrenSysOrg.Any())
                {
                    SetChildrenList(dataAll, m, resList);
                }
                else
                {
                    m.IsLeaf = true;
                }
                i = 1;
            }
            //
            if (resList != null)
            {
                return resList;
            }
            else
            {
                return parentData.ToList();
            }
        }

        /// <summary>
        /// 递归获取所有子节点
        /// </summary>
        /// <param name="dataAll"></param>
        /// <param name="currentModel"></param>
        /// <param name="resList"></param>
        private void SetChildrenList(List<SysOrg> dataAll, SysOrg currentModel, List<SysOrg> resList)
        {
            foreach (var item in currentModel.ChildrenSysOrg)
            {
                item.SysOrgLevel = currentModel.SysOrgLevel + 1;

                if (resList != null)
                {
                    resList.Add(item);
                }
                //
                item.ChildrenSysOrg = dataAll.Where(p => p.ParentOrgID == item.Id).ToList();
                if (item.ChildrenSysOrg.Any())
                {
                    SetChildrenList(dataAll, item, resList);
                }
                else
                {
                    item.IsLeaf = true;
                }

            }
        }

        /// <summary>
        /// 修改节点下所有子节点的OrgNode
        /// </summary>
        /// <param name="newOrgNode">节点新的orgNode</param>
        /// <param name="oldOrgNode">节点原来的orgNode</param>
        /// <returns></returns>
        public void UpdateChildrensOrgNode(string newOrgNode, string oldOrgNode)
        {
            var childrens = base.Context.SysOrgs.Where(p => p.OrgNode != oldOrgNode && p.OrgNode.StartsWith(oldOrgNode));
            if (!childrens.Any()) return;
            foreach (var item in childrens)
            {
                //替换原来OrgNode前缀,仅替换第一次找到的匹配的字符串
                Regex orgNode = new Regex(oldOrgNode);
                item.OrgNode = orgNode.Replace(item.OrgNode, newOrgNode, 1);
            }

            base.Context.SaveChanges();
        }
    }
}
