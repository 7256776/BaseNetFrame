using Abp.WebApi.Controllers;
using Frame.Core;

namespace Frame.WebApi
{
    public abstract class FrameExtAbpApiController  : AbpApiController
    {
        protected FrameExtAbpApiController()
        {
            LocalizationSourceName = ConstantConfig.LocalizationName;
        }
    }
}