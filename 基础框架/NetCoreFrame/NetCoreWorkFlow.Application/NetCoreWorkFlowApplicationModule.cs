﻿using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using NetCoreWorkFlow.Core;

namespace NetCoreWorkFlow.Application
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(NetCoreWorkFlowCoreModule)
        )]
    public class NetCoreWorkFlowApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //IocManager.Register<ClassMain, ClassSubA>(DependencyLifeStyle.Transient);
            //IocManager.Register<ClassMain, ClassSubB>(DependencyLifeStyle.Transient);

        }

        public override void Initialize()
        {
            var thisAssembly = typeof(NetCoreWorkFlowApplicationModule).GetAssembly();
            IocManager.RegisterAssemblyByConvention(thisAssembly);
        }

        public override void PostInitialize()
        {

           
        }



    }
}
