using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreWorkFlow.Application
{
    public abstract class ClassMain: ITransientDependency
    {

        public virtual void GetA()
        {

        }

        public abstract void GetB();

    }
}
