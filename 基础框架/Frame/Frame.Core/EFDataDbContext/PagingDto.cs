using Abp.Application.Services.Dto;

namespace Frame.Core
{
    public class PagingDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 当前页索引
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 跳转索引
        /// </summary>
        public override int SkipCount
        {
            get
            {
                return (PageIndex - 1) * MaxResultCount;
            }
        }

      

    }
}
