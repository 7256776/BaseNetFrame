using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Rewrite.Internal;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NetCoreFrame.Web
{
    /// <summary>
    /// 解决http跳转到https中间件
    /// 摘至 http://www.cnblogs.com/dudu/p/7044923.html
    /// </summary>
    public class RedirectToProxiedHttpsRule : RedirectToHttpsRule
    {
        public RedirectToProxiedHttpsRule()
        {
            base.StatusCode = StatusCodes.Status301MovedPermanently;
            base.SSLPort = null;
        }

        /// <summary>
        /// 设置规则
        /// </summary>
        /// <param name="context"></param>
        public override void ApplyRule(RewriteContext context)
        {
            var key = "X-Forwarded-Proto";
            var request = context.HttpContext.Request;
            if (request.Headers.ContainsKey(key))
            {
                if (request.Headers[key].FirstOrDefault() == "http")
                {
                    base.ApplyRule(context);
                }
            }
        }
    }

    /// <summary>
    /// 扩展
    /// </summary>
    public static class RewriteOptionsExtensions
    {
        public static RewriteOptions AddRedirectForwardedHttpToHttps(this RewriteOptions options)
        {
            options.Rules.Add(new RedirectToProxiedHttpsRule());
            return options;
        }
    }

}
