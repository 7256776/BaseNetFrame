using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Web;
using System.Xml.Serialization;

namespace NetCoreFrame.Core
{
    public class UtilityCommon
    {

        /// <summary>
        /// xml实体文件转换对象
        /// 转换的对象需要设置标签 
        /// [XmlRoot("根节点对象类型名称")]  
        /// [XmlType("对象类型名称")] 
        /// [XmlAttribute("属性名称")]  
        /// [XmlElement("对象类型名称")]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlUrl"></param>
        /// <returns></returns>
        public static T DeserializeXml<T>(string xmlUrl)
        {
            //获取目录物理路径
            //string xmlUrl1 = HttpContext.Current.Server.MapPath("/");
            //或目录下所有xml类型的文件
            //string[] fileNamesList = Directory.GetFiles(xmlUrl1, "*.xml", SearchOption.TopDirectoryOnly);

            //获取文件物理路径
            string xmlServerUrl = ConstantConfig.WebContentRootPath + xmlUrl;
            using (FileStream fs = File.Open(xmlServerUrl, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs);
                string xml = sr.ReadToEnd();
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StringReader reader = new StringReader(xml))
                {
                    T myObject = (T)serializer.Deserialize(reader);
                    reader.Close();
                    return myObject;
                }
            }
        }

        /// <summary>
        /// json实体文件转换对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonUrl"></param>
        /// <returns></returns>
        public static T DeserializeJson<T>(string jsonUrl)
        {
            //获取文件物理路径
            string serverUrl = ConstantConfig.WebContentRootPath + jsonUrl;
            if (!File.Exists(serverUrl))
            {
                return default(T);
            }
            using (FileStream fs = File.Open(serverUrl, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs);
                string json = sr.ReadToEnd();
                T model = (T)JsonConvert.DeserializeObject(json, typeof(T));
                return model;
            }
        }

    }
}
