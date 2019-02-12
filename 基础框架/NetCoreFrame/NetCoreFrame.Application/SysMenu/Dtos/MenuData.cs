using Abp.AutoMapper;
using NetCoreFrame.Core;
using System.ComponentModel.DataAnnotations;

namespace NetCoreFrame.Application
{
    [AutoMap(
        typeof(SysMenuAction),
        typeof(MenusInput)
        )]
    public class MenuData
    {

        /// <summary>
        /// 主键
        /// </summary>	 
        public long? Id { get; set; }

        /// <summary>
        /// 模块主键
        /// </summary>	 
        public long? MenuID { get; set; }

        /// <summary>
        /// 授权名称
        /// </summary>	 
        public string PermissionName { get; set; }

     

    }
}
