using Abp.Application.Services;
using Abp.Web.Models;
using NetCoreFrame.Core;
using System.Threading.Tasks;

namespace NetCoreFrame.Application
{

    /// <summary>
    /// 实现登录接口
    /// 此处不需要使用 ITransientDependency 自动注入,改为手动注入
    /// </summary>
    [RemoteService(false)]
    public class AccounExtens : IAccounExtens
    {

        private readonly IUserInfoAppService _userInfoAppService;

        public AccounExtens(IUserInfoAppService userInfoAppService)
        {
            _userInfoAppService = userInfoAppService;
        }

        /// <summary>
        /// 验证登录用户
        /// </summary>
        /// <param name="context"></param>
        public virtual async Task<SysLoginResult<UserInfo>> LoginRequest(LoginUser model)
        {
            //验证用户信息
            SysLoginResult<UserInfo> result = await _userInfoAppService.LoginAuth(model);
            //返回登录
            return result;
        }

        /// <summary>
        /// 个人设置-修改账户密码
        /// </summary>
        /// <returns></returns>
        public virtual async Task<AjaxResponse> UpdateUserPassExtension(UserPassInput model)
        {
            /*扩展执行修改密码前的业务*/
            return await _userInfoAppService.UpdateUserPass(model);
        }

        /// <summary>
        /// 注销之前的业务
        /// </summary>
        /// <param name="context"></param>
        public virtual void LogoutExtension()
        {
            /*注销前需要执行的业务*/
        }

    }
}
