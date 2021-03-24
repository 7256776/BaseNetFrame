using Abp.AutoMapper;
using NetCoreFrame.Core;
using System.ComponentModel.DataAnnotations;

namespace NetCoreFrame.Application
{
    [AutoMap(typeof(UserInfo))]
    public class UserInfoSettingInput
    {
        /// <summary>
        /// ID
        /// </summary>	
        public long ID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>	
        //[Required]
        [Required(ErrorMessage = "请输入用户名称")]
        [StringLength(50, ErrorMessage = "用户名称长度超过50")]
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

        /// <summary>
        /// 旧密码
        /// </summary>
        [Required(ErrorMessage = "请输入旧密码")]
        public string OldPass { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "请输入新密码")]
        public string NewPass { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Required(ErrorMessage = "请输入确认密码")] 
        public string CheckPass { get; set; }
    }

}
