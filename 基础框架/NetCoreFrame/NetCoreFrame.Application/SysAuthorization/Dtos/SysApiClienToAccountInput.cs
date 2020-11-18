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
 	/// Api客户授权账号对象
 	/// </summary>
    [AutoMap(typeof(SysApiClienToAccount))]
    public class SysApiClienToAccountInput
    {
        public SysApiClienToAccountInput()
        {
        }

        /// <summary>
        /// 账号主键
        /// </summary>
        [Required(ErrorMessage = "请输入账号ID")]
        public virtual Guid? ApiAccountId { get; set; }

        /// <summary>
        /// 服务客户主键
        /// </summary>		
        [Required(ErrorMessage = "请输入客户ID")]
        public virtual Guid? ApiClientId { get; set; }



    }
}
