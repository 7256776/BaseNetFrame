using Abp.Application.Services;
using Frame.Core;

namespace Frame.Application
{
    public class FrameExtApplicationService : ApplicationService
    {
        protected FrameExtApplicationService()
        {
            LocalizationSourceName = ConstantConfig.LocalizationName;
        }

        /// <summary>
        /// 替换Abp的AbpSession
        /// </summary>
        public new IAbpSessionExtens AbpSession { get; set; }


    }
}