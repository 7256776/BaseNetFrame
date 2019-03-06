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
            //    // 扫描程序集，查找从AutoMapper.Profile继承的类 获取自定义设置的映射关系
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

    #region 自定义映射关系 示例
    /*
     
    public class CustomProfile: AutoMapper.Profile
    {
        public CustomProfile()
        {
            CreateMap<ModelA, ModelB>()
            .ForMember(d => d.NameCodeText, opt => { opt.MapFrom(s => s.NameText); })
            .ForMember(d => d.NameText, opt => { opt.MapFrom(s => s.NameCodeText); });
        }
    }

    public class ModelA
    {
        public string NameText { get; set; }
        public string NameCodeText { get; set; }
    }

    //[AutoMap(typeof(ModelA))]
    public class ModelB
    {
        public string NameText { get; set; }
        public string NameCodeText { get; set; }

    }
    */
    #endregion




}
