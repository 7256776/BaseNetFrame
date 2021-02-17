using Abp.Domain.Services;

namespace NetCoreWorkFlow.Core
{
    /// <summary>
    /// 领域核心 基础类
    /// </summary>
    public abstract class NetCoreWorkFlowDomainBase : DomainService
    {

        protected NetCoreWorkFlowDomainBase()
        {
            base.LocalizationSourceName= ConstantConfig.LocalizationName;
        }


    }
}
