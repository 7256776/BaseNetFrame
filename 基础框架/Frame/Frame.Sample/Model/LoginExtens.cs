using Abp.Dependency;
using Abp.Web.Models;
using Frame.Application;
using Frame.Core;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace Frame.Sample
{
    /*
        1.重写登录验证业务(新建类名LoginExtens)
            实现接口 ILoginExtens 可以完成登录验证的后台业务重写

        2.重写登录页面的布局
            在视图文件夹新建目录 J_Account 并且添加 Login.cshtml 然后登录请求地址 /J_Account/LoginRequest
        
        3.重写登录后的桌面布局
            在视图文件夹新建目录 J_Home 并且添加 DesktopPage.cshtml 以及 DesktopPage.js
    
     */

    /// <summary>
    /// 获取所有授权许可
    /// </summary>
    public class LoginExtens : ILoginExtens, ITransientDependency
    {
        private readonly IUserInfoAppService _userInfoAppService;


        public LoginExtens(IUserInfoAppService userInfoAppService)
        {
            _userInfoAppService = userInfoAppService;
        }

        /// <summary>
        /// 验证登录用户
        /// </summary>
        /// <param name="context"></param>
        public async Task<SysLoginResult<UserInfo>> LoginRequest(LoginUser model, string returnUrl)
        {
            //验证用户信息
            SysLoginResult<UserInfo> result = await _userInfoAppService.LoginAuth(model);
            //返回登录
            return result;
        }

    }

    /// <summary>
    /// 重写注销
    /// </summary>
    public class LogoutExtens : ILogoutExtens, ITransientDependency
    {
        /// <summary>
        /// 重构注销中扩展
        /// </summary>
        /// <param name="context"></param>
        public void LogoutExtension()
        {
            /*此处实现注销前事件*/
        }
    }


}
