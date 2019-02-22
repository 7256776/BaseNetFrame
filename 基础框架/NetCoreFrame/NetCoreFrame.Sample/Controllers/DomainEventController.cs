using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
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

        public JsonResult DoEvent([FromBody]CacheParam param)
        {
            //触发事件
            _eventBus.Trigger(this, new CustomEventData { CacheParamModel = param });
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

    }
}
