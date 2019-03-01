using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAuthService
{
    /// <summary>
    /// 注册一个IResourceStore装饰器实现，它将维护IdentityResource和ApiResource配置对象的内存缓存。 
    /// 缓存持续时间可以在IdentityServerOptions上的缓存配置选项上配置。
    /// 
    /// 分别支持 AddResourceStore与AddResourceStoreCache
    /// 注:AddResourceStoreCache需要添加AddInMemoryCaching
    /// </summary>
    public class ResourceStore : IResourceStore
    {
        public ResourceStore()
        {
        }

        public List<IdentityResource> identityResource = new List<IdentityResource> {
                new IdentityResources.OpenId(), //必须要添加，否则报无效的scope错误
                new IdentityResources.Profile()
            };

        public List<ApiResource> apiResource = new List<ApiResource> {
                new ApiResource("apiA", "My API")
            };

        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            return await Task.FromResult<ApiResource>(null);
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            return await Task.FromResult<Resources>(new Resources(identityResource, apiResource));
        }

        /// <summary>
        ///  需要移除内存中添加
        ///  .AddInMemoryIdentityResources()
        ///  .AddInMemoryApiResources()
        /// </summary>
        /// <param name="scopeNames"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            
            return await Task.FromResult<IEnumerable<ApiResource>>(apiResource);
        }

        /// <summary>
        ///  需要移除内存中添加
        ///  .AddInMemoryIdentityResources()
        ///  .AddInMemoryApiResources()
        /// </summary>
        /// <param name="scopeNames"></param>
        /// <returns></returns>
        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return await Task.FromResult<IEnumerable<IdentityResource>>(identityResource);
        }

    }


}
