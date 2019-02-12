using Abp.AspNetCore.Mvc.Controllers;
using NetCoreFrame.Core;

namespace NetCoreFrame.Web
{ 
    public class NetCoreFrameControllerBase : AbpController 
    {
        protected NetCoreFrameControllerBase()
        {
            LocalizationSourceName = ConstantConfig.LocalizationName;
        }
        /// <summary>
        /// 替换Abp的AbpSession
        /// </summary>
        public new IAbpSessionExtens AbpSession { get; set; }

    }
}