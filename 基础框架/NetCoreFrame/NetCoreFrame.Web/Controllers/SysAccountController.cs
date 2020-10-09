using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.Web.Controllers
{
    [DisableAuditing]
    public class SysAccountController : NetCoreFrameControllerBase
    {
        private readonly IUserInfoAppService _userInfoAppService;
        private readonly IAccounExtens _accounExtens;
        public SysAccountController(
            IUserInfoAppService userInfoAppService,
            IAccounExtens accounExtens
            )
        {
            _userInfoAppService = userInfoAppService;
            _accounExtens = accounExtens;
        }

        #region 用户管理

        [AbpMvcAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取登录用户所有授权模块以及动作
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public JsonResult GetUserPermission()
        {
            var data = _userInfoAppService.GetUserPermission();
            return Json(data);
        }

        /// <summary>
        /// 查询用户集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("UserInfoManager")]
        public JsonResult GetUserList([FromBody]RequestParam<UserOut> requestParam)
        {
            var data = _userInfoAppService.GetUserList(requestParam);
            return Json(data);
        }

        /// <summary>
        /// 查询用户集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public JsonResult GetAllUserList([FromBody]UserOut model)
        {
            var data = _userInfoAppService.GetAllUserList(model);
            return Json(data);
        }

        /// <summary>
        /// 获取用户对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public JsonResult GetUserModel([FromBody]long id)
        {
            var data = _userInfoAppService.GetUserModel(id);
            return Json(data);
        }

        /// <summary>
        /// 保存用户对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("UserInfoManager.SaveUser")]
        public async Task<JsonResult> SaveUserModel([FromBody]UserInput model)
        {
            var ajaxResponse = await _userInfoAppService.SaveUserModel(model);
            return Json(ajaxResponse);
        }

        /// <summary>
        /// 删除用户对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("UserInfoManager.DelUser")]
        public async Task<JsonResult> DelUserModel([FromBody]List<UserInput> model)
        {
            await _userInfoAppService.DelUserModel(model);
            return Json(true);
        }

        #endregion

        #region 用户设置
        [AbpMvcAuthorize]
        public ActionResult UserSettings()
        {
            return View();
        }

        /// <summary>
        /// 修改用户基础信息
        /// </summary> 
        [AbpMvcAuthorize]
        public async Task<JsonResult> SeetingUserInfo([FromBody]UserInfoInput model)
        {
            var ajaxResponse = await _userInfoAppService.SeetingUserInfo(model);
            return Json(ajaxResponse);
        }

        /// <summary>
        /// 设置用户图像
        /// </summary>
        /// <param name="imgId"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<JsonResult> SaveAvatars([FromBody]string imgId)
        {
            var ajaxResponse = await _userInfoAppService.SaveAvatars(imgId);
            return Json(ajaxResponse);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("UserInfoManager.ResetPass")]
        public async Task<JsonResult> ResetUserPass([FromBody]long id)
        {
            //重置密码 接口实现用户扩展
            var ajaxResponse = await _accounExtens.ResetUserPass(id);
            //原接口调用的业务
            //var ajaxResponse = await _userInfoAppService.ResetUserPass(id);
            return Json(ajaxResponse);
        }
        #endregion


    }
}