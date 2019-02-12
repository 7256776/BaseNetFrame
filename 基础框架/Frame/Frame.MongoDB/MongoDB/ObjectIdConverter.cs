using MongoDB.Bson;
using Newtonsoft.Json;
using System;

namespace Frame.MongoDB
{

    /// <summary>
    /// 设置json转换 ObjectId使用方式给实体对象的属性添加标签
    /// [JsonConverter(typeof(ObjectIdConverter))]
    /// </summary>
    public class ObjectIdConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToString());
        }

        /// <summary>
        //  反序列化时将字符串转换成ObjectId类型
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object obj = serializer.Deserialize(reader);
            if (obj == null)
            {
                return null;
            }
            return new ObjectId(serializer.Deserialize(reader).ToString());
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(ObjectId).IsAssignableFrom(objectType);
        }
    }


}

