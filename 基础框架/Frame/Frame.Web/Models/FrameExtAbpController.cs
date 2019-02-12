using Abp.Web.Mvc.Controllers;
using Frame.Core;

namespace Frame.Web
{
    public class FrameExtAbpController : AbpController 
    {
        protected FrameExtAbpController()
        {
            LocalizationSourceName = ConstantConfig.LocalizationName;
        }
        /// <summary>
        /// 替换Abp的AbpSession
        /// </summary>
        public new IAbpSessionExtens AbpSession { get; set; }

    }
}