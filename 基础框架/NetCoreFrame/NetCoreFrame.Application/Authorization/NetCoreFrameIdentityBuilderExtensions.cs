using Abp;
using Abp.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using System;

namespace NetCoreFrame.Application
{
    public interface INetCoreFrameEntityTypes
    {
        /// <summary>
        ///应用程序的用户类型
        /// </summary>
        Type User { get; set; }
    }

    public class NetCoreFrameEntityTypes : INetCoreFrameEntityTypes
    {
        public Type User
        {
            get { return _user; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                if (!typeof(SysUserAccounts).IsAssignableFrom(value))
                {
                    throw new AbpException(value.AssemblyQualifiedName + " 应该来自 " + typeof(SysUserAccounts).AssemblyQualifiedName);
                }
                _user = value;
            }
        }
        private Type _user;
    }

    public class NetCoreFrameIdentityBuilder : IdentityBuilder
    {
        public Type TenantType { get; }

        public NetCoreFrameIdentityBuilder(IdentityBuilder identityBuilder, Type tenantType)
            : base(identityBuilder.UserType, identityBuilder.Services)
        {
            TenantType = tenantType;
        }
    }

    public static class NetCoreFrameIdentityBuilderExtensions
    {
        public static NetCoreFrameIdentityBuilder AddFrameIdentity<TUser>(this IServiceCollection services)
            where TUser : SysUserInfo<TUser>
        {
            return services.AddFrameIdentity<TUser>(setupAction: null);
        }

        /// <summary>
        /// 注入验证授权对象
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static NetCoreFrameIdentityBuilder AddFrameIdentity<TUser>(this IServiceCollection services, Action<IdentityOptions> setupAction)
             where TUser : SysUserInfo<TUser>
        {

            services.AddSingleton<INetCoreFrameEntityTypes>(new NetCoreFrameEntityTypes
            {
                User = typeof(TUser)
            });

            //SysUserInfoManager
            services.TryAddScoped<SysUserInfoManager<TUser>>();
            services.TryAddScoped(typeof(UserManager<TUser>), provider => provider.GetService(typeof(SysUserInfoManager<TUser>)));

            //SysSignInManager
            services.TryAddScoped<SysSignInManager<TUser>>();
            services.TryAddScoped(typeof(SignInManager<TUser>), provider => provider.GetService(typeof(SysSignInManager<TUser>)));

            //SysUserClaimsPrincipalFactory
            services.TryAddScoped<SysUserClaimsPrincipalFactory<TUser>>();
            services.TryAddScoped(typeof(UserClaimsPrincipalFactory<TUser>), provider => provider.GetService(typeof(SysUserClaimsPrincipalFactory<TUser>)));
            services.TryAddScoped(typeof(IUserClaimsPrincipalFactory<TUser>), provider => provider.GetService(typeof(SysUserClaimsPrincipalFactory<TUser>)));

            //SysUserInfoStore
            services.TryAddScoped<SysUserInfoStore<TUser>>();
            services.TryAddScoped(typeof(IUserStore<TUser>), provider => provider.GetService(typeof(SysUserInfoStore<TUser>)));

            return new NetCoreFrameIdentityBuilder(services.AddIdentityCore<TUser>(setupAction), null);
        }

        /// <summary>
        /// 注入用户验证授权对象
        /// </summary>
        /// <typeparam name="TUserManager"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static NetCoreFrameIdentityBuilder AddFrameUserManager<TUserManager>(this NetCoreFrameIdentityBuilder builder)
            where TUserManager : class
        {
            var sysUserInfoManagerType = typeof(SysUserInfoManager<>).MakeGenericType( builder.UserType);
            var userManagerType = typeof(UserManager<>).MakeGenericType(builder.UserType);
            builder.Services.AddScoped(sysUserInfoManagerType, services => services.GetRequiredService(userManagerType));
            builder.AddUserManager<TUserManager>();
            return builder;
        }

        /// <summary>
        /// 注入登录验证对象
        /// </summary>
        /// <typeparam name="TSignInManager"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static NetCoreFrameIdentityBuilder AddFrameSignInManager<TSignInManager>(this NetCoreFrameIdentityBuilder builder)
            where TSignInManager : class
        {
            var sysSignInManagerType = typeof(SysSignInManager<>).MakeGenericType(builder.UserType);
            var signInManagerType = typeof(SignInManager<>).MakeGenericType(builder.UserType);
            builder.Services.AddScoped(sysSignInManagerType, services => services.GetRequiredService(signInManagerType));
            builder.AddSignInManager<TSignInManager>();
            return builder;
        }
 
        /// <summary>
        /// 注入创建授权凭证对象
        /// </summary>
        /// <typeparam name="TUserClaimsPrincipalFactory"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static NetCoreFrameIdentityBuilder AddFrameUserClaimsPrincipalFactory<TUserClaimsPrincipalFactory>(this NetCoreFrameIdentityBuilder builder)
            where TUserClaimsPrincipalFactory : class
        {
            var type = typeof(TUserClaimsPrincipalFactory);
            builder.Services.AddScoped(typeof(UserClaimsPrincipalFactory<>).MakeGenericType(builder.UserType), services => services.GetRequiredService(type));
            builder.Services.AddScoped(typeof(SysUserClaimsPrincipalFactory<>).MakeGenericType(builder.UserType), services => services.GetRequiredService(type));
            builder.Services.AddScoped(typeof(IUserClaimsPrincipalFactory<>).MakeGenericType(builder.UserType), services => services.GetRequiredService(type));
            builder.Services.AddScoped(type);
            return builder;
        }
         
        /// <summary>
        /// 注入用户对象仓储对象
        /// </summary>
        /// <typeparam name="TUserStore"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static NetCoreFrameIdentityBuilder AddFrameUserStore<TUserStore>(this NetCoreFrameIdentityBuilder builder)
            where TUserStore : class
        {
            var type = typeof(TUserStore);
            var sysUserInfoStoreType = typeof(SysUserInfoStore<>).MakeGenericType(builder.UserType);
            var userStoreType = typeof(IUserStore<>).MakeGenericType(builder.UserType);
            builder.Services.AddScoped(type);
            builder.Services.AddScoped(sysUserInfoStoreType, services => services.GetRequiredService(type));
            builder.Services.AddScoped(userStoreType, services => services.GetRequiredService(type));
            return builder;
        }

        /// <summary>
        /// 注入服务授权验证
        /// </summary>
        /// <typeparam name="TPermissionChecker"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static NetCoreFrameIdentityBuilder AddPermissionChecker<TPermissionChecker>(this NetCoreFrameIdentityBuilder builder)
        where TPermissionChecker : class
        {
            var type = typeof(TPermissionChecker);
            //var checkerType = typeof(PermissionCheckerCore<,>).MakeGenericType(builder.RoleType, builder.UserType);
            var checkerType = typeof(PermissionCheckerCore<>).MakeGenericType(builder.UserType);
            builder.Services.AddScoped(type);
            builder.Services.AddScoped(checkerType, provider => provider.GetService(type));
            builder.Services.AddScoped(typeof(IPermissionChecker), provider => provider.GetService(type));
            return builder;
        }

    }
}
