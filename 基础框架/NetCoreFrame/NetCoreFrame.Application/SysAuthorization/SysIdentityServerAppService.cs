using Abp.Auditing;
using Abp.Authorization;
using Abp.Runtime.Caching;
using Abp.UI;
using IdentityServer4;
using IdentityServer4.Models;
using NetCoreFrame.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 授权资源
    /// </summary>
    [Audited]
    public class SysIdentityServerCacheAppService : NetCoreFrameApplicationBase, ISysIdentityServerCacheAppService
    {
        private readonly ISysApiResourceRepository _sysApiResourceRepository;
        private readonly ISysApiClienToAccountRepository _sysApiClienToAccountRepository;
        private readonly ISysApiResourceToClientRepository _sysApiResourceToClientRepository;
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysApiResourceRepository"></param>
        /// <param name="sysApiAccountRepository"></param>
        /// <param name="sysApiClientRepository"></param>
        /// <param name="cacheManager"></param>
        public SysIdentityServerCacheAppService(
            ISysApiResourceRepository sysApiResourceRepository,
            ISysApiClienToAccountRepository sysApiClienToAccountRepository,
            ISysApiResourceToClientRepository sysApiResourceToClientRepository,
            ICacheManager cacheManager
        )
        {
            _sysApiResourceRepository = sysApiResourceRepository;
            _sysApiClienToAccountRepository = sysApiClienToAccountRepository;
            _sysApiResourceToClientRepository = sysApiResourceToClientRepository;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// 所有通知类型 
        /// </summary>
        private const string ALL_CLIENT_TO_RESOURCE = "ALL_CLIENT_TO_RESOURCE";

        /// <summary>
        /// 所有资源 
        /// </summary>
        private const string ALL_RESOURCE = "ALL_RESOURCE";

        /// <summary>
        /// 所有账号与客户关系
        /// </summary>
        private const string ALL_ACCOUNT = "ALL_ACCOUNT";


        #region 客户与资源关系

        /// <summary>
        /// 获取所有客户与资源关系
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Client> GetClientCache()
        {
            return _cacheManager
                    .GetCache<string, List<Client>>(ALL_CLIENT_TO_RESOURCE)
                    .Get(ALL_CLIENT_TO_RESOURCE, () =>
                   {
                       return GetAllClients();
                   });
        }

        /// <summary>
        /// 移除所有客户与资源关系
        /// </summary>
        /// <param name="userId"></param>
        public void RemoveClientCache()
        {
            _cacheManager
                .GetCache<string, List<Client>>(ALL_CLIENT_TO_RESOURCE)
                .Remove(ALL_CLIENT_TO_RESOURCE);
        }

        /// <summary>
        /// 获取客户与资源
        /// </summary>
        /// <returns></returns>
        private List<Client> GetAllClients()
        {
            //获取客户与资源关系
            var apiClientToResourceList = _sysApiResourceToClientRepository.GetApiClientAndResource().ToList();

            //
            List<Client> clients = new List<Client>();
            //
            if (apiClientToResourceList == null && !apiClientToResourceList.Any())
            {
                return clients;
            }
            //
            apiClientToResourceList = apiClientToResourceList.Where(w => w.IsActiveClient.Value && w.IsActiveResource.Value).ToList();
            //
            foreach (var client in apiClientToResourceList)
            {
                clients.Add(new Client()
                {
                    ClientId = client.ClientId,
                    ClientSecrets = new[] {
                        new Secret() {
                            //Value = client.ClientSecrets.Sha256(),
                            Value = client.ClientSecrets,                                                                               //客户ID
                            Type = IdentityServerConstants.SecretTypes.SharedSecret                                //设置秘钥类型 可以在 ISecretValidator 实现具体解密方式
                        }
                    },
                    AllowedScopes = new[] { client.ResourceName },                                                       //关联的资源服务器
                    AccessTokenLifetime = client.AccessTokenLifetime.Value * 60 * 60,                           //授权token有效期 （单位/小时 转换 单位/秒）
                    AllowOfflineAccess = client.AllowOfflineAccess.Value,                                               //产生刷新令牌
                    SlidingRefreshTokenLifetime = client.SlidingRefreshTokenLifetime.Value * 60 * 60,    //设置刷新令牌有效时间（单位/小时 转换 单位/秒）
                    RefreshTokenExpiration = TokenExpiration.Sliding,                                                    //设置刷新令牌将在固定的时间点过期

                    //支持客户端验证同时支持客户端与密码验证 
                    //自定义 GrantType 方式 示例：new[] { "demo_validation" } 
                    AllowedGrantTypes = { GrantType.ClientCredentials, GrantType.ResourceOwnerPassword },
                });
            }
            return clients;
        }

        #endregion

        #region 资源

        /// <summary>
        /// 获取所有资源
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<ApiResource> GetResourcesCache()
        {
            return _cacheManager
                    .GetCache<string, List<ApiResource>>(ALL_RESOURCE)
                    .Get(ALL_RESOURCE, () =>
                    {
                        return GetAllResources();
                    });
        }

        /// <summary>
        /// 移除所有资源
        /// </summary>
        /// <param name="userId"></param>
        public void RemoveResourcesCache()
        {
            _cacheManager
                .GetCache<string, List<ApiResource>>(ALL_RESOURCE)
                .Remove(ALL_RESOURCE);
        }

        /// <summary>
        /// 获取客户与资源
        /// </summary>
        /// <returns></returns>
        private List<ApiResource> GetAllResources()
        {
            //获取客户与资源关系
            var apiResourceList = _sysApiResourceRepository.GetAllList();
            //
            List<ApiResource> resourceList = new List<ApiResource>();
            //
            if (apiResourceList == null && !apiResourceList.Any())
            {
                return resourceList;
            }
            //
            apiResourceList = apiResourceList.Where(w => w.IsActive).ToList();
            //
            foreach (var client in apiResourceList)
            {
                resourceList.Add(new ApiResource(client.ResourceName, client.ResourceDisplayName));

            }
            return resourceList;
        }

        #endregion

        #region 账号

        /// <summary>
        /// 获取所有账号与客户关系
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<SysApiClienToAccountData> GetClientAndAccountCache()
        {
            return _cacheManager
                    .GetCache<string, List<SysApiClienToAccountData>>(ALL_ACCOUNT)
                    .Get(ALL_ACCOUNT, () =>
                    {
                        return GetAllAccount();
                    });
        }

        /// <summary>
        /// 移除所有账号与客户关系
        /// </summary>
        /// <param name="userId"></param>
        public void RemoveClientAndAccountCache()
        {
            _cacheManager
                .GetCache<string, List<SysApiClienToAccountData>>(ALL_ACCOUNT)
                .Remove(ALL_ACCOUNT);
        }

        /// <summary>
        /// 获取账号与客户关系
        /// </summary>
        /// <returns></returns>
        private List<SysApiClienToAccountData> GetAllAccount()
        {
            //获取客户与资源关系
            var apiAccountList = _sysApiClienToAccountRepository.GetApiClientAndAccount().ToList();
            //
            List<ApiResource> resourceList = new List<ApiResource>();
            //
            if (apiAccountList == null && !apiAccountList.Any())
            {
                return new List<SysApiClienToAccountData>();
            }
            //
            apiAccountList = apiAccountList.Where(w => w.IsActiveAccount.Value && w.IsActiveClient.Value).ToList();
            //
            return apiAccountList;
        }

        #endregion



    }
}
