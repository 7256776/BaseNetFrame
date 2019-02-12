using Frame.Core;
using System.Threading.Tasks;

namespace Frame.Application
{

    /// <summary>
    /// 获取所有授权许可
    /// 此处不需要使用 ITransientDependency 自动注入,改为手动注入
    /// </summary>
    public class LoginExtens : ILoginExtens
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

    public class LogoutExtens :  ILogoutExtens
    {
        /// <summary>
        /// 重构注销中扩展
        /// </summary>
        /// <param name="context"></param>
        public void LogoutExtension()
        {
            /*重写注销前需要执行的业务*/
        }
    }

}
