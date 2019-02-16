using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCoreFrame.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreFrame.Sample.Controllers
{
    public class RabbitMQController : NetCoreFrameControllerBase
    {
        //https://www.rabbitmq.com/install-windows.html
        //https://www.wyxxw.cn/blog-detail-2-21-343
        //https://www.cnblogs.com/stulzq/p/7551819.html
        //http://localhost:15672/

        //需要启动RabbitMQService
        private readonly BaseExchange baseExchange;
        private readonly DirectExchange directExchange;
        private readonly FanoutExchange fanoutExchange;
        private readonly TopicExchange topicExchange;

        public RabbitMQController()
        {
            baseExchange = new BaseExchange();
            directExchange = new DirectExchange();
            fanoutExchange = new FanoutExchange();
            topicExchange = new TopicExchange();

        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 基础演示
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Task<JsonResult> SendMessage([FromBody]SendMessageDto dto)
        {
            baseExchange.SendMQ(dto);
            return Task.FromResult(Json(true));
        }

        /// <summary>
        /// 路由模式
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<JsonResult> SendDirectMessage([FromBody]SendMessageDto dto)
        {
            directExchange.SendMQ(dto);
            return Task.FromResult(Json(true));
        }

        /// <summary>
        /// 订阅模式
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Task<JsonResult> SendFanoutMessage([FromBody]SendMessageDto dto)
        {
            fanoutExchange.SendMQ(dto.Message);
            return Task.FromResult(Json(true));
        }

        /// <summary>
        /// #：匹配0-n个字符语句 
        /// *：匹配一个字符语句
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<JsonResult> SendTopicMessage([FromBody]SendMessageDto dto)
        {
            topicExchange.SendMQ(dto);
            return Task.FromResult(Json(true));
        }







    }
}
