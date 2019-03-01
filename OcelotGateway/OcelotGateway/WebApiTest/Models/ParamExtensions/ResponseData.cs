
namespace WebApiTest.Models
{
    /// <summary>
    /// 请求参数
    /// </summary>
    public class ResponseData
    {
        public ResponseData()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        private bool success;

        /// <summary>
        /// 返回状态
        /// </summary>
        public bool Success
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Code) && Code == "200")
                {
                    return true;
                }
                return success;
            }
            set { success = value; }
        }

        /// <summary>
        /// 错误编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 错误消息描述
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 错误消息描述
        /// </summary>
        public string Msg { get; set; }

    }

    /// <summary>
    /// 请求参数对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseData<T> : ResponseData
    {
        public ResponseData()
        {

        }

        public T Data { get; set; }

    }

}
