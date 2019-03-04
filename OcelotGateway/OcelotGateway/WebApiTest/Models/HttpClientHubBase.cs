using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTest.Models
{
    public class HttpClientHubBase
    {

        public HttpClient _httpClient { get;  set; }
        
        //url扩展地址
        //private string BaseAddressExtend = "";

        public HttpClientHubBase()
        {
            _httpClient = HttpClientFactory.Create();

        }

        #region POST请求
        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public StringContent SetParam(object requestData)
        {
            StringContent dataContent = null;
            if (requestData != null)
            {
                //设置首字母小写
                var setting = new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                };

                //var jsonData =JsonConvert.SerializeObject(requestData);
                dataContent = new StringContent(
                  JsonConvert.SerializeObject(requestData, Formatting.None, setting),
                   Encoding.UTF8,
                   "application/json");
            }
            return dataContent;
        }

        /// <summary>
        /// POST请求
        /// 返货json字符串
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public async Task<string> PostDataBase(string requestUri, object requestData)
        {
            StringContent dataContent = SetParam(requestData);

            try
            {
                var response = await _httpClient.PostAsync(requestUri, dataContent);
                //HTTP成功状态值
                var result = response.EnsureSuccessStatusCode();
                if (!result.IsSuccessStatusCode)
                {
                    return "";
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("服务请求异常:"+ ex.Message);
            }
        }

        /// <summary>
        /// Post请求(异步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri"></param>
        /// <param name="requestData"></param>
        /// <returns>返回json字符串</returns>
        public async Task<T> PostDataAsync<T>(string requestUri, object requestData = null)
        {
            var resultJson = await PostDataBase(requestUri, requestData);
            return JsonConvert.DeserializeObject<T>(resultJson);
        }

        /// <summary>
        /// Post请求(同步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri"></param>
        /// <param name="requestData"></param>
        /// <returns>返回json字符串</returns>
        public T PostData<T>(string requestUri, object requestData)
        {
            var resultJson = PostDataBase(requestUri, requestData).GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<T>(resultJson);
        }

        #endregion

        #region GET请求
        /// <summary>
        /// GET请求
        /// 返回json字符串
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public async Task<string> GetDataBase(string requestUri)
        {
            try
            {
                var response = await _httpClient.GetAsync(requestUri);
                //HTTP成功状态值
                var result = response.EnsureSuccessStatusCode();
                if (!result.IsSuccessStatusCode)
                {
                    return "";
                }
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("服务请求异常:" + ex.Message);
            }
        }

        /// <summary>
        /// GET请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public async Task<T> GetDataAsync<T>(string requestUri)
        {
            var resultJson = await GetDataBase(requestUri);
            return JsonConvert.DeserializeObject<T>(resultJson);
        }

        /// <summary>
        /// GET请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public T GetData<T>(string requestUri)
        {
            var resultJson = GetDataBase(requestUri).GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<T>(resultJson);
        }
        #endregion



    }
}