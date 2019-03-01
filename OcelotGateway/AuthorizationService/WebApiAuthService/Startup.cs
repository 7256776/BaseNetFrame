﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApiAuthService;

namespace WebApiAuthService
{
    public class Startup
    {

        /*
         * http://localhost:27749/.well-known/openid-configuration
         {
	        "issuer": "http://localhost:27749",
	        "jwks_uri": "http://localhost:27749/.well-known/openid-configuration/jwks",
	        "authorization_endpoint": "http://localhost:27749/connect/authorize",
	        "token_endpoint": "http://localhost:27749/connect/token",
	        "userinfo_endpoint": "http://localhost:27749/connect/userinfo",
	        "end_session_endpoint": "http://localhost:27749/connect/endsession",
	        "check_session_iframe": "http://localhost:27749/connect/checksession",
	        "revocation_endpoint": "http://localhost:27749/connect/revocation",
	        "introspection_endpoint": "http://localhost:27749/connect/introspect",
	        "frontchannel_logout_supported": true,
	        "frontchannel_logout_session_supported": true,
	        "backchannel_logout_supported": true,
	        "backchannel_logout_session_supported": true,
	        "scopes_supported": ["openid", "profile", "api", "offline_access"],
	        "claims_supported": ["sub", "name", "family_name", "given_name", "middle_name", "nickname", "preferred_username", "profile", "picture", "website", "gender", "birthdate", "zoneinfo", "locale", "updated_at"],
	        "grant_types_supported": ["authorization_code", "client_credentials", "refresh_token", "implicit", "password"],
	        "response_types_supported": ["code", "token", "id_token", "id_token token", "code id_token", "code token", "code id_token token"],
	        "response_modes_supported": ["form_post", "query", "fragment"],
	        "token_endpoint_auth_methods_supported": ["client_secret_basic", "client_secret_post"],
	        "subject_types_supported": ["public"],
	        "id_token_signing_alg_values_supported": ["RS256"],
	        "code_challenge_methods_supported": ["plain", "S256"]
        }
         */

        private const string _defaultCorsPolicyName = "localhost";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc(
              options =>
              {
                  //设置跨域筛选器(必须使用 services.AddCors() 设置筛选规则) **方案一 (两种方案选其一)
                  //options.Filters.Add(new CorsAuthorizationFilterFactory(_defaultCorsPolicyName));
              }
          );

            #region  设置跨域访问的规则
            services.AddCors(options => options.AddPolicy(_defaultCorsPolicyName,
                    builder =>
                        builder.WithOrigins(new string[] { "*" })
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                )
            );
            #endregion

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            #region 注释
            /*
            AddSigningCredential
            添加一个签名密钥服务，该服务将指定的密钥提供给各种令牌创建 / 验证服务。 您可以传入X509Certificate2，SigningCredential或对证书存储区中证书的引用。
            AddDeveloperSigningCredential
            在启动时创建临时密钥。 这是仅用于开发场景，当您没有证书使用。 生成的密钥将被保存到文件系统，以便在服务器重新启动之间保持稳定（可以通过传递false来禁用）。 这解决了在开发期间client / api元数据缓存不同步的问题。
            AddValidationKey
            添加验证令牌的密钥。 它们将被内部令牌验证器使用，并将显示在发现文档中。 您可以传入X509Certificate2，SigningCredential或对证书存储区中证书的引用。 这对于关键的转换场景很有用。
            */

            /*
            AddInMemoryClients
            添加基于IClientStore和ICorsPolicyService的内存集合注册实现，以注册客户端配置对象。
            AddInMemoryIdentityResources
            添加基于IResourceStore的IdentityResource的内存集合注册实现，以注册身份验证资源。
            AddInMemoryApiResources
            添加基于IResourceStore的ApiResource的内存集合注册实现，以注册API资源。
            AddTestUsers
            基于TestUserStore的TestUser对象的集合注册实现。 还注册IProfileService和IResourceOwnerPasswordValidator的实现。
             */
            #endregion

            #region  
            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()
            //    .AddInMemoryIdentityResources(new List<IdentityResource>
            //    {
            //        new IdentityResources.Address(),
            //        new IdentityResources.OpenId(),
            //        new IdentityResources.Profile()
            //    })
            //    .AddInMemoryApiResources(new List<ApiResource>
            //    {
            //        new ApiResource("apiA","myapi"),      //api资源定义 scopes
            //        new ApiResource("apiB","myapi")
            //    })
            //    .AddInMemoryClients(new[] {
            //        new Client
            //        {
            //            ClientId="clientCode",
            //            AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
            //            ClientSecrets={
            //                new Secret("secretPass".Sha256())
            //            },
            //            AllowedScopes={"apiA"},
            //        },
            //        new Client
            //        {
            //            ClientId="client",
            //            AllowedGrantTypes=GrantTypes.ClientCredentials,
            //            ClientSecrets={
            //                new Secret("secret".Sha256())
            //            },
            //            AllowedScopes={"apiA"},
            //        }
            //    })
            //    .AddTestUsers(new List<TestUser> {
            //        new TestUser{
            //            Username="zjf",
            //            Password="zjf",
            //            SubjectId="1"
            //        }
            //    }); 
            #endregion


            services
            .AddIdentityServer()
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            .AddClientConfigurationValidator<ClientConfigurationValidator>()

            //添加一个“AppAuth”（OAuth 2.0 for Native Apps）兼容的重定向URI验证器（进行严格的验证，但也允许随机端口为http://127.0.0.1）。
            //.AddAppAuthRedirectUriValidator()
            //.AddRedirectUriValidator<RedirectUriValidator>()

            //使用JWT对客户机认证的支持。
            //.AddJwtBearerClientAuthentication()

            //.AddProfileService<ProfileService>()
            //.AddAuthorizeInteractionResponseGenerator<AuthorizeInteractionResponseGenerator>()
            //.AddCustomAuthorizeRequestValidator<CustomAuthorizeRequestValidator>()
            //.AddCustomTokenRequestValidator<CustomTokenRequestValidator>()
            //.AddExtensionGrantValidator<ExtensionGrantValidator>()
            //.AddSecretParser<SecretParser>()
            //.AddSecretValidator<SecretValidator>()

            #region 要使用下面描述的任何缓存
            //要使用下面描述的任何缓存，必须在DI中注册ICache的实现。 此API注册基于ASP.NET Core的ICache 的MemoryCache默认内存缓存实现。
            //.AddInMemoryCaching()
            //.AddClientStoreCache<ClientStore>()
            //.AddResourceStoreCache<ResourceStore>()
            //.AddCorsPolicyCache<CorsPolicyService>()
            #endregion

            #region 通过数据库获取配置
            .AddCorsPolicyService<CorsPolicyService>()
            .AddClientStore<ClientStore>()
            .AddResourceStore<ResourceStore>()
            #endregion


            //.AddInMemoryIdentityResources(Config.GetIdentityResourceResources())
            //.AddInMemoryApiResources(Config.GetApiResources())
            //.AddInMemoryClients(Config.GetClients())
            //.AddTestUsers(new List<TestUser> {
            //        new TestUser{
            //            Username="zjf",
            //            Password="zjf",
            //            SubjectId="1"
            //        }
            //    })
            .AddDeveloperSigningCredential();

            //DbContextOptionsBuilder
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Server=.; Database=FrameDB; user id=sa;pwd=sa;");
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(_defaultCorsPolicyName);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseIdentityServer();

            //app.UseAuthentication();

            app.UseMvc();
        }
    }
}
