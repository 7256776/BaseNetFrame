
namespace WebApiTest.Models
{
    /// <summary>
    /// 请求参数
    /// 分页参数
    /// </summary>
    public class RequestData
    {
    
    }

    public class RequestPageData: RequestData
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }

    /// <summary>
    /// 请求参数对象
    /// 多参数 + 分页参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RequestData<T> : RequestPageData
    {
        public RequestData()   {   }

        public RequestData(T data)
        {
            Data = data;
        }

        public T Data { get; set; }

    }




}
