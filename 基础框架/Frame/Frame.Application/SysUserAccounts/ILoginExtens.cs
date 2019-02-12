using Frame.Core;
using System.Threading.Tasks;

namespace Frame.Application
{
    /// <summary>
    /// 获取所有授权许可
    /// </summary>
    public interface ILoginExtens
    {
        /// <summary>
        /// 重构登录验证
        /// </summary>
        /// <param name="context"></param>
        Task<SysLoginResult<UserInfo>> LoginRequest(LoginUser model, string returnUrl);

    }

    public interface ILogoutExtens
    {
        /// <summary>
        /// 重构注销中
        /// </summary>
        /// <param name="context"></param>
        void LogoutExtension();

    }
}
