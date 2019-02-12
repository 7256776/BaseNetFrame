namespace Frame.Core
{
    /// <summary>
    /// 实现系统用户信息仓储
    /// </summary>
    public class UserInfoStore : SysUserInfoStore<UserInfo>
    {
        public UserInfoStore(IUserInfoRepository userInfoRepository) : base(userInfoRepository) { }
    }

}
