using Abp.Dependency;
using Abp.Events.Bus.Exceptions;
using Abp.Events.Bus.Handlers;

namespace NetCoreFrame.Web
{
    public class FrameExceptionHandler : IEventHandler<AbpHandledExceptionData>, ITransientDependency
    {
        /// <summary>
        /// 异常事件
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(AbpHandledExceptionData eventData)
        {
            //处理检查数据异常事件!
        }
    }
}

