using Abp.Dependency;
using System.Threading.Tasks;

namespace NetCoreFrame.Application
{
    /// <summary>
    /// 获取所有授权许可
    /// </summary>
    public interface IUserInfoExtens
    {

        /// <summary>
        /// 保存用户基本信息扩展
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdateUserInfo(UserInfoInput model);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserInfoData GetUserModel(long id);

    }

}
