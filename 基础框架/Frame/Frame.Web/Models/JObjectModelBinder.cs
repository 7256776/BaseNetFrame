using Newtonsoft.Json;
using System.IO;
using System.Web.Mvc;

namespace Frame.Web
{
    public class JObjectModelBinder : IModelBinder
    {
        /// <summary>
        /// Mvc异步请求获取JObject类型数据
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var stream = controllerContext.RequestContext.HttpContext.Request.InputStream;
            stream.Seek(0, SeekOrigin.Begin);
            string json = new StreamReader(stream).ReadToEnd();
            return JsonConvert.DeserializeObject<dynamic>(json);
        }
    }

}

