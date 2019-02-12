using Abp.AutoMapper;
using NetCoreFrame.Core;
using System.ComponentModel.DataAnnotations;

namespace NetCoreFrame.Application
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
        [Required(ErrorMessage="请输入用户账号")]
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
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }

        /// <summary>
        /// 返回页面的地址
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 自定义请求的数据
        /// </summary>
        public dynamic RequestData { get; set; }

    }
}
