using Abp.Application.Services.Dto;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 请求参数
    /// 分页参数
    /// </summary>
    public class RequestParam 
    {
        public PagingDto PagingDto { get; set; }
    }

    /// <summary>
    /// 请求参数对象
    /// 多参数 + 分页参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RequestParam<T> : RequestParam
    {
        public T Params { get; set; }

    }




}
