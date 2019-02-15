using RabbitMQ.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreFrame.Sample
{

    public class Producer
    {
        //模拟存储对象
        public readonly static ConcurrentDictionary<string, string> dbDict = new ConcurrentDictionary<string, string>();
        public readonly static List<MessageData> dbList= new List<MessageData>();


        public  static ConnectionFactory factory ;
        public  static IConnection connection;
        public  static IModel channel;
        public Producer()
        {
            if (connection==null  || !connection.IsOpen)
            {
                factory = new ConnectionFactory();
                //创建连接工厂
                factory.UserName = "test";//用户名
                factory.Password = "test";//密码
                factory.HostName = "127.0.0.1";//rabbitmq ip
                //创建连接
                connection = factory.CreateConnection();
                //创建通道
                channel = connection.CreateModel();
                //声明一个队列
                //channel.QueueDeclare("hello", false, false, false, null);
            }
        }


        public void SendMQ(string mesg)
        {
            var sendBytes = Encoding.UTF8.GetBytes(mesg);
            channel.BasicPublish("", "hello", null, sendBytes);
        }

    }


    public class MessageData
    {
        public MessageData() { }

        public MessageData(string msgName, string msgValue)
        {
            MsgName = msgName;
            MsgValue = msgValue;
            MsgDate = DateTime.Now;
        }

        public string MsgName { get; set; }
        public string MsgValue { get; set; }
        public DateTime MsgDate { get; set; }
    }

}
