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
using System.Threading.Tasks;

/// <summary>
/// 自定义驱动事件
/// </summary>
namespace NetCoreFrame.Sample
{
    /// <summary>
    /// 定义事件
    /// </summary>
    public class CustomEventData : EventData
    {
        public CacheParam CacheParamModel { get; set; }
    }

    /// <summary>
    /// 事件激活实现
    /// </summary>
    public class ActivityWriter : IEventHandler<CustomEventData>, ITransientDependency
    {
        public void HandleEvent(CustomEventData eventData)
        {
            eventData.CacheParamModel.Message = "通过驱动事件后的数据";
        }

    }

}
