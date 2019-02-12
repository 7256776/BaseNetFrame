using Abp.Authorization;

namespace Frame.Core
{
    /// <summary>
    /// 实现系统用户管理对象
    /// </summary>
    public class UserInfoManager : SysUserInfoManager<UserInfo> 
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfoStore"></param>
        public UserInfoManager(
            UserInfoStore userInfoStore,
             ICacheManagerExtens cacheManagerExtens,
             IPermissionManager permissionManager,
             IAbpSessionExtens abpSessionExtens
            ) : 
            base(
                userInfoStore ,
                cacheManagerExtens,
                permissionManager,
                abpSessionExtens
                )
        {
             
        }

    }
}
