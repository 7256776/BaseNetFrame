using Abp.Application.Services;
using System.Threading.Tasks;

namespace NetCoreFrame.Application
{
    /// <summary>
    /// 用户基本信息扩展
    /// 注册放在启动类里面,此处不需要使用 ITransientDependency 自动注入
    /// </summary>
    [RemoteService(false)]
    public class UserInfoExtens : IUserInfoExtens
    {
        public UserOut GetUserModel(long id)
        {
            return null;
        }

        /// <summary>
        /// 保存用户基本信息扩展
        /// </summary>
        /// <param name="context"></param>
        public Task<bool> UpdateUserInfo(UserInput model)
        {
            return Task.FromResult(true);
        }



    }
}
