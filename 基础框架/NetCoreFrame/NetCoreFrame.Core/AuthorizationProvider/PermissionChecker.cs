using Abp.Localization;
using NetCoreFrame.Core;

namespace NetCoreFrame.Application
{
    public class PermissionChecker : PermissionCheckerCore<UserInfo>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        public PermissionChecker(UserInfoManager userManager, ILocalizationManager localizationManager)
          : base(userManager, localizationManager)
            
        {
 
        }

    }
}
