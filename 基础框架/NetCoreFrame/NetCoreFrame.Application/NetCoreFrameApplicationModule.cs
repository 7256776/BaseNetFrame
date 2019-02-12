using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using NetCoreFrame.Core;

namespace NetCoreFrame.Application
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(NetCoreFrameCoreModule)
        )]
    public class NetCoreFrameApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(NetCoreFrameApplicationModule).GetAssembly();
            IocManager.RegisterAssemblyByConvention(thisAssembly);

            //Configuration.Modules.AbpAutoMapper().Configurators.Add(
            //    // 扫描程序集，查找从AutoMapper.Profile继承的类
            //    cfg => cfg.AddProfiles(thisAssembly)
            //);
        }

        public override void PostInitialize()
        {
            #region 放在请求过程中注入,便于重写的对象优先注入(实现接口重写的过程中新增的类名称的命名方式必须接口的命名,名称)
            //手动注入登录验证、注销、密码修改 扩展类的默认实现(提供重写登录接口的类优先注册)
            IocManager.RegisterIfNot<IAccounExtens, AccounExtens>(DependencyLifeStyle.Transient);
            //手动注入用户基本信息扩展的默认实现(提供重写登录接口的类优先注册)
            IocManager.RegisterIfNot<IUserInfoExtens, UserInfoExtens>(DependencyLifeStyle.Transient);
            #endregion
        }



    }
}
