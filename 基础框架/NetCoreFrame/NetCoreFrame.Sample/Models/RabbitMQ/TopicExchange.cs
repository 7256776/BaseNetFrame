using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreFrame.Sample
{
    /// <summary>
    /// 消息根据routeKey规则发送到消息队列
    /// </summary>
    public class TopicExchange
    {
        public static ConnectionFactory factory ;
        public static IConnection connection;
        public static IModel channel;
        public string RabbitmqIp = "127.0.0.1";
        string exchangeName = "TopicChange";

        string exchangeName1 = "TestTopicChange1";
        string exchangeName2 = "TestTopicChange2";
        string queueName1 = "hello";
        string queueName2 = "helloC";
        string routeKey1 = "TestRouteKey.*";
        string routeKey2 = "TestRouteKey.#";

        public TopicExchange()
        {
            if (connection == null || !connection.IsOpen)
            {
                //创建连接工厂
                factory = new ConnectionFactory
                {
                    UserName = "test",//用户名
                    Password = "test",//密码
                    HostName = RabbitmqIp
                };

                //创建连接
                connection = factory.CreateConnection();
                //创建通道
                channel = connection.CreateModel();
                //定义一个Direct类型交换机
                channel.ExchangeDeclare(exchangeName, ExchangeType.Topic, false, false, null);
                //channel.ExchangeDeclare(exchangeName2, ExchangeType.Topic, false, false, null);
                ////定义队列1
                //channel.QueueDeclare(queueName1, false, false, false, null);
                ////定义队列2
                //channel.QueueDeclare(queueName2, false, false, false, null);
                ////将队列绑定到交换机
                //channel.QueueBind(queueName1, exchangeName1, routeKey1, null);
                ////将队列绑定到交换机
                //channel.QueueBind(queueName2, exchangeName1, routeKey2, null);
            }
        }

        /// <summary>
        /// 发送到C队列
        /// </summary>
        /// <param name="mesg"></param>
        public void SendMQ(SendMessageDto dto)
        {
            string mesg = "来自队列(" + dto.QueueName + ")" + dto.Message;
            var sendBytes = Encoding.UTF8.GetBytes(mesg);
            //发布消息
            channel.BasicPublish(exchangeName, dto.QueueName, null, sendBytes);
        }


        //public void SendMQ2(string mesg)
        //{
        //    var sendBytes = Encoding.UTF8.GetBytes(mesg);
        //    //发布消息
        //    channel.BasicPublish(exchangeName1, "TestRouteKey.A.EX", null, sendBytes);
        //}

    }
}
