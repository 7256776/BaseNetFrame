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
    /// 消息发送到所有注册的队列
    /// </summary>
    public class FanoutExchange
    {
        public static ConnectionFactory factory ;
        public static IConnection connection;
        public static IModel channel;

        string exchangeName = "FanoutExchange";
        string queueName1 = "hello";
        string queueName2 = "helloB";
        string routeKey = "";

        public FanoutExchange()
        {
            if (connection == null || !connection.IsOpen)
            {
                //创建连接工厂
                factory = new ConnectionFactory
                {
                    UserName = "test",//用户名
                    Password = "test",//密码
                    HostName = "127.0.0.1"//rabbitmq ip
                };

                //创建连接
                connection = factory.CreateConnection();
                //创建通道
                channel = connection.CreateModel();
                //定义一个Direct类型交换机
                channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, false, false, null);

                //定义队列1
                channel.QueueDeclare(queueName1, false, false, false, null);
                //定义队列2
                channel.QueueDeclare(queueName2, false, false, false, null);

                //将队列绑定到交换机
                channel.QueueBind(queueName1, exchangeName, routeKey, null);
                channel.QueueBind(queueName2, exchangeName, routeKey, null);
               
            }
        }

        public void SendMQ(string mesg)
        {
            var sendBytes = Encoding.UTF8.GetBytes(mesg);
            channel.BasicPublish(exchangeName, routeKey, null, sendBytes);
        }
         

    }
}
