using Abp.Localization;
using Frame.Core;

namespace Frame.Application
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
