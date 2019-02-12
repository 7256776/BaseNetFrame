using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

namespace NetCoreFrame.Web
{
    public interface IWebResourceManager
    {
        /// <summary>
        /// 获取js引用 
        /// 适用于页面head内加载的js
        /// </summary>
        /// <returns></returns>
        HelperResult RenderTopScripts();

        /// <summary>
        /// 获取js引用
        /// 适用于页面body底部加载的js
        /// </summary>
        /// <returns></returns>
        HelperResult RenderBottomScripts();

        /// <summary>
        /// 获取css引用
        /// </summary>
        /// <returns></returns>
        HelperResult RenderStyles();
    }
}
