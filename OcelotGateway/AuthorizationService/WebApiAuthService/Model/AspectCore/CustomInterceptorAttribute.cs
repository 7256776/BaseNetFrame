using AspectCore.DynamicProxy;
using Castle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAuthService
{
    public class CustomInterceptorAttribute : AbstractInterceptorAttribute
    {

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                //获取 参数
                var param = context.Parameters;
                // 执行被拦截的方法
                await next(context); 
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                
            }
        } 

    }

    public class AppFilterAttribute : AbstractInterceptorAttribute
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                Console.WriteLine("Before service call");

                await next(context); // 执行被拦截的方法
            }
            catch (Exception)
            {
                Console.WriteLine("Service threw an exception");
                throw;
            }
            finally
            {
                Console.WriteLine("After service call");
            }
        }

    }

    //public class CustomInterceptorAllAttribute : InterceptorAttribute
    //{

    //    private readonly string _name;

    //    public CustomInterceptorAllAttribute(string name) : base(name)
    //    {

    //        _name = name;
    //    }

    //    public async override Task Invoke(AspectContext context, AspectDelegate next)
    //    {
    //        try
    //        {
    //            Console.WriteLine("Before service call");
    //            await next(context);
    //        }
    //        catch (Exception)
    //        {
    //            Console.WriteLine("Service threw an exception!");
    //            throw;
    //        }
    //        finally
    //        {
    //            Console.WriteLine("After service call");
    //        }
    //    }
    //}








}
