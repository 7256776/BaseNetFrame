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

        private readonly Producer producer;
        private readonly ConsumerA consumerA;
        private readonly ConsumerB consumerB;
        private readonly ConsumerC consumerC;
        private readonly ConsumerEx consumerEx;
        private readonly DirectExchange directExchange;
        private readonly FanoutExchange fanoutExchange;
        private readonly TopicExchange topicExchange;

        public RabbitMQController()
        {
            producer = new Producer();
            directExchange = new DirectExchange();
            //fanoutExchange = new FanoutExchange();
            //topicExchange = new TopicExchange();
            //consumerA = new ConsumerA();
            //consumerB = new ConsumerB();
            //consumerC = new ConsumerC();
            //consumerEx = new ConsumerEx();

        }

        public IActionResult Index()
        {
            return View();
        }


        public Task<JsonResult> SendMessage([FromBody]string msg)
        {
            producer.SendMQ(msg);
            return Task.FromResult(Json(true));
        }


        public Task<JsonResult> SendDirectMessage([FromBody]SendMessageDto dto)
        {
            directExchange.SendMQ(dto);
            return Task.FromResult(Json(true));
        }












        public Task<JsonResult> SendFanoutMessage([FromBody]string msg)
        {
            fanoutExchange.SendMQ(msg);
            return Task.FromResult(Json(true));
        }

        public Task<JsonResult> SendTopicMessage1([FromBody]string msg)
        {
            topicExchange.SendMQ1(msg);
            return Task.FromResult(Json(true));
        }
        public Task<JsonResult> SendTopicMessage2([FromBody]string msg)
        {
            topicExchange.SendMQ2(msg);
            return Task.FromResult(Json(true));
        }




        public Task<JsonResult> GetMessage()
        {
            Producer.dbList.Sort((a, b) =>
            {
                if (b.MsgDate > a.MsgDate)
                    return 1;
                else
                {
                    return -1;
                }
            });
            return Task.FromResult(Json(Producer.dbList));
        }

        public Task<JsonResult> ClearMessage()
        {
            Producer.dbList.Clear();
            return Task.FromResult(Json(Producer.dbList));
        }




    }
}
