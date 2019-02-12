using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Modules;
using Frame.Core;
using System.Reflection;

namespace Frame.Application
{
    [DependsOn(
        typeof(StartModuleCore),
        typeof(AbpAutoMapperModule))]
    public class StartModuleApplication : AbpModule
    {
        /// <summary>
        /// 初始化之前
        /// </summary>
        public override void PreInitialize()
        {
        }

        /// <summary>
        /// 初始化中
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// 初始化之后
        /// </summary>
        public override void PostInitialize()
        {
            //手动注入登录验证扩展类的默认实现(提供重写登录接口的类优先注册)
            IocManager.RegisterIfNot<ILoginExtens, LoginExtens>(DependencyLifeStyle.Transient);
            //手动注入注销扩展类的默认实现(提供重写登录接口的类优先注册)
            IocManager.RegisterIfNot<ILogoutExtens, LogoutExtens>(DependencyLifeStyle.Transient);
            //手动注入用户基本信息扩展的默认实现(提供重写登录接口的类优先注册)
            IocManager.RegisterIfNot<IUserInfoExtens, UserInfoExtens>(DependencyLifeStyle.Transient);
        }


    }
}