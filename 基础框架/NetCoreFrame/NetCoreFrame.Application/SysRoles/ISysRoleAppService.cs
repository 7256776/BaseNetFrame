using Abp.Application.Services;
using Abp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.Application
{
    public interface ISysRoleAppService : IApplicationService
    {
        /// <summary>
        /// 查询角色集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<RoleOut> GetRoleList();

        /// <summary>
        /// 查询角色对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RoleOut GetRoleModel(long id);

        /// <summary>
        /// 保存角色(新增,修改)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<AjaxResponse> SaveRoleModel(RoleInput model);

        /// <summary>
        /// 获取角色授权用户以及带授权用户集合
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        RoleToUserReturn GetRoleToUser(long roleId);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="model"></param>
        void DelRoleModel(List<RoleInput> model);

        /// <summary>
        /// 保存角色授权模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        AjaxResponse SaveRoleToMenu(RoleInput model);

        /// <summary>
        /// 保存角色用户授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        AjaxResponse SaveRoleToUser(RoleToUser model);
    }
}
