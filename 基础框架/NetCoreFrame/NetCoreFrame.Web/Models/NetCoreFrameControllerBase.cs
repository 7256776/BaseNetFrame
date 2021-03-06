﻿using Abp.AspNetCore.Mvc.Controllers;
using NetCoreFrame.Core;

namespace NetCoreFrame.Web
{ 
    /// <summary>
    /// 控制器基类,所有控制需继承该类
    /// </summary>
    public class NetCoreFrameControllerBase : AbpController 
    {
        protected NetCoreFrameControllerBase()
        {
            LocalizationSourceName = ConstantConfig.LocalizationName;
        }
        /// <summary>
        /// 替换Abp的AbpSession
        /// </summary>
        public new IAbpSessionExtens AbpSession { get; set; }

    }
}