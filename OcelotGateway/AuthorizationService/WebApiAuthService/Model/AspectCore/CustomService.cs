using AspectCore.DynamicProxy;
using Castle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAuthService
{
    public interface ICustomService
    {
        [CustomInterceptor]
        void Call();

        void Say();

    }

    public class CustomService : ICustomService
    {
        public void Call()
        {
            Console.WriteLine("service calling...");
        }

        public void Say()
        {
            Console.WriteLine("service Say...");
        }

    }








}
