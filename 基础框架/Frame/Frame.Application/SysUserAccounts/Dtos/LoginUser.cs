using Abp.AutoMapper;
using Frame.Core;

namespace Frame.Application
{
    [AutoMap(typeof(UserInfo))]
    public class LoginUser
    {
        /// <summary>
        /// 
        /// </summary>
        //public long ID { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public  string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>		
        public  string UserNameCn { get; set; }

        /// <summary>
        /// 是否记住密码
        /// </summary>		
        public  bool IsPersistent { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>		
        public  int? TenantId { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>		
        public string Password { get; set; }

    }
}
