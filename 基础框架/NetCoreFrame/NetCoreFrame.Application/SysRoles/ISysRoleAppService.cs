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
        List<SysRoleData> GetRoleList();

        /// <summary>
        /// 查询角色对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SysRoleData GetRoleModel(long id);

        /// <summary>
        /// 保存角色(新增,修改)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task SaveRoleModel(SysRoleInput model);

        /// <summary>
        /// 获取角色授权用户以及带授权用户集合
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        SysRoleToUserData GetRoleToUser(long roleId);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="model"></param>
        void DelRoleModel(List<SysRoleInput> model);

        /// <summary>
        /// 保存角色授权模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void SaveRoleToMenu(SysRoleInput model);

        /// <summary>
        /// 保存角色用户授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SaveRoleToUser(SysRoleToUserInput model);
    }
}
