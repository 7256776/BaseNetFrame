using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace NetCoreWorkFlow.Core
{
    public class FlowPagingParam<T>
    {
        /// <summary>
        /// 当前页索引
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页 数据条数
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// 返回数据对象
        /// </summary>
        public T Params { get; set; }
    }

    public class FlowPagingResult<T>
    {
 
        /// <summary>
        /// 总数据条数
        /// </summary>
        public int TotalCount { get; set; } = 20;

        /// <summary>
        /// 返回数据对象
        /// </summary>
        public IReadOnlyList<T> ResultData { get; set; }

    }

}
