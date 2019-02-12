using Abp;
using Abp.Application.Services.Dto;
using Newtonsoft.Json;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class RequestParamExtensions
    {

        /// <summary>
        /// 默认转换 Params 参数为T对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        public static T ToEntityObject<T>(this RequestParam<dynamic> requestParam)
        {
            Check.NotNull(requestParam.Params, nameof(requestParam.Params));
            return JsonConvert.DeserializeObject<T>(requestParam.Params.ToString());
        }

        /// <summary>
        /// 转换 Params 参数对应的 <paramref name="paramName"/> 为T对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestParam"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static T ToEntityObject<T>(this RequestParam<dynamic> requestParam, string paramName)
        {
            Check.NotNull(requestParam.Params[paramName], nameof(requestParam));
            return JsonConvert.DeserializeObject<T>(requestParam.Params[paramName].ToString());
        }



    }
}
