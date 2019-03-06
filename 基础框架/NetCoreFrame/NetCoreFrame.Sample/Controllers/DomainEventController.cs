using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using NetCoreFrame.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Threading.Tasks;

namespace NetCoreFrame.Sample.Controllers
{
    public class DomainEventController : NetCoreFrameControllerBase
    {
        private readonly IRepository<SysSetting, Guid> _auditLogRepository;

        public IEventBus _eventBus { get; set; }

        public DomainEventController(IRepository<SysSetting, Guid> auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;

            _eventBus = NullEventBus.Instance;
        }


        public IActionResult Index()
        {
            return View();
        }

        #region 领域驱动事件的处理
        public  Task<JsonResult> DoEventAsync([FromBody]CacheParam param)
        {
            EventDataDto dto = new EventDataDto { EventDataName = "参数" };
            //触发事件
            _eventBus.TriggerAsync(this, dto);

            //return Json(param);
            return Task.FromResult(Json(dto.EventDataName)) ;
        }

        /// <summary>
        /// 基本驱动事件 示例
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public JsonResult DoEvent([FromBody]CacheParam param)
        {
            //触发事件
            _eventBus.Trigger(this, new CustomEventData { CacheParamModel = param });

            #region 还可以通过注册来实现
            CustomEventHandler customEventHandler = new CustomEventHandler();
            //注册
            _eventBus.Register<CustomEventData>(customEventHandler.SetHandleEvent);
            //触发事件
            _eventBus.Trigger(this, new CustomEventData { CacheParamModel = param });
            //注销
            _eventBus.Unregister<CustomEventData>(customEventHandler.SetHandleEvent);
            #endregion
            return Json(param);
        }


        public JsonResult InsertLog([FromBody]CacheParam param)
        {
            SysSetting sysSetting = new SysSetting() { Name = param.Message };
            sysSetting.Name += "_修改";

            SysSettingDto modelInput = sysSetting.MapTo<SysSettingDto>();

            var data = _auditLogRepository.Insert(sysSetting);
            return Json(data.Id);
        }

        public JsonResult UpdateLog([FromBody]CacheParam param)
        {
            _auditLogRepository.Update(new SysSetting()
            {
                Id = Guid.Parse(param.Id),
                Name = param.Message,
                //CreationTime = new DateTime()
            });
            return Json(param);
        }

        public JsonResult DeleteLog([FromBody]Guid id)
        {
            _auditLogRepository.Delete(id);
            return Json(true);
        }
        #endregion

        #region 自定义数据返回筛选器
        [FrameResultHandler]
        public JsonResult GetCustomResultData()
        {
            //触发事件
            var data = new
            {
                name = "名称",
                value = "值类型"
            };
            return Json(data);
        }

        public JsonResult GetResultData()
        {
            var data = new
            {
                name = "名称",
                value = "值类型"
            };
            return Json(data);
        }
        #endregion

    }
}
