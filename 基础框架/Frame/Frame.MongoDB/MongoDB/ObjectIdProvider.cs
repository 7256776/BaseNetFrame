using MongoDB.Bson;
using System;
using System.Web.Mvc;

namespace Frame.MongoDB
{
    /// <summary>
    /// 设置mvc在获取请求时,进行反序列化绑定
    /// </summary>
    public class ObjectIdProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            if (modelType == typeof(ObjectId) || modelType == typeof(ObjectId?))
            {
                return new ObjectIdModelBinder();
            }
            return null;
        }
    }




}

