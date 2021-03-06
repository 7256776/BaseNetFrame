﻿using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using NetCoreFrame.Application;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.WebApi
{
    /// <summary>
    /// 获取客户信息
    /// AddClientStoreCache它将维护客户端配置对象的内存缓存。 缓存持续时间可以在IdentityServerOptions上的缓存配置选项上配置 IClientStore装饰器实现，
    /// </summary>
    public class ClientStore : IClientStore
    {
        private ISysIdentityServerCacheAppService _sysIdentityServerCacheAppService;

        public ClientStore(ISysIdentityServerCacheAppService sysIdentityServerCacheAppService)
        {
            _sysIdentityServerCacheAppService = sysIdentityServerCacheAppService;
        }

        /// <summary>
        /// 实现接口获取客户ID
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public Task<Client> FindClientByIdAsync(string clientId)
        {
            List<Client> clients = _sysIdentityServerCacheAppService.GetClientCache();

            var queryClients = clients.Where(w => w.ClientId == clientId);

            if (queryClients.Any())
            {
                return Task.FromResult(queryClients.ToList()[0]);
            }
            return Task.FromResult<Client>(null);
        }

         

        #region 测试数据源

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<Client> GetTestClients()
        {
            List<Client> clients = new List<Client>();
            //
            clients.Add(new Client()
            {
                ClientId = "clientCC",
                ClientSecrets = new[] { new Secret("secretCC".Sha256()) },

                //支持客户端验证 可以配置多个 GrantType  同时支持客户端与密码验证  该方式同理GrantTypes.ClientCredentials.Union(GrantTypes.ResourceOwnerPassword).ToList(),
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = new[] { "ResourceApi" },                                //StandardScopes.OfflineAccess,//如果要获取refresh_tokens ,必须在scopes中加上OfflineAccess
                AllowOfflineAccess = true                                                       //产生刷新令牌
            });
            //
            clients.Add(new Client()
            {
                ClientId = "clientZjf",
                ClientSecrets = { new Secret("secretZjf".Sha256()) },
                AllowOfflineAccess = true,
                AllowedScopes = { "ResourceApi" },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword
            });
            //
            clients.Add(new Client()
            {
                ClientId = "clientAll",
                ClientSecrets = {
                    //new Secret("secretAll".Sha256())
                    new Secret()
                    {
                        Value = "secretAll",
                        Type = IdentityServerConstants.SecretTypes.SharedSecret         //设置秘钥类型 可以在 ISecretValidator 实现具体解密方式
                    },
                    new Secret()
                    {
                        Value = "secretAll1",
                        Type = IdentityServerConstants.SecretTypes.SharedSecret         //设置秘钥类型 解密验证在 ISecretValidator 实现
                    },
                },
                //设置token有效时间
                AccessTokenLifetime = 60,
                //设置RefreshToken有效时间以及有效方式 绝对时间, 该方式根据初次验证完成后获取token的时间作为有效期参照, 后续通过RefreshToken获取的方式不作为参照标准
                //AbsoluteRefreshTokenLifetime = 3,
                //RefreshTokenExpiration = TokenExpiration.Absolute,

                //设置RefreshToken有效时间以及有效方式 相对时间,该方式根据每次RefreshToken获取的时间作为有效期参照
                SlidingRefreshTokenLifetime = 90,
                RefreshTokenExpiration = TokenExpiration.Sliding,

                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowOfflineAccess = true,
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "ResourceApi"
                },
            });
            //
            clients.Add(new Client()
            {
                ClientId = "clientDD",
                ClientSecrets = { new Secret("secretDD".Sha256()) },

                AllowedGrantTypes = new[] { "demo_validation" }, //自定义 GrantType
                AllowOfflineAccess = true,
                AllowedScopes = { "ResourceApi" }
            });

            //单点登录配置
            //Client client = new Client
            //{
            //    ClientId = "clientAll",  //这个client id 跟 MfcImplicit 里面的配置要一致 clientAll
            //    ClientName = "sso implicit clinet",
            //    AllowedGrantTypes = GrantTypes.Implicit,

            //    RedirectUris = { "http://localhost:44077/signin-oidc", "http://localhost:4838/signin-oidc" },
            //    PostLogoutRedirectUris = { "http://localhost:44077/signout-callback-oidc", "http://localhost:4838/signout-callback-oidc" },

            //    RequireConsent = false,
            //    AllowedScopes = new List<string>
            //        {
            //            IdentityServerConstants.StandardScopes.OpenId,
            //            IdentityServerConstants.StandardScopes.Profile,
            //            IdentityServerConstants.StandardScopes.Email,
            //            "ResourceApi"
            //        }
            //};
           
            return clients;
        }

        #endregion



    }
}
