using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreFrame.Core
{
    public interface ISysRolesRepository : IRepository<SysRoles, long>
    {
        /// <summary>
        /// 获取角色未授权的用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IQueryable GetRoleNotToUSer(long roleId);

        /// <summary>
        /// 获取角色已经授权的用户
        /// (改方法需要ToList后才能继续使用)
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IQueryable<SysRoleToUser> GetRoleToUSer(long roleId);

        /// <summary>
        /// 删除角色所授权的所有用户
        /// </summary>
        /// <param name="roleId"></param>
        void DelRoleToUser(long roleId);

        /// <summary>
        /// 添加角色授权用户
        /// </summary>
        /// <param name="model"></param>
        void AddRoleToUser(List<SysRoleToUser> model);

        /// <summary>
        /// 获取制定角色的授权
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
         List<RoleToPermissionCache>  GetRoleToMenuActions(long roleId);

    }
}
