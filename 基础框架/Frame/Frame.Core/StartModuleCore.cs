using Abp.EntityFramework;
using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Json;
using Abp.Modules;
using System;
using System.Reflection;
using System.Transactions;
using System.Web;

namespace Frame.Core
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(AbpEntityFrameworkModule))]
    public class StartModuleCore : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void PreInitialize()
        {

        

            Configuration.MultiTenancy.IsEnabled = false;

            //设置数据连接字符串的名称对应 connectionStrings配置的Name值
            Configuration.DefaultNameOrConnectionString = "Default";

            //设置工作单元
            Configuration.UnitOfWork.IsolationLevel = IsolationLevel.ReadCommitted;
            Configuration.UnitOfWork.Timeout = TimeSpan.FromMinutes(1);

            //设置应用程序语言
            Configuration.Localization.Languages.Add(new LanguageInfo("zh-CN", "简体中文", "cn.png", true));
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "us.png", false));
            //使用本地文件
            Configuration.Localization.Sources.Add(new DictionaryBasedLocalizationSource(
                ConstantConfig.LocalizationName,
                new JsonFileLocalizationDictionaryProvider(
                    HttpContext.Current.Server.MapPath("~/Localization/SourceJson")
                     )
                )
             );
 

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

            //是否启用审计日志
            //Configuration.Auditing.IsEnabled = false;

            //设置指定类型记录审计日志(指定类型可以是接口或类对象 等效于 在类设置标签[Audited])
            // Configuration.Auditing.Selectors.Add(
            //    new NamedTypeSelector(
            //        "Frame.ICacheManagerExtens",
            //        type => typeof(ICacheManagerExtens).IsAssignableFrom(type)
            //    )
            //);

            //用户配置信息
            Configuration.Settings.Providers.Add<FrameSettingProvider>();

            //配置通知订阅
            //Configuration.Notifications.Providers.Add<FrameNotificationProvider>();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

    }
}