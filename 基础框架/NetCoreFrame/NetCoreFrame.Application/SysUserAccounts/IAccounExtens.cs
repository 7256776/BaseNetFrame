using Abp.Dependency;
using Abp.Web.Models;
using NetCoreFrame.Core;
using System.Threading.Tasks;

namespace NetCoreFrame.Application
{
    /// <summary>
    /// 登录、注销、修改密码、
    /// 扩展实现
    /// </summary>
    public interface IAccounExtens
    {
        /// <summary>
        /// 重构登录验证
        /// </summary>
        /// <param name="context"></param>
        Task<SysLoginResult<UserInfo>> LoginRequest(LoginUser model);

        /// <summary>
        /// 个人设置修改密码
        /// </summary>
        /// <param name="context"></param>
        Task<AjaxResponse> UpdateUserPassExtension(UserPassInput model);

        /// <summary>
        /// 用户管理-重置密码
        /// </summary>
        /// <returns></returns>
        Task<AjaxResponse> ResetUserPass(long id);

        /// <summary>
        /// 扩展注销中
        /// </summary>
        /// <param name="context"></param>
        void LogoutExtension();

    }
}
