using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAuthService
{
    /// <summary>
    /// 注册一个ICorsPolicyService装饰器实现，它将维护一个CORS策略服务评估结果的内存缓存。 
    /// 缓存持续时间可以在IdentityServerOptions上的缓存配置选项上配置。
    /// </summary>
    public class CorsPolicyService : ICorsPolicyService
    {
        public CorsPolicyService()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin">请求地址url</param>
        /// <returns></returns>
        public async Task<bool> IsOriginAllowedAsync(string origin)
        {
            //用于验证请求的跨域地址是否合法,此处可以添加验证
            //此处使用可以取代 app.UseCors()与services.AddCors()
            //bool isCors = false;
            //if (isCors)
            //{
            //    return await Task.FromResult(false);
            //}

            return await Task.FromResult(true);
        }
    }

}
