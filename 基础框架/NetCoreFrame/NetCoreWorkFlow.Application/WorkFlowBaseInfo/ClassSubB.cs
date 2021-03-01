using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreWorkFlow.Application
{
    public class ClassSubB : ClassMain
    {

        public override void GetA()
        {
            base.GetA();
        }

        public override void GetB()
        {

        }

        /// <summary>
        /// ToDo 通过函数获取数据源
        /// </summary>
        public void get()
        {
            var aassa = System.Reflection.Assembly.Load("NetCoreWorkFlow.Application").CreateInstance("NetCoreWorkFlow.Application.ClassSubB");
            ClassSubB cc1 = new ClassSubB();

           

            //需要注入
            IIocResolver _iocResolver = null;

            //获取当前类库下所有类型, 获取非抽象类 排除接口继承
            var types = typeof(ClassMain).Assembly.GetTypes().Where(t => t.BaseType == typeof(ClassMain) && !t.IsAbstract && t.IsClass);

            var classList = types.ToList();

            //创造实例，并返回结果
            //var obj =  System.Activator.CreateInstance(dd[0]) as ClassMain;


            using (var personService2 = _iocResolver.ResolveAsDisposable(classList[0]))
            {
                var asdaada = personService2.Object as ClassMain;
                asdaada.GetA();
            }

            using (var personService2 = _iocResolver.ResolveAsDisposable(classList[1]))
            {
                var asdaada = personService2.Object as ClassMain;
                asdaada.GetA();
            }
        }


    }
}
