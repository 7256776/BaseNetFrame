using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Abp.Timing;
using Microsoft.Extensions.Configuration;
using NetCoreFrame.Core;
using Microsoft.Extensions.Options;

namespace NetCoreFrame.Web
{

    /// <summary>
    /// 页面添加 js,css资源引用
    /// 配置网站目录下 customResource.json 文件
    /// </summary>
    public class WebResourceManager : IWebResourceManager
    {

        private readonly CustomWebResource _customWebResource;

        public WebResourceManager(
            IOptions<CustomWebResource> options
           )
        {
            _customWebResource = options.Value;
        }

        /// <summary>
        /// 获取js引用 
        /// 适用于页面head内加载的js
        /// </summary>
        /// <returns></returns>
        public HelperResult RenderTopScripts()
        {
            return new HelperResult(async writer =>
            {
                foreach (var scriptUrl in _customWebResource.ScriptTop)
                {
                    await writer.WriteAsync($"<script src=\"{scriptUrl}?v=" + Clock.Now.Ticks + "\"></script>");
                }
            });
        }

        /// <summary>
        /// 获取js引用
        /// 适用于页面body底部加载的js
        /// </summary>
        /// <returns></returns>
        public HelperResult RenderBottomScripts()
        {
            return new HelperResult(async writer =>
            {
                foreach (var scriptUrl in _customWebResource.ScriptBottom)
                {
                    await writer.WriteAsync($"<script src=\"{scriptUrl}?v=" + Clock.Now.Ticks + "\"></script>");
                }
            });
        }

        /// <summary>
        /// 获取css引用
        /// 加载于页面head内
        /// </summary>
        /// <returns></returns>
        public HelperResult RenderStyles()
        {
            return new HelperResult(async writer =>
            {
                foreach (var styleUrl in _customWebResource.style)
                {
                    await writer.WriteAsync($"<link href=\"{styleUrl}?v=" + Clock.Now.Ticks + "\"  rel = \"stylesheet\" />");
                }
            });
        }


    }
}
