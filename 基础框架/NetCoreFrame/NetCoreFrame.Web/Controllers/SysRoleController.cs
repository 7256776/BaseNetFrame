using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreFrame.Web.Controllers
{
    [AbpMvcAuthorize]
    [DisableAuditing]
    public class SysRoleController : NetCoreFrameControllerBase
    {
        private readonly ISysRoleAppService _sysRoleManager;
        private readonly ISysMenusAppService _sysMenusManager;
        private readonly IUserInfoAppService _userInfoAppService;

        public SysRoleController(
            ISysRoleAppService sysRoleManager, 
            ISysMenusAppService sysMenusManager,
            IUserInfoAppService userInfoAppService
            )
        {
            _sysRoleManager = sysRoleManager;
            _sysMenusManager = sysMenusManager;
            _userInfoAppService = userInfoAppService;
             
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询角色集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("RoleManager")]
        public JsonResult GetRoleList()
        {
            var data = _sysRoleManager.GetRoleList();
            return Json(data);
        }

        /// <summary>
        /// 查询角色对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("RoleManager")]
        public JsonResult GetRoleModel([FromBody]long id)
        {
            var data = _sysRoleManager.GetRoleModel(id);
            return Json(data);
        }

        /// <summary>
        /// 查询授权菜单集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public JsonResult GetMenusListOrderByRole([FromBody]long roleId)
        {
            List<SysMenusData> data = _sysMenusManager.GetMenusListOrderBy(roleId);
            return Json(data);
        }

        /// <summary>
        /// 查询授权用户集合
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public JsonResult GetRoleToUser([FromBody]long roleId)
        {
            SysRoleToUserData data = _sysRoleManager.GetRoleToUser(roleId);
            return Json(data);
        }

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("RoleManager.SaveRole")]
        public async Task<JsonResult> SaveRoleModel([FromBody]SysRoleInput model)
        {
            await _sysRoleManager.SaveRoleModel(model);
            return Json(true);
        }

        /// <summary>
        /// 删除角色对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("RoleManager.DelRole")]
        public JsonResult DelRoleModel([FromBody]List<SysRoleInput> model)
        {
            _sysRoleManager.DelRoleModel(model);
            return Json(true);
        }

        /// <summary>
        /// 保存授权菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("RoleManager.SaveRoleToMenu")]
        public JsonResult SaveRoleToMenu([FromBody]SysRoleInput model)
        {
            _sysRoleManager.SaveRoleToMenu(model);
            return Json(true);
        }

        /// <summary>
        /// 保存授权用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("RoleManager.SaveRoleToUser")]
        public JsonResult SaveRoleToUser([FromBody]SysRoleToUserInput model)
        {
            var ajaxResponse = _sysRoleManager.SaveRoleToUser(model);
            return Json(true);
        }
        
    }
}