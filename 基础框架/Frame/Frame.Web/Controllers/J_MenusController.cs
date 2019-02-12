using Abp.Auditing;
using Abp.Web.Mvc.Authorization;
using Frame.Application;
using Frame.Core;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Frame.Web
{
    [AbpMvcAuthorize]
    [DisableAuditing]
    public class J_MenusController : FrameExtAbpController
    {
        private readonly ISysMenusAppService _sysMenusManager;
        private readonly ICacheManagerExtens _cacheManagerExtens;

        public J_MenusController(ISysMenusAppService sysMenusManager, ICacheManagerExtens cacheManagerExtens)
        {
            _sysMenusManager = sysMenusManager;
             _cacheManagerExtens= cacheManagerExtens;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult SideMenu()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult TopMenu()
        {
            return View();
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
        /// 查询集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("MenusManager")]
        public JsonResult GetMenusList()
        {
            List<MenusOut> data = _sysMenusManager.GetMenusList();
            return Json(data);
        }

        /// <summary>
        /// 查询对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("MenusManager")]
        public JsonResult GetMenusModel(long id)
        {
            var data = _sysMenusManager.GetMenusModel(id);
            return Json(data);
        }

        /// <summary>
        /// 保存对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("MenusManager.SaveMenus")]
        public JsonResult SaveMenus(MenusInput model)
        {
            var ajaxResponse = _sysMenusManager.SaveMenusModel(model);
            return Json(ajaxResponse);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("MenusManager.DelMenus")]
        public JsonResult DelMenusModel(long id)
        {
            _sysMenusManager.DelMenusModel(id);
            return Json(true);
        }

        [AbpMvcAuthorize]
        public JsonResult IsPermissionRepeat(string permission, long? id)
        {
            bool res = _sysMenusManager.IsPermissionRepeat(permission, id);
            return Json(res);
        }




    }
}