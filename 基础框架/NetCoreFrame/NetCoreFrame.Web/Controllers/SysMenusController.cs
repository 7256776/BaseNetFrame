using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.Web.Controllers
{
    [AbpMvcAuthorize]
    [DisableAuditing]
    public class SysMenusController : NetCoreFrameControllerBase
    {
        private readonly ISysMenusAppService _sysMenusManager;
        private readonly ICacheManagerExtens _cacheManagerExtens;

        public SysMenusController(ISysMenusAppService sysMenusManager, ICacheManagerExtens cacheManagerExtens)
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
        public JsonResult GetMenusModel([FromBody]long id)
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
        public JsonResult SaveMenus([FromBody]MenusInput model)
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
        public JsonResult DelMenusModel([FromBody]long id)
        {
            _sysMenusManager.DelMenusModel(id);
            return Json(true);
        }

        /// <summary>
        /// 查询授权是否重复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public JsonResult IsPermissionRepeat([FromBody]MenuData model)
        {
            bool res = _sysMenusManager.IsPermissionRepeat(model);
            return Json(res);
        }




    }
}