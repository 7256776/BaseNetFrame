using System;

namespace Frame.Core
{
    /// <summary>
    /// 设置自定义属性标签 忽略webapi创建
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class IgnoreFrameApi : Attribute
    {
       



    }

}

