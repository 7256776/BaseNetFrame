using IdentityServer4.Models;
using IdentityServer4.Stores;
using NetCoreFrame.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.WebApi
{

    /// <summary>
    /// 获取资源
    /// </summary>
    public class ResourceStore : IResourceStore
    {

        private ISysIdentityServerCacheAppService _sysIdentityServerCacheAppService;

        public ResourceStore(ISysIdentityServerCacheAppService sysIdentityServerCacheAppService)
        {
            _sysIdentityServerCacheAppService = sysIdentityServerCacheAppService;
        }

        /// <summary>
        /// 查询资源对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            var apiResourceList = _sysIdentityServerCacheAppService.GetResourcesCache();
            var data = apiResourceList.Where(w => name.Contains(w.Name));
            if (data.Any())
            {
                return Task.FromResult(data.ToList()[0]);
            }
            return Task.FromResult<ApiResource>(null);
        }

        /// <summary>
        /// 查询资源集合
        /// </summary>
        /// <param name="scopeNames">请求用户所包含的AllowedScopes</param>
        /// <returns></returns>
        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var apiResourceList = _sysIdentityServerCacheAppService.GetResourcesCache();
            var data = apiResourceList.Where(w => scopeNames.Contains(w.Name));
            return Task.FromResult(data);
        }

        /// <summary>
        /// 查询身份资源
        /// </summary>
        /// <param name="scopeNames">请求用户所包含的AllowedScopes</param>
        /// <returns></returns>
        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            IEnumerable<IdentityResource> identityResourceList = this.GetIdentityResources();
            //
            return Task.FromResult(identityResourceList);
        }

        /// <summary>
        /// 访问资源服务器初次会调用该服务获取相关资源列表
        /// 获取全部资源
        /// </summary>
        /// <returns></returns>
        public Task<Resources> GetAllResourcesAsync()
        {
            var apiResourceList = _sysIdentityServerCacheAppService.GetResourcesCache();
            Resources Resources = new Resources();
            foreach (var item in apiResourceList)
            {
                Resources.ApiResources.Add(item);
            }
            return Task.FromResult(Resources);
        }


        #region 测试数据源

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<ApiResource> GetApiResource()
        {
            List<ApiResource> apiResourceList = new List<ApiResource>();
            apiResourceList.Add(new ApiResource("ResourceApi", "Default (all) API"));
            apiResourceList.Add(new ApiResource("FrameApi", "Default (all) API"));
            return apiResourceList;
        }

        /// <summary>
        /// 获取身份验证资源
        /// </summary>
        /// <returns></returns>
        private IEnumerable<IdentityResource> GetIdentityResources()
        {
            var customProfile = new IdentityResource(
              name: "Frame.Identity",
              displayName: "框架自定义的身份资源标签",
              claimTypes: new[] { "FrameName", "FrameEmail" });
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
                new IdentityResources.Address(),
               customProfile
            };
        }

        #endregion

    }
}
