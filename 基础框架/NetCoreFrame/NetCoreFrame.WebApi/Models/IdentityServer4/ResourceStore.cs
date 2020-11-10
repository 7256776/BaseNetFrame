using IdentityServer4.Models;
using IdentityServer4.Stores;
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
        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            IEnumerable<ApiResource> identityResourceList = GetApiResource();

            var data = identityResourceList.Where(w => name.Contains(w.Name));

            if (data.Any())
            {
                return Task.FromResult(data.ToList()[0]);
            }
            return Task.FromResult<ApiResource>(null);
        }

        /// <summary>
        /// 查询作用域资源
        /// </summary>
        /// <param name="scopeNames">请求用户所包含的AllowedScopes</param>
        /// <returns></returns>
        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            IEnumerable<ApiResource> identityResourceList = GetApiResource();

            var data = identityResourceList.Where(w => scopeNames.Contains(w.Name));

            return Task.FromResult(data);
        }

        /// <summary>
        /// 查询身份资源
        /// </summary>
        /// <param name="scopeNames">请求用户所包含的AllowedScopes</param>
        /// <returns></returns>
        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            IEnumerable<IdentityResource> identityResourceList = GetIdentityResources();

            var data = identityResourceList.Where(w => scopeNames.Contains(w.Name));

            return Task.FromResult(identityResourceList);
        }

        /// <summary>
        /// 资源服务器请求资源的初次
        /// 获取全部资源
        /// </summary>
        /// <returns></returns>
        public Task<Resources> GetAllResourcesAsync()
        {
            Resources Resources = new Resources();

            List<ApiResource> apiResourceList = GetApiResource();
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
        /// 
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var customProfile = new IdentityResource(
              name: "Custom.Identity",
              displayName: "Custom Identity",
              claimTypes: new[] { "MyName", "MyEmail" });
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
