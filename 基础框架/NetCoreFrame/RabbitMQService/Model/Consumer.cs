using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQService
{
    /*
     * 创建消息(消费端)处理消息的的服务
     * 每个队列有单独的名称,也可以同名称
     * 消息发送方会集合消息队列名称进行推送消息
     */
    public class Consumer
    {
        public string RabbitmqIp = "127.0.0.1";
        public  ConnectionFactory factory ;
        public  IConnection connection;
        public  IModel channel;
        public  string queueName;

        public Consumer() { }

        public Consumer(string queueName)
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

            }
        }

        /// <summary>
        /// 创建队列
        /// </summary>
        /// <param name="queue">队列名称</param>
        public void CreateChannelModel()
        {
            channel = connection.CreateModel();
            //定义队列1
            channel.QueueDeclare(queueName, false, false, false, null);
            //事件基本消费者
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            //执行发送消息
            consumer.Received += ReceivedMQ;
            //启动消费者 设置为手动应答消息
            channel.BasicConsume(queueName, false, consumer);

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

        //public void CreateChannelModel11(string queue)
        //{
        //    channel = connection.CreateModel();
        //    //定义队列1
        //    channel.QueueDeclare(queue, false, false, false, null);
        //    //事件基本消费者
        //    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
        //    //执行发送消息
        //    consumer.Received += ReceivedMQ;
        //    //启动消费者 设置为手动应答消息
        //    channel.BasicConsume(queue, false, consumer);

        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="ch"></param>
        ///// <param name="ea"></param>
        //public void ReceivedMQ11(object ch, BasicDeliverEventArgs ea)
        //{
        //    var message = Encoding.UTF8.GetString(ea.Body);
        //    //确认一条或多条已传递的消息
        //    channel.BasicAck(ea.DeliveryTag, false);
        //    //
        //    Console.WriteLine("QueueB接收到消息:" + message);
        //}

    }
    
    //public class ConsumerB
    //{
    //    public static ConnectionFactory factory;
    //    public static IConnection connection;
    //    public static IModel channel;
    //    public ConsumerB()
    //    {
    //        if (connection == null || !connection.IsOpen)
    //        {
    //            //创建连接工厂
    //            factory = new ConnectionFactory
    //            {
    //                UserName = "test",//用户名
    //                Password = "test",//密码
    //                HostName = RabbitmqIp
    //            };

    //            //创建连接
    //            connection = factory.CreateConnection();
    //            //创建通道
    //            channel = connection.CreateModel();
    //            //事件基本消费者
    //            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
    //            //执行发送消息
    //            consumer.Received += ReceivedMQ;
    //            //启动消费者 设置为手动应答消息
    //            channel.BasicConsume("helloB", false, consumer);
    //        }
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="ch"></param>
    //    /// <param name="ea"></param>
    //    public void ReceivedMQ(object ch, BasicDeliverEventArgs ea)
    //    {
    //        var message = Encoding.UTF8.GetString(ea.Body);
    //        //确认一条或多条已传递的消息
    //    channel.BasicAck(ea.DeliveryTag, false);
    //        //
    //        Producer.dbList.Add(new MessageData("队列B", message));
    //    }

    //}

    //public class ConsumerC
    //{
    //    public static ConnectionFactory factory;
    //    public static IConnection connection;
    //    public static IModel channel;
    //    public ConsumerC()
    //    {
    //        if (connection == null || !connection.IsOpen)
    //        {
    //            //创建连接工厂
    //            factory = new ConnectionFactory
    //            {
    //                UserName = "test",//用户名
    //                Password = "test",//密码
    //                HostName = RabbitmqIp
    //            };

    //            //创建连接
    //            connection = factory.CreateConnection();
    //            //创建通道
    //            channel = connection.CreateModel();
    //            //事件基本消费者
    //            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
    //            //执行发送消息
    //            consumer.Received += ReceivedMQ;
    //            //启动消费者 设置为手动应答消息
    //            channel.BasicConsume("helloC", false, consumer);
    //        }
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="ch"></param>
    //    /// <param name="ea"></param>
    //    public void ReceivedMQ(object ch, BasicDeliverEventArgs ea)
    //    {
    //        var message = Encoding.UTF8.GetString(ea.Body);
    //        //确认一条或多条已传递的消息
    //        channel.BasicAck(ea.DeliveryTag, false);
    //        //
    //        Producer.dbList.Add(new MessageData("队列C", message));
    //    }

    //}

    //public class ConsumerEx
    //{
    //    public static ConnectionFactory factory;
    //    public static IConnection connection;
    //    public static IModel channel;
    //    public ConsumerEx()
    //    {
    //        if (connection == null || !connection.IsOpen)
    //        {
    //            //创建连接工厂
    //            factory = new ConnectionFactory
    //            {
    //                UserName = "test",//用户名
    //                Password = "test",//密码
    //                HostName = RabbitmqIp
    //            };

    //            //创建连接
    //            connection = factory.CreateConnection();
    //            //创建通道
    //            channel = connection.CreateModel();
    //            //事件基本消费者
    //            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
    //            //执行发送消息
    //            consumer.Received += ReceivedMQ;
    //            //启动消费者 设置为手动应答消息
    //            channel.BasicConsume("hello", false, consumer);
    //        }
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="ch"></param>
    //    /// <param name="ea"></param>
    //    public void ReceivedMQ(object ch, BasicDeliverEventArgs ea)
    //    {
    //        var message = Encoding.UTF8.GetString(ea.Body);
    //        //确认一条或多条已传递的消息
    //        channel.BasicAck(ea.DeliveryTag, false);
    //        //
    //        Producer.dbList.Add(new MessageData("队列hello", message));
    //    }

    //}

}
