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

namespace NetCoreFrame.Sample
{
 

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

        /// <summary>
        /// 更新前
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityUpdatingEventData<SysSetting> eventData)
        {
            //新增持久化数据,此处改变数据会写入数据
            //eventData.Entity.Name += "_Updating";
        }

        /// <summary>
        /// 更新后
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityUpdatedEventData<SysSetting> eventData)
        {
            //新增持久化数据,此处改变数据会写入数据
            //eventData.Entity.Name += "_Updated";
        }

        /// <summary>
        /// 创建前
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityCreatingEventData<SysSetting> eventData)
        {
            //新增持久化数据,此处改变数据会写入数据
            //eventData.Entity.Name += "_Creating";
        }

        /// <summary>
        /// 创建后
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityCreatedEventData<SysSetting> eventData)
        {
            //新增持久化数据后,此处改变数据不影响写入数据
            //eventData.Entity.Name += "_Created";
        }

        /// <summary>
        /// 删除前
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityDeletingEventData<SysSetting> eventData)
        {
            //删除数据,此处改变数据 持久化数据
        }

        /// <summary>
        /// 删除后
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityDeletedEventData<SysSetting> eventData)
        {
            //删除数据,此处改变数据 不影响持久化数据
        }

        /// <summary>
        /// 变更前
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityChangingEventData<SysSetting> eventData)
        {
            // 新增 更新 删除 前都会触发
            //eventData.Entity.Name += "_Changing";
        }

        /// <summary>
        /// 变更后
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityChangedEventData<SysSetting> eventData)
        {
            // 新增 更新 删除 后都会触发
            //eventData.Entity.Name += "_Changed";
        }
    }


}
