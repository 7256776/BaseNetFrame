using Abp.Application.Services;
using NetCoreFrame.Core;

namespace NetCoreFrame.Application
{
    /// <summary>
    /// 应用程序服务 基础类
    /// </summary>
    public abstract class NetCoreFrameApplicationBase : ApplicationService
    {

        protected NetCoreFrameApplicationBase()
        {
            LocalizationSourceName = ConstantConfig.LocalizationName;
        }

        /// <summary>
        /// 替换Abp的AbpSession
        /// </summary>
        public new IAbpSessionExtens AbpSession { get; set; }

    }
}
