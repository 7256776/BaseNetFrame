using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace WebApiAuthService
{
    /// <summary>
    /// 注册一个IClientStore装饰器实现，它将维护客户端配置对象的内存缓存。 
    /// 缓存持续时间可以在IdentityServerOptions上的缓存配置选项上配置。
    /// 
    /// 
    /// 分别支持 AddClientStore 与 AddClientStoreCache
    /// 注:AddClientStoreCache需要添加AddInMemoryCaching
    /// </summary>
    public class ClientStore : IClientStore
    {
        public ClientStore()
        {

        }

        /// <summary>
        /// 需要移除内存中添加
        /// AddInMemoryClients
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var data = new List<Client>
            {
                new Client
                {  
                    //取消验证 Secret
                    //RequireClientSecret=false,

                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =  {new Secret("secret".Sha256())  },
                    AccessTokenLifetime=15,

                    //AllowOfflineAccess = true,//如果要获取refresh_tokens ,必须把AllowOfflineAccess设置为true
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = 20,
                    AllowedScopes = {
                        "apiA",
                       //PersistedGrantTypes.ReferenceToken,
                       //PersistedGrantTypes.RefreshToken,
                        //StandardScopes.OfflineAccess, //如果要获取refresh_tokens ,必须在scopes中加上OfflineAccess
                        //StandardScopes.OpenId,//如果要获取id_token,必须在scopes中加上OpenId和Profile，id_token需要通过refresh_tokens获取AccessToken的时候才能拿到（还未找到原因）
                        //StandardScopes.Profile//如果要获取id_token,必须在scopes中加上OpenId和Profile
                    },
                },
          
                new Client
                {
                    // resource owner password grant client

                    //取消验证 Secret
                    //RequireClientSecret=false,

                    ClientId = "clientCode",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = {new Secret("secretPass".Sha256())},
                    AccessTokenLifetime=10,

                    AllowOfflineAccess = true,//如果要获取refresh_tokens ,必须把AllowOfflineAccess设置为true
                    RefreshTokenUsage=TokenUsage.OneTimeOnly,//每次发出一个新的刷新令牌

                    //Sliding=每次获取后刷新令牌都更新有效期 (两种方法去其一)
                    RefreshTokenExpiration = TokenExpiration.Sliding,      
                    SlidingRefreshTokenLifetime=360,

                    //Absolute=从获取后就确定了刷新令牌有效期, Sliding=每次获取后刷新令牌都更新有效期
                    //RefreshTokenExpiration = TokenExpiration.Absolute,     
                    //AbsoluteRefreshTokenLifetime = 360,                          
                   
                    AllowedScopes = {
                        "apiA",
                        StandardScopes.OfflineAccess, //如果要获取refresh_tokens ,必须在scopes中加上OfflineAccess
                        //StandardScopes.OpenId,//如果要获取id_token,必须在scopes中加上OpenId和Profile，id_token需要通过refresh_tokens获取AccessToken的时候才能拿到（还未找到原因）
                        //StandardScopes.Profile//如果要获取id_token,必须在scopes中加上OpenId和Profile
                    },

                },
            };
            var dataSub = data.Find(w => w.ClientId == clientId);

            return await Task.FromResult<Client>(dataSub);
        }

    }


}
