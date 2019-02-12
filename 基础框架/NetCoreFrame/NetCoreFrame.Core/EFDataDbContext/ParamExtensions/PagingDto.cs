using Abp.Application.Services.Dto;

namespace NetCoreFrame.Core
{
    public class PagingDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 当前页索引
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 跳转索引
        /// </summary>
        public override int SkipCount
        {
            get
            {
                if (PageIndex == 0)
                {
                    PageIndex = 1;
                }
                if (MaxResultCount == 0)
                {
                    MaxResultCount = 20;
                }
                return (PageIndex - 1) * MaxResultCount;
            }
        }

      

    }
}
