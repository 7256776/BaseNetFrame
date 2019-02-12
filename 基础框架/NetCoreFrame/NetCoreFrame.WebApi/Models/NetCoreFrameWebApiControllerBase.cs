using Abp.AspNetCore.Mvc.Controllers;
using NetCoreFrame.Core;

namespace NetCoreFrame.WebApi
{ 
    public class NetCoreFrameWebApiControllerBase : AbpController 
    {
        protected NetCoreFrameWebApiControllerBase()
        {
            LocalizationSourceName = ConstantConfig.LocalizationName;
        }
        /// <summary>
        /// 替换Abp的AbpSession
        /// </summary>
        public new IAbpSessionExtens AbpSession { get; set; }

    }
}