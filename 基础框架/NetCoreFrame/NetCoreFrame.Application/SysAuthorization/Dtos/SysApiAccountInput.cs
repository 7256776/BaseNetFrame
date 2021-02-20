using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Application
{
    /// <summary>
    /// Api授权客户对象
    /// </summary>
    [AutoMap(typeof(SysApiAccount))]
    public class SysApiAccountInput 
    {
        public SysApiAccountInput()
        {
            IsActive = true;
        }

        /// <summary>
        /// ID
        /// </summary>	
        public Guid? Id { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        [Required(ErrorMessage = "请输入账号名称")]
        [StringLength(100, ErrorMessage = "账号名称长度超过100")]
        public virtual string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>		
        [Required(ErrorMessage = "请输入账号密码")]
        [StringLength(100, ErrorMessage = "账号密码长度超过100")]
        public virtual string Password { get; set; }

        /// <summary>
        /// 描述
        /// </summary>	
        [StringLength(1000)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>	
        [StringLength(4000)]
        public virtual string ExtensionData { get; set; }

        /// <summary>
        /// 账号关联客户
        /// </summary>	
        public virtual string ApiClientId { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>		
        [Required]
        public virtual bool IsActive { get; set; } = true;
         
         
    }
}
