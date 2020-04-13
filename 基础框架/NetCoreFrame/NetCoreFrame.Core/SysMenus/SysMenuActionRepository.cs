using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreFrame.Core
{
    public class SysMenuActionRepository : EfCoreRepositoryBase<NetCoreFrameDbContext, SysMenuAction, long>, ISysMenuActionRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysMenuActionRepository(IDbContextProvider<NetCoreFrameDbContext> dbcontext) : base(dbcontext)
        {

        }

        /// <summary>
        /// 新增动作列表
        /// </summary>
        /// <returns></returns>
        public void AddMenusAction(List<SysMenuAction> modelList)
        {
            foreach (var m in modelList)
            {
                base.Insert(m);
            }
        }

        /// <summary>
        /// 更新动作列表
        /// 1.modelList 为空 将移除所有该模块动作数据
        /// 2.新增修改不会调整原有数据主键ID
        /// 3.modelList 集合的数据中数据库不存在将进行删除
        /// </summary>
        /// <param name="modelList"></param>
        /// <param name="menuId"></param>
        public void UpdataMenusAction(List<SysMenuAction> modelList, long menuId)
        {
            //如果没有集合就清空原有数据
            if (modelList == null || !modelList.Any())
            {
                DelMenusAction(menuId);
                return;
            }
           
            //首先新增
            foreach (var m in modelList)
            {
                base.InsertOrUpdate(m);
            }
            //读取数据
            var delList = base.GetAll().Where(p => p.MenuID == menuId).ToList();
            //判断删除(数据库存在但是页面没有传递的对象将被删除,避免导致数据主键Id变更的情况)
            foreach (var delItem in delList)
            {
                var isDel = modelList.Where(w => w.Id == delItem.Id);
                if (!isDel.Any())
                {
                    base.Delete(delItem.Id);
                }
            }
        }
         
        /// <summary>
        /// 删除模块动作列表
        /// </summary>
        /// <param name="menuId"></param>
        public void DelMenusAction(long menuId)
        {
            var delList = base.GetAll().Where(p => p.MenuID == menuId);
            foreach (var item in delList)
            {
                base.Delete(item);
            }
        }

        /// <summary>
        /// 新增角色授权的菜单以及动作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //public int AddRoleToMenuAction(List<SysMenus> model, long roleID)
        public int AddRoleToMenuAction(List<SysMenus> model, long roleID)
        {
            foreach (var item in model)
            {
                #region 保存菜单授权
                if (item.IsCheck)
                {
                    //保存菜单授权
                    base.Context.SysRoleToMenuActions.Add(new SysRoleToMenuAction()
                    {
                        RoleID = roleID,
                        MenuID = item.Id,
                        IsMenu = true,
                    });
                }
                #endregion

                #region 保存动作授权
                foreach (var subAction in item.SysMenuActions)
                {
                    if (subAction.IsCheck)
                    {
                        //保存动作授权
                        base.Context.SysRoleToMenuActions.Add(new SysRoleToMenuAction()
                        {
                            RoleID = roleID,
                            MenuActionID = subAction.Id,
                            MenuID = subAction.MenuID,
                            IsMenu = false
                        });
                    }
                }
                #endregion
            }
            return base.Context.SaveChanges();
        }

        /// <summary>
        /// 删除角色授权的菜单以及动作
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int DelRoleToMenuAction(long roleId)
        {
            //获取角色已授权的菜单以及动作
            var data = base.Context.SysRoleToMenuActions.Where(p => p.RoleID == roleId);
            foreach (var item in data)
            {
                base.Context.SysRoleToMenuActions.Remove(item);
            }
            return base.Context.SaveChanges();
        }

        /// <summary>
        /// 获取当前角色授权的菜单以及动作
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IQueryable<SysRoleToMenuAction> GetMenuActionByRole(long roleId)
        {
            var data = base.Context.SysRoleToMenuActions.Where(p => p.RoleID == roleId);
            return data;
        }

        /// <summary>
        /// 获取所有模块以及动作请求的授权信息
        /// </summary>
        /// <returns></returns>
        public List<MenuActionPermissionCache> GetAllPermissionName()
        {
            return
                 (
                 from sma in base.Context.SysMenuActions
                 join sm in base.Context.SysMenuss on sma.MenuID equals sm.Id
                 select new MenuActionPermissionCache
                 {
                     MenuName = sm.MenuName,
                     MenuDisplayName = sm.MenuDisplayName,
                     MenuId = sm.Id,
                     ActionName = sma.ActionName,
                     ActionDisplayName = sma.ActionDisplayName,
                     ActionId = sma.Id,
                     PermissionName = sma.PermissionName,
                     RequiresAuthModel = sma.RequiresAuthModel,
                     IsActive = sma.IsActive.Value,
                     IsMenu = false
                 })
                 .Union(
                  from sm in base.Context.SysMenuss
                  select new MenuActionPermissionCache
                  {
                      MenuName = sm.MenuName,
                      MenuDisplayName = sm.MenuDisplayName,
                      MenuId = sm.Id,
                      ActionName = string.Empty,
                      ActionDisplayName = string.Empty,
                      ActionId = null,
                      PermissionName = sm.PermissionName,
                      RequiresAuthModel = sm.RequiresAuthModel,
                      IsActive = sm.IsActive.Value,
                      IsMenu = true
                  }
                 ).ToList();
        }

    }
}
