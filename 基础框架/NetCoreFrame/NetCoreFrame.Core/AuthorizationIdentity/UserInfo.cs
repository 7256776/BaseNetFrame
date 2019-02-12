using Microsoft.AspNetCore.Identity;

namespace NetCoreFrame.Core
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
                Id = 1,
                UserCode = "sys",
                UserNameCn = "管理员",
                ImageUrl = "m",
                Sex = "1",
                IsActive = true,
                IsDeleted = false,
                IsAdmin = true,
                Password = new PasswordHasher<UserInfo>().HashPassword(null, ConstantConfig.DefaultPassword)
            };
        }
    }

}
