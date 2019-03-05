using AspectCore.DynamicProxy;
using Castle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAuthService;

namespace WebAop.Aop
{
    public interface IIgnoreService
    {
        [CustomInterceptor]
        void GetA(SysUser user);

        void GetB();

        void GetC();

    }

    public class IgnoreService : IIgnoreService
    {
        public void GetA(SysUser user)
        {
        }

        public void GetB()
        {
        }
        public void GetC()
        {
        }

    }








}
