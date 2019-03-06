using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dapper.Repositories;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Web.Models;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 自定义驱动事件
/// </summary>
namespace NetCoreFrame.Sample
{
    /// <summary>
    /// 定义事件所需 的参数对象
    /// </summary>
    public class CustomEventData : EventData
    {
        public CacheParam CacheParamModel { get; set; }

        public SysSettingDto SysSettingDtoModel { get; set; }
    }

    /// <summary>
    /// 定义事件所需 的参数对象
    /// </summary>
    public class EventDataDto : EventData
    {
        public string EventDataName { get; set; }
    }


    /// <summary>
    /// 事件激活实现
    /// 1. 如何该类不继承ITransientDependency可以通过手动注册的方式
    /// 2. 手动注册事件驱动
    ///         ActivityWriter activityWriter = new ActivityWriter();
    ///         //注册
    ///         IEventBus.Register<CustomEventData>(activityWriter.HandleEvent);
    ///         触发事件
    ///         IEventBus.Trigger(this, new CustomEventData { CacheParamModel = param
    ///         注销
    ///         IEventBus.Unregister<CustomEventData>(activityWriter.HandleEvent);
    /// </summary>
    public class CustomEventHandler : IEventHandler<CustomEventData>, ITransientDependency
    {
        /// <summary>
        /// 默认注入的实现
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(CustomEventData eventData)
        {
            eventData.CacheParamModel.Message += "HandleEvent驱动事件后的数据  ";
        }

        /// <summary>
        /// 添加自定义的实现,但是需要手动注册
        /// </summary>
        /// <param name="eventData"></param>
        public void SetHandleEvent(CustomEventData eventData)
        {
            eventData.CacheParamModel.Message += "SetHandleEvent驱动事件后的数据  ";
        }

    }

    /// <summary>
    /// 可以根据事件参数对象定义多个事件驱动实体
    /// </summary>
    public class DoEventHandler : IEventHandler<EventDataDto>, ITransientDependency
    {
        /// <summary>
        /// 默认注入的实现
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EventDataDto eventData)
        {
            eventData.EventDataName += "事件驱动";
            Thread.Sleep(3000);
        }

    }



}
