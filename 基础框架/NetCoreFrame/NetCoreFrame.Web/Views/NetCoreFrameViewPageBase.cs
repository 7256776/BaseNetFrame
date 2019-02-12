using Abp.AspNetCore.Mvc.ViewComponents;
using Abp.Threading;
using NetCoreFrame.Core;
using System;

namespace NetCoreFrame.Web.Views
{
    public abstract class NetCoreFrameViewPageBase : AbpViewComponent
    {
        protected NetCoreFrameViewPageBase()
        {
            LocalizationSourceName = ConstantConfig.LocalizationName;
        }

      

    }
}