using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebAop.Aop;
using WebApiAuthService;
using WebApiAuthService.Controllers;
using static WebApiAuthService.RSAHelper;

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

        /*

        https://slproweb.com/products/Win32OpenSSL.html   
        下载Win64 OpenSSL v1.1.0j

        配置环境变量
        C:\OpenSSL-Win64\bin

        配置方法
        https://www.jianshu.com/p/1b82f6d2644e
        执行步骤 在CMD中执行以下命令
        1.生成cas.clientservice.key与cas.clientservice.cer  过程中会输入一些证书信息
        openssl req - newkey rsa: 2048 - nodes - keyout cas.clientservice.key - x509 - days 365 -out cas.clientservice.cer
        2. 把cas.clientservice.key与cas.clientservice.cer文件打包成一个文件clientservice.pfx
        openssl pkcs12 - export -in cas.clientservice.cer - inkey cas.clientservice.key -out clientservice.pfx

         */

        private const string _defaultCorsPolicyName = "localhost";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
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

            #region 生成RSE加密证书
            string cerificate = Path.Combine(Environment.CurrentDirectory, Configuration["Cerificates:Cerificate"]);
            string pass = Configuration["Cerificates:Password"];

            //RSA：证书长度2048以上，否则抛异常
            //配置AccessToken的加密证书
            var rsa = new RSACryptoServiceProvider();
            //从配置文件获取加密证书
            rsa.ImportCspBlob(Convert.FromBase64String(Configuration["SigningCredential"]));
            //动态生成加密证书
            //using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(2048))
            //{
            //    string data1 = Convert.ToBase64String(provider.ExportCspBlob(false));   //PublicKey
            //    string data = Convert.ToBase64String(provider.ExportCspBlob(true));    //PrivateKey
            //}
            #endregion

            //刷新token持久化
            //services.AddSingleton<IPersistedGrantStore, PersistedGrantStore>();

            #region 
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = OAuthDefaults.DisplayName;
            //})
            //.AddCookie()
            //.AddOAuth(OAuthDefaults.DisplayName, options =>
            //{
            //    options.ClientId = "oauth.code";
            //    options.ClientSecret = "secret";
            //    options.AuthorizationEndpoint = "https://oidc.faasx.com/connect/authorize";
            //    options.TokenEndpoint = "https://oidc.faasx.com/connect/token";
            //    options.CallbackPath = "/signin-oauth";
            //    options.Scope.Add("openid");
            //    options.Scope.Add("profile");
            //    options.Scope.Add("email");
            //    options.SaveTokens = true;        // 事件执行顺序 ：
            //                                      // 1.创建Ticket之前触发
            //    options.Events.OnCreatingTicket = context => Task.CompletedTask;        // 2.创建Ticket失败时触发
            //    options.Events.OnRemoteFailure = context => Task.CompletedTask;        // 3.Ticket接收完成之后触发
            //    options.Events.OnTicketReceived = context => Task.CompletedTask;        // 4.Challenge时触发，默认跳转到OAuth服务器
            //    // options.Events.OnRedirectToAuthorizationEndpoint = context => context.Response.Redirect(context.RedirectUri);
            //});
            #endregion

            services
            .AddIdentityServer()
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            .AddClientConfigurationValidator<ClientConfigurationValidator>()

            #region 
            //.AddValidators()
            //.AddDefaultSecretValidators()
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
            #endregion

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

            #region 内存授权配置文件
            //.AddInMemoryIdentityResources(Config.GetIdentityResourceResources())
            //.AddInMemoryApiResources(Config.GetApiResources())
            //.AddInMemoryClients(Config.GetClients())
            //添加基于IResourceStore的ApiResource的内存集合注册实现，以注册API资源。
            //.AddTestUsers(new List<TestUser> {
            //        new TestUser{
            //            Username="zjf",
            //            Password="zjf",
            //            SubjectId="1"
            //        }
            //    })
            #endregion
            .AddDeveloperSigningCredential();  //临时证书开发使用
            //.AddSigningCredential(new RsaSecurityKey(rsa)); //设置加密证书
            //.AddSigningCredential(new System.Security.Cryptography.X509Certificates.X509Certificate2(cerificate, pass));    //使用OpenSSL证书

            //DbContextOptionsBuilder
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Server=.; Database=FrameDB; user id=sa;pwd=sa;");
            });


            services.AddTransient<ICustomService, CustomService>();
            services.AddTransient<IIgnoreService, IgnoreService>();

            //services.AddTransient<CustomInterceptorAllAttribute>(provider => new CustomInterceptorAllAttribute("service"));

            #region 注册全局拦截器
            //注册全局拦截器
            services.ConfigureDynamicProxy(config =>
            {
                #region 根据策略忽略拦截器
                //WebAop 命名空间下的Service不会被代理
                //config.NonAspectPredicates.AddNamespace("WebA*");

                //最后一级为 Aop 的命名空间下的Service不会被代理
                //config.NonAspectPredicates.AddNamespace("*.Aop");

                //IIgnoreService接口不会被代理
                //config.NonAspectPredicates.AddService("IIgnoreService");

                //前缀缀为Service的接口和类不会被代理
                //config.NonAspectPredicates.AddService("*Service");

                //命名为 Get 的方法不会被代理
                //config.NonAspectPredicates.AddMethod("GetB");

                //为 Get 前缀的方法不会被代理
                //config.NonAspectPredicates.AddMethod("Get*");
                #endregion

                //通过自定义策略来确定该筛选器所使用的范围
                config.Interceptors.AddTyped<CustomInterceptorAttribute>(new AspectPredicate(IsValidFn));

                config.Interceptors.AddTyped<AppFilterAttribute>(
                     new AspectPredicate[] { new AspectPredicate(IsValidFn2) }
                    );

            });

            #endregion

            return services.BuildDynamicProxyServiceProvider();

        }

        /// <summary>
        /// 自定义策略筛选器
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool IsValidFn(MethodInfo e)
        {
            if (e.Name== "GetB" || e.Name == "GetA" || e.Name == "GetC")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 自定义策略筛选器
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool IsValidFn2(MethodInfo e)
        {
            if (e.Name == "GetB" || e.Name == "GetA" || e.Name == "GetC")
            {
                return false;
            }
            return true;
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
