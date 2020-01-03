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
        public string RabbitmqIp = "127.0.0.1";

        public string exchangeName = "FanoutExchange";
        public string routeKey = "";

        /// <summary>
        /// 订阅模式发送到所以该交换机队列
        /// </summary>
        public FanoutExchange()
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
                try
                {
                    //创建连接
                    connection = factory.CreateConnection();
                    //创建通道
                    channel = connection.CreateModel();
                    //定义一个 Fanout 类型交换机
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, false, false, null);
                }
                catch (Exception ex)
                {
                    throw new Abp.UI.UserFriendlyException("RabbitMQ", "请检查是否开启或安装RabbitMQ服务!", ex);
                }
            }
        }

        public void SendMQ(string mesg)
        {
            var sendBytes = Encoding.UTF8.GetBytes(mesg);
            channel.BasicPublish(exchangeName, routeKey, null, sendBytes);
        }
         

    }
}
