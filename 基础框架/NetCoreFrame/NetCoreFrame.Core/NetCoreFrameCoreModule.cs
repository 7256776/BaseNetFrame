using Abp.Dapper;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Json;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using System.Transactions;

namespace NetCoreFrame.Core
{
    [DependsOn(
        typeof(AbpEntityFrameworkCoreModule),
        typeof(AbpDapperModule)
        )]
    public class NetCoreFrameCoreModule : AbpModule
    {

        private readonly IConfigurationRoot _appConfiguration;

        public NetCoreFrameCoreModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.GetConfigurationCache();
        }

        public override void PreInitialize()
        {
            //多租户
            Configuration.MultiTenancy.IsEnabled = false;

            //设置数据连接字符串的名称对应 connectionStrings配置的Name值
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(ConstantConfig.DBDefaultName);

            //设置工作单元
            Configuration.UnitOfWork.IsolationLevel = IsolationLevel.ReadCommitted;
            Configuration.UnitOfWork.Timeout = TimeSpan.FromMinutes(1);
            //这货虽然是关闭本地化,但是关闭后似乎系统提示全部变英文了
            //Configuration.Localization.IsEnabled = true;
            //设置应用程序本地化语言 zh-CN  zh-Hans(简体中文)
            //Configuration.Localization.Languages.Add(new LanguageInfo("zh-CN", "简体中文", "cn.png", false));
            Configuration.Localization.Languages.Add(new LanguageInfo("zh-Hans", "简体中文", "cn.png", true));
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "us.png", false));
            //加载本地化资源文件(所加载的文件路径是 启动的web程序集根目录下的Localization目录)
            NetCoreFrameLocalizationConfigurer.LocalPath(Configuration.Localization);

            //初始加载导航菜单
            Configuration.Navigation.Providers.Add<NavigationMenusProvider>();

            //授权配置
            Configuration.Authorization.Providers.Add<AuthorizationPermissionProvider>();

            //配置缓存
            Configuration.Caching.ConfigureAll(cache =>
            {
                //设置默认过期时间 10小时
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(10);
            });

            //是否启用未登录用户的审计日志
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            //是否启用审计日志(默认启用)
            //Configuration.Auditing.IsEnabled = false;

            #region  设置指定类型记录审计日志(指定类型可以是接口或类对象 等效于 在类设置标签[Audited])
            // Configuration.Auditing.Selectors.Add(
            //    new NamedTypeSelector(
            //        "Frame.ICacheManagerExtens",
            //        type => typeof(ICacheManagerExtens).IsAssignableFrom(type)
            //    )
            //);
            #endregion

            //系统配置信息
            Configuration.Settings.Providers.Add<FrameSettingProvider>();

            //配置通知订阅
            Configuration.Notifications.Providers.Add<FrameNotificationProvider>();

            //注册EfDBContext,或者在Startup启动类注册
            Configuration.Modules.AbpEfCore().AddDbContext<NetCoreFrameDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    NetCoreFrameDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    NetCoreFrameDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                }
            });
        }

        public override void Initialize()
        { 
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            //获取启动时间
            //IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }



    }
}
