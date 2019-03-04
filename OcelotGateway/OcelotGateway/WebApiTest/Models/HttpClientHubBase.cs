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
        
        //url��չ��ַ
        //private string BaseAddressExtend = "";

        public HttpClientHubBase()
        {
            _httpClient = HttpClientFactory.Create();

        }

        #region POST����
        /// <summary>
        /// ���ò���
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public StringContent SetParam(object requestData)
        {
            StringContent dataContent = null;
            if (requestData != null)
            {
                //��������ĸСд
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
        /// POST����
        /// ����json�ַ���
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
                //HTTP�ɹ�״ֵ̬
                var result = response.EnsureSuccessStatusCode();
                if (!result.IsSuccessStatusCode)
                {
                    return "";
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("���������쳣:"+ ex.Message);
            }
        }

        /// <summary>
        /// Post����(�첽)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri"></param>
        /// <param name="requestData"></param>
        /// <returns>����json�ַ���</returns>
        public async Task<T> PostDataAsync<T>(string requestUri, object requestData = null)
        {
            var resultJson = await PostDataBase(requestUri, requestData);
            return JsonConvert.DeserializeObject<T>(resultJson);
        }

        /// <summary>
        /// Post����(ͬ��)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri"></param>
        /// <param name="requestData"></param>
        /// <returns>����json�ַ���</returns>
        public T PostData<T>(string requestUri, object requestData)
        {
            var resultJson = PostDataBase(requestUri, requestData).GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<T>(resultJson);
        }

        #endregion

        #region GET����
        /// <summary>
        /// GET����
        /// ����json�ַ���
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public async Task<string> GetDataBase(string requestUri)
        {
            try
            {
                var response = await _httpClient.GetAsync(requestUri);
                //HTTP�ɹ�״ֵ̬
                var result = response.EnsureSuccessStatusCode();
                if (!result.IsSuccessStatusCode)
                {
                    return "";
                }
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("���������쳣:" + ex.Message);
            }
        }

        /// <summary>
        /// GET����
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
        /// GET����
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