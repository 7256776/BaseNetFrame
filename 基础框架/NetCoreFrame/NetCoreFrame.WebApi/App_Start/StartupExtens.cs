using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreFrame.Core;

namespace NetCoreFrame.WebApi
{
    public static class StartupExtens
    {
        /// <summary>
        /// 添加授权中间件
        /// 该注入在 适用于 Configure(IApplicationBuilder app) 中进行
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <param name="schema"></param>
        public static void UseAuthTokenMiddleware(this IApplicationBuilder app, IConfigurationRoot configuration, string schema = JwtBearerDefaults.AuthenticationScheme)
        {
            //oidc
            string authorizationWay = configuration["Authentication:AuthenticationWay"].ToString().ToUpper();
            if (authorizationWay == "OIDC")
            {
                //该中间件包含了UseAuthentication,  该中间件仅在IdentityServer4 授权验证端启动, 资源服务器可以忽略
                app.UseIdentityServer();
            }
            else if (authorizationWay == "JWT")
            {
                app.UseJwtTokenMiddleware(configuration, schema);
                app.UseAuthentication();
            }
        }

        /// <summary>
        /// 添加配置授权验证
        /// 该注入在 适用于 ConfigureServices(IServiceCollection services) 中进行
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddBearerAuthentication(this IServiceCollection services, IConfiguration configuration, string scheme = JwtBearerDefaults.AuthenticationScheme)
        {
            //加载配置文件到实体对象   可通过 IOptions<AuthConfigurerModel> 构造注入
            services.Configure<AuthConfigurerModel>(configuration.GetSection("Authentication"));
            string authorizationWay = configuration["Authentication:AuthenticationWay"].ToString().ToUpper();
            if (authorizationWay == "OIDC")
            {
                #region 在IdentityServer4 授权验证端需要加载的注入, 资源服务器可以忽略

                services
                    .AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddResourceStore<ResourceStore>()
                    .AddClientStore<ClientStore>()
                    .AddSecretValidator<FrameSecretValidator>()
                    .AddExtensionGrantValidator<FrameExtensionGrantValidator>()
                    .AddResourceOwnerValidator<FrameResourceOwnerPasswordValidator>();

                //可扩展 实现授权信息的持久化处理, 默认实现内存方式(InMemoryPersistedGrantStore).
                //services.AddTransient<IPersistedGrantStore, FramePersistedGrantStore>();

                #endregion

                services.AddOidcBearerAuthentication(configuration, scheme);
            }
            else if (authorizationWay == "JWT")
            {
                services.AddJwtBearerAuthentication(configuration, scheme);
            }
        }


    }
}
