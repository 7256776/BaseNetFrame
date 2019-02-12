using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace NetCoreFrame.Core
{
    public class UserClaimsPrincipalFactory : SysUserClaimsPrincipalFactory<UserInfo>
    {
        public UserClaimsPrincipalFactory(
            UserInfoManager userManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(
                  userManager,
                  optionsAccessor)
        {
        }
    }
}
