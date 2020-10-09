using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQService
{
    /*
     * 创建消息(消费端)处理消息的的服务
     * 每个队列有单独的名称,也可以同名称
     * 消息发送方会集合消息队列名称进行推送消息
     */
    public class ConsumerFanoutExchange
    {
        public string RabbitmqIp = "127.0.0.1";
        public ConnectionFactory factory;
        public IConnection connection;
        public IModel channel;
        public string exchangeName = "FanoutExchange";
        public string queueName;

        public ConsumerFanoutExchange() { }

        public ConsumerFanoutExchange(string queueName)
        {
            this.queueName = queueName;

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

                //定义一个 Fanout 类型交换机
                channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, false, false, null);
                //定义队列
                channel.QueueDeclare(queueName, false, false, false, null);
                //将队列绑定到交换机
                channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "");
                //告诉Rabbit每次只能向消费者发送一条信息,再消费者未确认之前,不再向他发送信息
                channel.BasicQos(0, 1, false);
                //事件基本消费者
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                //执行发送消息
                consumer.Received += ReceivedMQ;
                //启动消费者 设置为手动应答消息
                channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="ea"></param>
        public void ReceivedMQ(object ch, BasicDeliverEventArgs ea)
        {
            var message = Encoding.UTF8.GetString(ea.Body);
            //确认一条或多条已传递的消息
            channel.BasicAck(ea.DeliveryTag, false);
            //
            Console.WriteLine(queueName + "接收到消息:" + message);
        }

    }
}
