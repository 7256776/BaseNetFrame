using Microsoft.AspNet.Identity;

namespace Frame.Core
{
    /// <summary>
    /// 实现用户对象
    /// </summary>
    public class UserInfo : SysUserInfo<UserInfo>
    {
        //设置默认新用户的信息
        public static UserInfo CreateAdminUser()
        {
            return new UserInfo
            {
                UserCode = "admin",
                UserNameCn = "管理员",
                ImageUrl = "m",
                IsActive = true,
                //IsDeleted = false,
                IsAdmin = false,
                Password = new PasswordHasher().HashPassword(ConstantConfig.DefaultPassword)
            };
        }
    }

}
