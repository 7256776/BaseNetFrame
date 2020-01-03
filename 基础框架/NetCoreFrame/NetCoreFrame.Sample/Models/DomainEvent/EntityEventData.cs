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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 定义视图对象的驱动事件
/// </summary>
namespace NetCoreFrame.Sample
{

    public class EntityEventProcessData
    {
        public DateTime DataDate { get; set; }
        public string DataContent { get; set; }
    }


    public class EntityEventData : 
        IEventHandler<EntityCreatingEventData<SysSetting>>, 
        IEventHandler<EntityCreatedEventData<SysSetting>>,
        IEventHandler<EntityUpdatingEventData<SysSetting>>,
        IEventHandler<EntityUpdatedEventData<SysSetting>>,
        IEventHandler<EntityDeletingEventData<SysSetting>>,
        IEventHandler<EntityDeletedEventData<SysSetting>>,
        IEventHandler<EntityChangingEventData<SysSetting>>,
        IEventHandler<EntityChangedEventData<SysSetting>>,
        ITransientDependency
    {
        public static readonly List<EntityEventProcessData> EntityEventProcessDataList =new List<EntityEventProcessData>();

        /// <summary>
        /// 更新前
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityUpdatingEventData<SysSetting> eventData)
        {

            //新增持久化数据,此处改变数据会写入数据
            //eventData.Entity.Name += "_更新前";
            EntityEventProcessDataList.Add(new EntityEventProcessData { DataDate = DateTime.Now, DataContent = "更新前" });
        }

        /// <summary>
        /// 更新后
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityUpdatedEventData<SysSetting> eventData)
        {
            //新增持久化数据,此处改变数据会写入数据
            //eventData.Entity.Name += "_更新后";
            EntityEventProcessDataList.Add(new EntityEventProcessData { DataDate = DateTime.Now, DataContent = "更新后" });
        }

        /// <summary>
        /// 创建前
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityCreatingEventData<SysSetting> eventData)
        {
            //新增持久化数据,此处改变数据会写入数据
            //eventData.Entity.Name += "_创建前";
            EntityEventProcessDataList.Add(new EntityEventProcessData { DataDate = DateTime.Now, DataContent = "创建前" });
        }

        /// <summary>
        /// 创建后
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityCreatedEventData<SysSetting> eventData)
        {
            //新增持久化数据后,此处改变数据不影响写入数据
            //eventData.Entity.Name += "_创建后";
            EntityEventProcessDataList.Add(new EntityEventProcessData { DataDate = DateTime.Now, DataContent = "创建后" });
        }

        /// <summary>
        /// 删除前
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityDeletingEventData<SysSetting> eventData)
        {
            //删除数据,此处改变数据 持久化数据
            EntityEventProcessDataList.Add(new EntityEventProcessData { DataDate = DateTime.Now, DataContent = "删除前" });
        }

        /// <summary>
        /// 删除后
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityDeletedEventData<SysSetting> eventData)
        {
            //删除数据,此处改变数据 不影响持久化数据
            EntityEventProcessDataList.Add(new EntityEventProcessData { DataDate = DateTime.Now, DataContent = "删除后" });
        }

        /// <summary>
        /// 变更前
        /// 当事件驱动过程中的对象数据进行修改后就会触发
        /// 1. 在其他事件驱动过程中不要
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityChangingEventData<SysSetting> eventData)
        {
            // 新增 更新 删除 前都会触发
            //eventData.Entity.Name += "_变更前";
            EntityEventProcessDataList.Add(new EntityEventProcessData { DataDate = DateTime.Now, DataContent = "变更前" });
        }

        /// <summary>
        /// 变更后
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityChangedEventData<SysSetting> eventData)
        {
            // 新增 更新 删除 后都会触发
            //eventData.Entity.Name += "_变更后";
            EntityEventProcessDataList.Add(new EntityEventProcessData { DataDate = DateTime.Now, DataContent = "变更后" });
        }




    }
}
