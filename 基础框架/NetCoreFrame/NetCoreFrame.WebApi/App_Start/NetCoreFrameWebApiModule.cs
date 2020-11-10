using System;
using System.Collections.Generic;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using NetCoreFrame.Application;

namespace NetCoreFrame.WebApi
{
    [DependsOn(
        typeof(AbpAspNetCoreModule),
        typeof(NetCoreFrameApplicationModule)
        )]
    public class NetCoreFrameWebApiModule : AbpModule
    {

        public override void PreInitialize()
        {
            /*
             * 注册所有服务层请求动态生成webapi服务(所有请求均为POST)
             * 注1: 在应用层使用 RemoteService 特性给类或者接口 enable/disable 设置是否产生Api服务
             * 注2: 在Controller使用AllowAnonymous 或 在应用层使用AbpAllowAnonymous 特性来进行匿名访问，避免身份以及授权认证检测
             * 注3: 可以通过Where与ConfigureControllerModel 代理函数处理生成服务前的筛选与设置
             */
            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(NetCoreFrameApplicationModule).GetAssembly(),
                     moduleName: "frame",
                     useConventionalHttpVerbs: false
                 )
                 .Where(IsCreateWebApi)                                       //提供生成的WebApi筛选
                 .ConfigureControllerModel(controllerModel);        //提供生成WebApi的配置调整

            //使用mvc的日期格式化方案
            Configuration.Modules.AbpAspNetCore().UseMvcDateTimeFormatForAppServices = true;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(NetCoreFrameWebApiModule).GetAssembly());
        }

        #region 动态生成WebApi服务代理函数
        private bool IsCreateWebApi(Type t)
        {
            /*
             * 此处添加判断业务用于筛选可以生成WebApi的后台服务
             */
            return true;
        }

        private void controllerModel(ControllerModel configurer)
        {
            /*
             * 此处对 configurer 对象进行调整可以反应到发布出的WebApi
             * 示例: configurer.ControllerName = "控制器添加名称-" + configurer.ControllerName;
             */
        }
        #endregion

    }
}
