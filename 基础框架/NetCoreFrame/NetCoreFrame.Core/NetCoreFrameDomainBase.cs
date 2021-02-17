using Abp.Application.Services;
using Abp.Domain.Services;
using NetCoreFrame.Core;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 领域核心 基础类
    /// </summary>
    public abstract class NetCoreFrameDomainBase : DomainService
    {

        protected NetCoreFrameDomainBase()
        {
            base.LocalizationSourceName= ConstantConfig.LocalizationName;
        }


    }
}
