using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Uow;

namespace NetCoreFrame.Core
{

    public class SysRolesRepository : EfCoreRepositoryBase<NetCoreFrameDbContext, SysRoles, long>, ISysRolesRepository
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysRolesRepository(IDbContextProvider<NetCoreFrameDbContext> dbcontext) : base(dbcontext)
        {
        }

        /// <summary>
        /// 获取角色未授权的用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IQueryable GetRoleNotToUSer(long roleId)
        {
            var data = base.Context.UserInfos.Where(
                p => !Context.SysRoleToUsers.Where(w => w.RoleID == roleId).Select(s => s.UserID).Contains(p.Id)
                ).Select(s => new  { UserId = s.Id,  UserCode = s.UserCode, UserName = s.UserNameCn });
            return data;
        }

        /// <summary>
        /// 获取角色已经授权的用户
        /// </summary> 
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IQueryable<SysRoleToUser> GetRoleToUSer(long roleId)
        {
            var data = base.Context.UserInfos
             .Join(base.Context.SysRoleToUsers, u => u.Id, r => r.UserID, (u, r) => new { u, r })
             .Where(w => w.r.RoleID == roleId)
             .Select(s => new SysRoleToUser { UserID = s.u.Id,  UserCode = s.u.UserCode, UserName = s.u.UserNameCn });
            return data;
        }

        /// <summary>
        /// 删除角色所授权的所有用户
        /// </summary>
        /// <param name="roleId"></param>
        public void DelRoleToUser(long roleId)
        {
           var data= base.Context.SysRoleToUsers.Where(p => p.RoleID == roleId);
            foreach (var item in data)
            {
                base.Context.SysRoleToUsers.Remove(item);
            }
            int res = base.Context.SaveChanges();
        }

        /// <summary>
        /// 添加角色授权用户
        /// </summary>
        /// <param name="model"></param>
        public void AddRoleToUser(List<SysRoleToUser> model)
        {
            foreach (var item in model)
            { 
                base.Context.SysRoleToUsers.Add(item);
            }
            int res = base.Context.SaveChanges();
        }

        /// <summary>
        /// 获取指定角色的授权
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<RoleToPermissionCache> GetRoleToMenuActions(long roleId)
        {
            //return await Task.Run(() =>
            //{
            //异步执行的内容
            var data = from rm in base.Context.SysRoleToMenuActions
                       join m in base.Context.SysMenuss
                            //on new { MenuId = rm.MenuID, IsMenu = rm.IsMenu } equals new { MenuId = m.Id, IsMenu = true }
                            on rm.MenuID equals m.Id
                            into rmm
                       from t1 in rmm.DefaultIfEmpty()
                       join r in base.Context.SysRoless
                            on rm.RoleID equals r.Id
                            into rmr
                       from t2 in rmr.DefaultIfEmpty()
                       join ma in base.Context.SysMenuActions
                            on new { MenuId = rm.MenuID, MenuActionID = rm.MenuActionID.Value, IsMenu = rm.IsMenu } equals new { MenuId = ma.MenuID, MenuActionID = ma.Id, IsMenu = false }
                            into rmma
                       from t3 in rmma.DefaultIfEmpty()
                       where rm.RoleID == roleId
                       select new RoleToPermissionCache
                       {
                           RoleId = t2.Id,
                           RoleName = t2.RoleName,
                           MenuId = rm.MenuID,
                           MenuName = t1.MenuName,
                           HandleDisplayName = string.IsNullOrEmpty(t3.ActionDisplayName) ? t1.MenuDisplayName : t3.ActionDisplayName,
                           HandleName = string.IsNullOrEmpty(t3.ActionName) ? t1.MenuName : t3.ActionName,
                           PermissionName = rm.IsMenu ? t1.PermissionName : t3.PermissionName,
                           RequiresAuthModel = string.IsNullOrEmpty(t3.RequiresAuthModel) ? t1.RequiresAuthModel : t3.RequiresAuthModel
                       };
            //return data.ToList();
            //});
            return data.ToList();
        }

    }
}
