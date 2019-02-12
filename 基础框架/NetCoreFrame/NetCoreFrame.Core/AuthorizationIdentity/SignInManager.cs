using Abp.Configuration;
using Abp.Domain.Uow;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NetCoreFrame.Core
{
    public class SignInManager : SysSignInManager<UserInfo>
    {
        public SignInManager(
            IUserInfoRepository userInfoRepository,
            UserInfoManager userManager,
            SysUserInfoStore<UserInfo> sysInUserInfoStore,
            UserClaimsPrincipalFactory claimsFactory,
            IHttpContextAccessor contextAccessor,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<UserInfo>> logger,
            IAuthenticationSchemeProvider schemes)
            : base(
                userInfoRepository,
                userManager,
                sysInUserInfoStore,
                claimsFactory,
                contextAccessor,
                optionsAccessor,
                logger,
                schemes)
        {
        }
    }
}
