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
        public ConnectionFactory factory;
        public IConnection connection;
        public IModel channel;
        //发送的交换机服务端必须同名称
        string exchangeName = "DirectExchange";

        /// <summary>
        /// 发送规则
        /// 发送到指定队列,如果存在多个同名称队列,每次发送的时候将轮询发放到不同队列
        /// </summary>
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
                try
                {
                    //创建连接
                    connection = factory.CreateConnection();
                    //创建通道
                    channel = connection.CreateModel();
                    //定义一个Direct类型交换机
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, false, false, null);
                }
                catch (Exception ex)
                {
                    throw new Abp.UI.UserFriendlyException("RabbitMQ", "请检查是否开启或安装RabbitMQ服务!", ex);
                }
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="dto"></param>
        public void SendMQ(SendMessageDto dto)
        {
            string mesg = "来自队列(" + dto.QueueName + ")" + dto.Message;

            //创建连接会话对象
            using (IModel channel = connection.CreateModel())
            {
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                //队列持久化
                //IBasicProperties properties = channel.CreateBasicProperties();
                //properties.Persistent = true;

                //消息内容
                byte[] body = Encoding.UTF8.GetBytes(mesg);
                //发送消息  routingKey设置接受队列的QueueName
                channel.BasicPublish(exchange: exchangeName, routingKey: dto.QueueName, basicProperties: null, body: body);
            }
        }



    }
}
