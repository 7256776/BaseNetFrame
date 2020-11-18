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
 	/// Api资源授权客户对象
 	/// </summary>
    [AutoMap(typeof(SysApiResourceToClient))]
    public class SysApiResourceToClientInput 
    {
        public SysApiResourceToClientInput()
        {
        }

        /// <summary>
        /// 资源服务主键
        /// </summary>
        [Required(ErrorMessage = "请输入资源服务ID")]
        public virtual Guid? ApiResourceId { get; set; }

        /// <summary>
        /// 服务客户主键
        /// </summary>		
        [Required(ErrorMessage = "请输入客户ID")]
        public virtual Guid? ApiClientId { get; set; }



    }
}
