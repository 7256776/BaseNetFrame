using Abp.Application.Services;
using NetCoreWorkFlow.Core;

namespace NetCoreWorkFlow.Application
{
    /// <summary>
    /// 应用程序服务 基础类
    /// </summary>
    public abstract class NetCoreWorkFlowApplicationBase : ApplicationService
    {

        protected NetCoreWorkFlowApplicationBase()
        {
            LocalizationSourceName = ConstantConfig.LocalizationName;
        }


    }
}
