using Abp.AutoMapper;
using NetCoreFrame.Core;
using System.ComponentModel.DataAnnotations;

namespace NetCoreFrame.Application
{
    [AutoMap(typeof(SysMenuAction))]
    public class MenuAction
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
        /// 动作标题
        /// </summary>	 
        [Required(ErrorMessage = "请输入动作标题")]
        [StringLength(50, ErrorMessage = "动作标题长度超过50")]
        public string ActionDisplayName { get; set; }

        /// <summary>
        /// 动作名称
        /// </summary>	 
        [Required(ErrorMessage = "请输入动作名称")]
        [StringLength(50, ErrorMessage = "动作名称长度超过50")]
        public string ActionName { get; set; }

        /// <summary>
        /// 授权名称
        /// </summary>	 
        public string PermissionName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>	 
        public string Description { get; set; }

        /// <summary>
        /// 授权类型:1=开放模式 2=登录模式 3=登录模式
        /// </summary>	 
        [Required(ErrorMessage = "请输入授权类型")]
        public string RequiresAuthModel { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>	 
        public bool? IsActive { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public  bool IsCheck { get; set; }

    }
}
