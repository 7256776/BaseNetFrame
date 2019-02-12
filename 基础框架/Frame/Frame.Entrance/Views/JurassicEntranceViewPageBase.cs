using Abp.Web.Mvc.Views;
using Frame.Core;
using Frame.Web.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frame.Entrance.Views
{
    public abstract class FrameEntranceViewPageBase : FrameEntranceViewPageBase<dynamic>
    {

    }

    public abstract class FrameEntranceViewPageBase<TModel> : FrameAppWebViewPageBase<TModel>
    {
        protected FrameEntranceViewPageBase()
        {
            LocalizationSourceName = ConstantConfig.LocalizationName;
        }


    }
}