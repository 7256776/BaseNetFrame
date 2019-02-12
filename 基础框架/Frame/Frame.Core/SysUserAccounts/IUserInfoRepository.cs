using Abp.Domain.Repositories;

namespace Frame.Core
{
    /// <summary>
    /// 用户管理仓储接口
    /// </summary>
    public interface IUserInfoRepository : IRepository<UserInfo, long>
    {
        /// <summary>
        /// 根据用户id获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        UserInfo GetUserInfo(long userId);

        /// <summary>
        /// 根据用户账号获取用户对象
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        UserInfo GetUserInfoByUserCode(string userCode);

    }
}
