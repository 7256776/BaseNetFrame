using System;

namespace RabbitMQService
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("启动!");

            Consumer consumerA = new Consumer("QueueA");
            consumerA.CreateChannelModel();

            Consumer consumerB = new Consumer("QueueB");
            consumerB.CreateChannelModel();
        

            Console.ReadLine();
        }






    }
}
