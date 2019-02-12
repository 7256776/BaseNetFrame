using Abp.AutoMapper;
using Frame.Core;

namespace Frame.Application
{
    [AutoMap(typeof(UserInfo))]
    public class UserInfoInput
    {
        /// <summary>
        /// ID
        /// </summary>	
        public long ID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>	
        //[Required]
        public string UserNameCn { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>		
        public int? TenantId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>		
        public string Sex { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>		
        public string EmailAddress { get; set; }

        /// <summary>
        /// 描述
        /// </summary>		
        public string Description { get; set; }

        /// <summary>
        /// 电话
        /// </summary>		
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>		
        public bool IsActive { get; set; }

    }


    public class UserPassInput
    {
        /// <summary>
        /// ID
        /// </summary>	
        public long ID { get; set; }
        public string OldPass { get; set; }
        public string NewPass { get; set; }
        public string CheckPass { get; set; }
    }

}
