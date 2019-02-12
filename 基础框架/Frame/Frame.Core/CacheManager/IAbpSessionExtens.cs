using Abp.Runtime.Session;
using System.Collections.Generic;

namespace Frame.Core
{

    /// <summary>
    /// ABPSession扩展
    /// </summary>
    public interface IAbpSessionExtens : IAbpSession
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        string UserNameCn { get; }

        /// <summary>
        /// 用户账号
        /// </summary>
        string UserCode { get; }

        /// <summary>
        /// 用户账号
        /// </summary>
        bool IsAdmin { get; }

        /// <summary>
        /// 用户包含的角色ID
        /// </summary>
        List<string> UserRoleList { get; }

        /// <summary>
        /// 扩展的用户授权信息
        /// </summary>
        string UserAuthInfoExt { get; }

    }
}
