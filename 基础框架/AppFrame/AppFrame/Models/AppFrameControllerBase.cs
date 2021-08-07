using Abp.AspNetCore.Mvc.Controllers;
using NetCoreFrame.Core;

namespace AppFrame.Controllers
{ 
    /// <summary>
    /// 控制器基类,所有控制需继承该类
    /// </summary>
    public class AppFrameControllerBase : AbpController 
    {
        protected AppFrameControllerBase()
        {
            LocalizationSourceName = ConstantConfig.LocalizationName;
        }
        /// <summary>
        /// 替换Abp的AbpSession
        /// </summary>
        public new IAbpSessionExtens AbpSession { get; set; }

    }
}