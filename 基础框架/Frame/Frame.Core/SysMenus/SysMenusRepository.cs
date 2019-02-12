using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Frame.Core
{
    public class SysMenusRepository : EfRepositoryBase<DataDbContext, SysMenus, long>, ISysMenusRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysMenusRepository(IDbContextProvider<DataDbContext> dbcontext) : base(dbcontext)
        {
       
        }

        /// <summary>
        /// 获取所有启用的菜单
        /// </summary>
        /// <returns></returns>
        public IQueryable<SysMenus> GetMenusAll()
        {
            return base.GetAll().Where(p => p.IsActive == true);
        }

        #region 查询模块数据并转换格式

        /// <summary>
        /// 转换菜单列表分别按照根节点菜单按顺序加载子菜单,数据不包含子节点集合:
        /// [{
        ///     id:"1",
        ///     MenuName:"菜单名称",
        ///     MenuNodeLevel:'1',
        ///     MenuNode:'1'
        /// },
        /// {
        ///     id:"2"
        ///     MenuName:"菜单名称"
        ///     MenuNodeLevel:'2'
        ///     MenuNode:'1.2'
        /// },..]
        /// </summary>
        /// <param name="dataAll">菜单集合</param>
        /// <param name="resList"></param>
        /// <returns></returns>
        public List<SysMenus> ConvertMenusByOrderByList(List<SysMenus> dataAll)
        {
            List<SysMenus> resData = new List<SysMenus>();
            return ConvertMenusList(dataAll, resData);
        }

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
        public List<SysMenus> ConvertMenusByChildrenList(List<SysMenus> dataAll)
        {
            return ConvertMenusList(dataAll);
        }

        /// <summary>
        /// 转换根节点
        /// </summary>
        /// <param name="dataAll"></param>
        /// <param name="resList"></param>
        /// <returns></returns>
        private List<SysMenus> ConvertMenusList(List<SysMenus> dataAll, List<SysMenus> resList=null)
        {
            int i = 1;
            //获取所有父节点
            var parentData = dataAll.Where(p => p.ParentID == null).OrderBy(o => o.OrderBy);
            //获取所有子节点
            foreach (var m in parentData)
            {
                if (resList != null)
                {
                    resList.Add(m);
                }
                m.MenuNode = m.Id + ".";
                m.MenuNodeLevel = i;
                m.ChildrenMenus = dataAll.Where(p => p.ParentID == m.Id).OrderBy(o => o.OrderBy).ToList();
                if (m.ChildrenMenus.Any())
                {
                    SetChildrenList(dataAll, m,  resList);
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
        /// 递归获取所有子节点模块菜单
        /// </summary>
        /// <param name="dataAll"></param>
        /// <param name="currentModel"></param>
        /// <param name="resList"></param>
        private void SetChildrenList(List<SysMenus> dataAll, SysMenus currentModel, List<SysMenus> resList)
        {
            foreach (var item in currentModel.ChildrenMenus)
            {
                item.MenuNodeLevel = currentModel.MenuNodeLevel + 1;
                item.MenuNode = currentModel.MenuNode + item.Id + ".";
                
                if (resList!=null)
                {
                    resList.Add(item);
                }
                //
                item.ChildrenMenus = dataAll.Where(p => p.ParentID == item.Id).OrderBy(o => o.OrderBy).ToList();
                if (item.ChildrenMenus.Any())
                {
                    SetChildrenList(dataAll, item, resList);
                }
                else
                {
                    item.IsLeaf = true;
                }

            }
        }

     
        #endregion

        

    }
}
