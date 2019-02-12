using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Web.Models;
using Frame.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frame.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserInfoAppService : IApplicationService
    {
        /// <summary>
        /// 登录授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<SysLoginResult<UserInfo>> LoginAuth(LoginUser model);

        /// <summary>
        /// 验证身份信息并且注册登录凭证
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<SysLoginResult<UserInfo>> VerifyAuthAndSignIn(LoginUser model);

        /// <summary>
        /// 注册身份验证的属性
        /// </summary>
        /// <param name="model"></param>
        /// <param name="resLoginUser"></param>
        [IgnoreFrameApi]
        void SetAuthenticationProperties(LoginUser model, SysLoginResult<UserInfo> resLoginUser);

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        AjaxResponse SignOut();

        /// <summary>
        /// 查询用户集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        PagedResultDto<UserOut> GetUserList(UserQuery model, PagingDto pagingDto);

        /// <summary>
        /// 获取用户对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserOut GetUserModel(long id);

        /// <summary>
        /// 保存用户信息(新增,修改)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<AjaxResponse> SaveUserModel(UserInput model);

        /// <summary>
        /// 删除用户对象(逻辑删除)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task DelUserModel(List<UserInput> model);

        /// <summary>
        /// 获取登录用户所有授权模块以及动作
        /// </summary>
        /// <returns></returns>
        object GetUserPermission();

        /// <summary>
        /// 保存用户自己维护的个人信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<AjaxResponse> SeetingUserInfo(UserInfoInput model);

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<AjaxResponse> UpdateUserPass(UserPassInput model);

        /// <summary>
        /// 设置用户图像
        /// </summary>
        /// <param name="imgId"></param>
        /// <returns></returns>
        Task<AjaxResponse> SaveAvatars(string imgId);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回重置后的密码</returns>
        Task<AjaxResponse> ResetUserPass(long id);
    }
}
