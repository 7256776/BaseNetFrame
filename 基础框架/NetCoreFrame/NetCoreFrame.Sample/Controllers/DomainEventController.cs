using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Core;
using NetCoreFrame.Web;
using System;
using System.Linq;
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
        /// <summary>
        /// 基本驱动事件 示例
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<JsonResult> DoEventAsync([FromBody]CacheParam param)
        {
            //通过事件驱动的实体对象确定所调用的事件驱动
            EventDataDto dto = new EventDataDto { EventDataName = "1:创建对象" + "  2:输入值=" + param.KeyName };
            //触发事件
            _eventBus.TriggerAsync(this, dto);
            return Task.FromResult(Json(dto.EventDataName));
        }

        /// <summary>
        /// 基本驱动事件 示例
        /// 注册,触发,注销
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
        #endregion

        #region 实体对象监控示例
        /// <summary>
        /// 新增对象
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public JsonResult InsertLog([FromBody]CacheParam param)
        {
            SysSetting sysSetting = new SysSetting() { Name = param.Message };
            SysSettingDto modelInput = ObjectMapper.Map<SysSettingDto>(sysSetting);
            var data = _auditLogRepository.Insert(sysSetting);
            return Json(data.Id);
        }

        /// <summary>
        /// 修改对象
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public JsonResult UpdateLog([FromBody]CacheParam param)
        {
            SysSetting sysSetting = _auditLogRepository.Get(Guid.Parse(param.Id));
            sysSetting.Name = param.Message + "_修改数据1";
            sysSetting.Name = param.Message + "_修改数据2";

            _auditLogRepository.Update(sysSetting);
            return Json(param);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult DeleteLog([FromBody]Guid id)
        {
            _auditLogRepository.Delete(id);
            return Json(true);
        }

        /// <summary>
        /// 获取监控结果
        /// </summary>
        /// <returns></returns>
        public JsonResult GetEntityEventProcessData()
        {
            var data = EntityEventData.EntityEventProcessDataList.OrderBy(o => o.DataDate).ToList();
            EntityEventData.EntityEventProcessDataList.Clear();
            return Json(data);
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
