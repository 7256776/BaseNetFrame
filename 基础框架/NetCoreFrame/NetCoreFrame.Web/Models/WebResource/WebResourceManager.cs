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
    /// ҳ����� js,css��Դ����
    /// ������վĿ¼�� customResource.json �ļ�
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
        /// ��ȡjs���� 
        /// ������ҳ��head�ڼ��ص�js
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
        /// ��ȡjs����
        /// ������ҳ��body�ײ����ص�js
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
        /// ��ȡcss����
        /// ������ҳ��head��
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
