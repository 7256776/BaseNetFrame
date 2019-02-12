using Abp.AutoMapper;
using Frame.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Frame.Application
{
    [AutoMap(typeof(SysRoles))]
    public class RoleInput
    {

        /// <summary>
        /// ID
        /// </summary>	
        public long? ID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string RoleName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>		
        public string Description { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>		
        public int? TenantId { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>		
        public bool IsActive { get; set; }

        /// <summary>
        /// 角色授权的菜单以及动作集合
        /// </summary>
        public List<MenusOut> MenusActionList { get; set; }

    }
}
