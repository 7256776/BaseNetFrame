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
    /// 轮询给同名称的队列发送消息
    /// </summary>
    public class DirectExchange
    {
        public static ConnectionFactory factory ;
        public static IConnection connection;
        public static IModel channel;

        string exchangeName = "DirectExchange";
        string queueName = "helloB";
        string routeKey = "helloRouteKey";

        public DirectExchange()
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
                channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, false, false, null);
                //定义一个队列
                channel.QueueDeclare(queueName, false, false, false, null);
                //将队列绑定到交换机
                channel.QueueBind(queueName, exchangeName, routeKey, null);

            }
        }

        public void SendMQ(string mesg)
        {
            var sendBytes = Encoding.UTF8.GetBytes(mesg);
            channel.BasicPublish(exchangeName, routeKey, null, sendBytes);
        }

       

    }
}
