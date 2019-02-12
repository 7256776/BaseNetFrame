using Abp.Auditing;
using Abp.Web.Mvc.Authorization;
using Frame.Application;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Frame.Web
{
    [AbpMvcAuthorize]
    [DisableAuditing]
    public class J_RoleController : FrameExtAbpController
    {
        private readonly ISysRoleAppService _sysRoleManager;
        private readonly ISysMenusAppService _sysMenusManager;
         
         
        public J_RoleController(
            ISysRoleAppService sysRoleManager, 
            ISysMenusAppService sysMenusManager 
            )
        {
            _sysRoleManager = sysRoleManager;
            _sysMenusManager = sysMenusManager;
             
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
        public JsonResult GetRoleList(RoleOut model)
        {
            var data = _sysRoleManager.GetRoleList(model);
            return Json(data);
        }

        /// <summary>
        /// 查询角色对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("RoleManager")]
        public JsonResult GetRoleModel(long id)
        {
            var data = _sysRoleManager.GetRoleModel(id);
            return Json(data);
        }

        /// <summary>
        /// 查询授权菜单集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult GetMenusListOrderByRole(long roleId)
        {
            List<MenusOut> data = _sysMenusManager.GetMenusListOrderBy(roleId);
            return Json(data);
        }

        /// <summary>
        /// 查询授权用户集合
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public JsonResult GetRoleToUser(long roleId)
        {
            RoleToUserReturn data = _sysRoleManager.GetRoleToUser(roleId);
            return Json(data);
        }

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("RoleManager.SaveRole")]
        public async Task<JsonResult> SaveRoleModel(RoleInput model)
        {
            var ajaxResponse = await _sysRoleManager.SaveRoleModel(model);
            return Json(ajaxResponse);
        }

        /// <summary>
        /// 删除角色对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("RoleManager.DelRole")]
        public JsonResult DelRoleModel(List<RoleInput> model)
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
        public JsonResult SaveRoleToMenu(RoleInput model)
        {
            var ajaxResponse = _sysRoleManager.SaveRoleToMenu(model);
            return Json(ajaxResponse);
        }

        /// <summary>
        /// 保存授权用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("RoleManager.SaveRoleToUser")]
        public JsonResult SaveRoleToUser(RoleToUser model)
        {
            var ajaxResponse = _sysRoleManager.SaveRoleToUser(model);
            return Json(ajaxResponse);
        }

    }
}