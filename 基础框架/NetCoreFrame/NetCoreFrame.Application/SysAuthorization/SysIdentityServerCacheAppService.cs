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
    [AbpAuthorize]
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
            //获取有效的客户以及相关资源关系对象
            apiClientToResourceList = apiClientToResourceList.Where(w => w.IsActiveClient.Value && w.IsActiveResource.Value).ToList();
            //构造客户端对象
            foreach (var clientItem in apiClientToResourceList)
            {
                Client client = new Client();
                //客户ID
                client.ClientId = clientItem.ClientId;
                client.ClientName = "客户端名称(" + clientItem.ClientId + ")";
                //设置秘钥类型 可以在 ISecretValidator 实现具体解密方式
                client.ClientSecrets = new[] {
                        new Secret() {
                            //Value = client.ClientSecrets.Sha256(),
                            Value = clientItem.ClientSecrets,
                            Type = IdentityServerConstants.SecretTypes.SharedSecret
                        }
                    };

                //身份认证服务器常量
                client.AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        //IdentityServerConstants.StandardScopes.Email,
                        //IdentityServerConstants.StandardScopes.Address,
                        //IdentityServerConstants.StandardScopes.Phone,
                        //IdentityServerConstants.StandardScopes.OfflineAccess,
                        clientItem.ResourceName
                    };
                //授权token有效期 （单位/小时 转换 单位/秒）
                client.AccessTokenLifetime = clientItem.AccessTokenLifetime.Value * 60 * 60;
                //产生刷新令牌
                client.AllowOfflineAccess = clientItem.AllowOfflineAccess.Value;
                //设置刷新令牌有效时间（单位/小时 转换 单位/秒）
                client.SlidingRefreshTokenLifetime = clientItem.SlidingRefreshTokenLifetime.Value * 60 * 60;
                //设置刷新令牌将在固定的时间点过期
                client.RefreshTokenExpiration = TokenExpiration.Sliding;

                //支持客户端验证同时支持客户端与密码验证 
                //ToDo 此处针对自定义 GrantType 可以进行扩展并添加到授权类型中
                //ToDo 默认添加自定义 GrantType 方式 示例：new[] { "demo_validation" } 
                //ToDo 默认针对每个资源相关的客户添加单点登录授权 GrantType.Implicit, 
                client.AllowedGrantTypes = new[] { GrantType.ClientCredentials, GrantType.ResourceOwnerPassword, GrantType.Implicit, "demo_validation" };

                //ToDo 设置单点登录相关的客户端回发以及注销地址
                client.RedirectUris = new[] { "http://localhost:4838/signin-oidc", "http://localhost:44077/signin-oidc" };
                client.PostLogoutRedirectUris = new[] { "http://localhost:4838/signout-callback-oidc", "http://localhost:4838/signout-callback-oidc" };
                client.RequireConsent = false;

                clients.Add(client);
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
            var apiResourceList = _sysApiResourceRepository.GetAllList(w => w.IsActive);
            //
            List<ApiResource> resourceList = new List<ApiResource>();
            //
            if (apiResourceList == null && !apiResourceList.Any())
            {
                return resourceList;
            }
            //
            foreach (var resource in apiResourceList)
            {
                resourceList.Add(new ApiResource(resource.ResourceName, resource.ResourceDisplayName));
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
