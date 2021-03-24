using Abp.AutoMapper;
using NetCoreFrame.Core;
using System.ComponentModel.DataAnnotations;

namespace NetCoreFrame.Application
{
    [AutoMap(
        typeof(SysMenuAction),
        typeof(SysMenusInput)
        )]
    public class SysMenuParam
    {

        /// <summary>
        /// 主键(用于映射模块表ID)
        /// </summary>	 
        public long? Id { get; set; }

        /// <summary>
        /// 模块主键ID
        /// </summary>	 
        public long? MenuID { get; set; }

        /// <summary>
        /// 授权名称
        /// </summary>	 
        public string PermissionName { get; set; }

     

    }
}
