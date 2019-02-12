using Abp.AutoMapper;
using NetCoreFrame.Core;
using System.ComponentModel.DataAnnotations;

namespace NetCoreFrame.Application
{
    [AutoMap(typeof(UserInfo))]
    public class UserInput 
    { 
        public UserInput()
        {
            IsDeleted = false;
        }

        /// <summary>
        /// ID
        /// </summary>	
        public long? ID { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        [Required(ErrorMessage = "请输入用户账号")]
        [RegularExpression("^[0-9a-zA-Z_]+$", ErrorMessage = "账号只能由字母，数字，下划线组成")]
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>	
        [Required]
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
        /// 所属部门
        /// </summary>		
        public string OrgCode { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>		
        public bool IsActive { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>		
        public bool IsDeleted { get; set; }


        /// <summary>
        /// 
        /// </summary>		
        public string UserInfoExtensJson { get; set; }

        /*
         * 自定义验证方式
         * 1） 实现ICustomValidate接口
         * 2） 在AddValidationErrors方法实现写具体验证规则
         * public void AddValidationErrors(CustomValidationContext context)
         * {
         *     if (验证规则)
         *     {
         *         context.Results.Add(new ValidationResult("返回错误消息!"));
         *     }
         * }
         */




    }
}
