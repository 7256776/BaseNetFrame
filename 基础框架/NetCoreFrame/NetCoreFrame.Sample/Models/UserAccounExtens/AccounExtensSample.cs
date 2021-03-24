using Abp.Application.Services;
using Abp.Dependency;
using Abp.Web.Models;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using System.Threading.Tasks;

namespace NetCoreFrame.Sample
{

    /// <summary>
    /// 实现登录接口 
    /// 此处不需要使用 ITransientDependency 自动注入,改为手动注入
    /// </summary>
    [RemoteService(false)]
    public class AccounExtensSample : AccounExtens, ITransientDependency
    {

        private readonly IUserInfoAppService _userInfoAppService;

        public AccounExtensSample(IUserInfoAppService userInfoAppService):base(userInfoAppService)
        {
            _userInfoAppService = userInfoAppService;
        }

        /// <summary>
        /// 验证登录用户
        /// </summary>
        /// <param name="context"></param>
        public override async Task<SysLoginResult<UserInfo>> LoginRequest(LoginUserInput model)
        {
            //重写还可以执行原有父类业务
            //var resultData = await base.LoginRequest(model);
            //验证用户信息
            SysLoginResult<UserInfo> result = await _userInfoAppService.LoginAuth(model);
            //返回登录
            return result;
        }

        /// <summary>
        /// 个人设置-修改账户密码
        /// </summary>
        /// <returns></returns>
        public override async Task<AjaxResponse> UpdateUserPassExtension(UserPassInput model)
        {
            /*扩展执行修改密码前的业务*/
            return await base.UpdateUserPassExtension(model);
        }

        /// <summary>
        /// 注销之前的业务
        /// 如不重写该方案,直接执行父类默认业务
        /// </summary>
        //public override void LogoutExtension()
        //{
        //    /*注销前需要执行的业务*/
        //}

    }
}
