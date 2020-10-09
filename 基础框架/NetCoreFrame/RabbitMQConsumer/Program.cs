using System;

namespace RabbitMQService
{
    public class Program
    {

        public static void Main(string[] args)
        {
            #region 基础示例,通过线程等待模式能者多劳的接受方式
            //Console.WriteLine("启动!");
            //Console.Write("输入y启动带线程等待的消息队列!");
            //Consumer consumerA = new Consumer("QueueA");
            //string str = Console.ReadLine();//从控制台读入输入
            //if (str == "y")
            //    consumerA.isSleep = true;
            //else
            //    consumerA.isSleep = false;
            #endregion

            #region 订阅模式Fanout 发送到所有
            //Console.WriteLine("订阅模式(Fanout) 启动!");
            //Console.Write("输入消息队列名称!");
            //string str = Console.ReadLine();
            //ConsumerFanoutExchange consumerA = new ConsumerFanoutExchange(str);
            #endregion

            #region 直接交换模式(Direct)
            Console.WriteLine("直接交换模式(Direct) 启动!");
            Console.Write("输入路由名称多个用 ',' 分割!");
            string str = Console.ReadLine();
            string[] arr = str.Split(',');
            ConsumerDirectExchange consumerDirectExchange = new ConsumerDirectExchange();
            foreach (string item in arr)
            {
                consumerDirectExchange.QueueBind(item);
            }
            #endregion

            #region 路由模式(Topic)
            //Console.WriteLine("路由模式(Topic) 启动!");
            //Console.Write("输入路由名称多个用 ',' 分割!");
            //string str = Console.ReadLine();
            //ConsumerTopicExchange consumerTopicExchange = new ConsumerTopicExchange();
            //consumerTopicExchange.QueueBind(str);
            #endregion

            #region 直接交换模式(Direct)
            //Console.WriteLine("直接交换模式(Direct) 启动!");
            //Console.Write("输入路由名称多个用 ',' 分割!");
            //string str = Console.ReadLine();
            //string[] arr = str.Split(',');
            //SAPIDirectDemo consumerDirectExchange = new SAPIDirectDemo();
            //foreach (string item in arr)
            //{
            //    consumerDirectExchange.QueueBind(item);
            //}
            #endregion
            Console.ReadLine();
        }






    }
}
