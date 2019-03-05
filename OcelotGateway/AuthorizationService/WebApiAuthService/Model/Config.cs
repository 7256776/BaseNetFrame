using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.ResponseHandling;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApiAuthService
{
    public class Config
    {
        /// <summary>
        /// 添加基于IResourceStore的IdentityResource的内存集合注册实现，以注册身份验证资源。
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResourceResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(), //必须要添加，否则报无效的scope错误
                new IdentityResources.Profile()
            };
        }

        /// <summary>
        /// 添加基于IResourceStore的ApiResource的内存集合注册实现，以注册API资源。
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("apiA", "My API")
            };
        }

        /// <summary>
        /// 添加基于IClientStore和ICorsPolicyService的内存集合注册实现，以注册客户端配置对象。
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {
                        "apiA",
                        //IdentityServerConstants.StandardScopes.OpenId, //必须要添加，否则报forbidden错误
                        //IdentityServerConstants.StandardScopes.Profile
                    },
                    AccessTokenLifetime=11,
                },
                // resource owner password grant client
                new Client
                {
                    ClientId = "clientCode",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secretPass".Sha256())
                    },
                    AllowedScopes = {
                        "apiA",
                        //IdentityServerConstants.StandardScopes.OpenId, //必须要添加，否则报forbidden错误
                        //IdentityServerConstants.StandardScopes.Profile
                    },
                    AccessTokenLifetime=15,
                }


            };
        }
    }


    /*************************************************************************************************************************************/
    /// <summary>
    /// 实现连接到您的自定义用户配置文件存储。DefaultProfileService`类提供了默认实现，它依靠身份验证cookie作为唯一的令牌发放源。
    /// </summary>
    public class ProfileService : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                //depending on the scope accessing the user data.
                var claims = context.Subject.Claims.ToList();

                //set issued claims to return
                context.IssuedClaims = claims.ToList();
            }
            catch (Exception ex)
            {
                //log your error
            }
            await Task.CompletedTask;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            await Task.CompletedTask;
        }
    }

    /*************************************************************************************************************************************/
    /// <summary>
    /// 实现来自定义重定向URI验证。
    /// </summary>
    public class RedirectUriValidator : IRedirectUriValidator
    {
        public RedirectUriValidator()
        {

        }

        public Task<bool> IsPostLogoutRedirectUriValidAsync(string requestedUri, Client client)
        {
            return Task.FromResult(true);
        }

        public Task<bool> IsRedirectUriValidAsync(string requestedUri, Client client)
        {
            return Task.FromResult(true);
        }
    }

    /*************************************************************************************************************************************/
    /// <summary>
    /// 实现，以在授权端点定制请求参数验证。
    /// </summary>
    public class CustomAuthorizeRequestValidator : ICustomAuthorizeRequestValidator
    {
        public CustomAuthorizeRequestValidator()
        {

        }


        public async Task ValidateAsync(CustomAuthorizeRequestValidationContext context)
        {
            await Task.CompletedTask;
        }
    }

    /*************************************************************************************************************************************/
    /// <summary>
    /// 实现来在授权端点定制逻辑，以便显示用户错误，登录，同意或任何其他自定义页面的UI。 
    /// AuthorizeInteractionResponseGenerator类提供了一个默认的实现，因此如果需要增加现有的行为，可以考虑从这个现有的类派生。
    /// </summary>
    public class AuthorizeInteractionResponseGenerator : IAuthorizeInteractionResponseGenerator
    {
        public AuthorizeInteractionResponseGenerator()
        {

        }

        public async Task<InteractionResponse> ProcessInteractionAsync(ValidatedAuthorizeRequest request, ConsentResponse consent = null)
        {
            return await Task.FromResult<InteractionResponse>(null);
        }
    }

    /*************************************************************************************************************************************/

    public class ClientConfigurationValidator : IClientConfigurationValidator
    {
        public ClientConfigurationValidator()
        {

        }


        public async Task ValidateAsync(ClientConfigurationValidationContext context)
        {
            //context.Client.ClientSecrets
            //设置isvalid为验证是否成功
            //context.IsValid = false;
            await Task.CompletedTask;
        }
    }



    /*************************************************************************************************************************************/
    /// <summary>
    /// 添加用于扩展授权的IExtensionGrantValidator实现。
    /// </summary>
    public class ExtensionGrantValidator : IExtensionGrantValidator
    {
        public ExtensionGrantValidator()
        {

        }

        public string GrantType { get; }

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            await Task.FromResult(true);
        }
    }


    /*************************************************************************************************************************************/
    /// <summary>
    /// 添加用于解析客户端或API资源凭证的ISecretParser实现。
    /// </summary>
    public class SecretParser : ISecretParser
    {
        public SecretParser()
        {

        } 

        public string AuthenticationMethod { get; }

        public async Task<ParsedSecret> ParseAsync(HttpContext context)
        {
            return await Task.FromResult<ParsedSecret>(null);
        }

    }
    /*************************************************************************************************************************************/
    /// <summary>
    /// 添加ISecretValidator实现，以针对凭证存储验证客户端或API资源凭证。
    /// </summary>
    public class SecretValidator : ISecretValidator
    {
        public SecretValidator()
        {

        } 

        public async Task<SecretValidationResult> ValidateAsync(IEnumerable<Secret> secrets, ParsedSecret parsedSecret)
        {
            return await Task.FromResult<SecretValidationResult>(null);
        }
    }

}
