using Abp.Localization;
using System;
using System.Configuration;

namespace Frame.Core
{
    /// <summary>
    /// 常量设置对象
    /// </summary>
    public static class ConstantConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static string DefaultPassword = "888888";

        /// <summary>
        /// 
        /// </summary>
        public static string LocalizationName = "J_LOCALIZATION";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ILocalizableString L(this string name)
        {
            return new LocalizableString(name, LocalizationName);
        }

        /// <summary>
        /// 数据库对象名称 SqlServer通常是 dbo; Oracle数据库通常是用户名称
        /// </summary>
        public static string DBSchemaName = ConfigurationManager.AppSettings["DBSchemaName"];

        /// <summary>
        /// 设置WebApi授权ToKen有效时间 单位/分钟
        /// </summary>
        public static int WebApiExpires = Convert.ToInt32(ConfigurationManager.AppSettings["WebApiExpires"]);

        #region Setting配置信息名称
        /// <summary>
        /// 是否启用多语言(默认启用)
        /// </summary>
        public readonly static string ProjectName = "ProjectName";

        /// <summary>
        /// 是否启用多语言(默认启用)
        /// </summary>
        public readonly static string IsMultilingual = "IsMultilingual";

        /// <summary>
        /// 布局类型  side = 侧边菜单 ;  top  = 顶部菜单
        /// </summary>
        public readonly static string FrameLayout = "FrameLayout";

        /// <summary>
        /// 菜单布局采用top方式时候使用;   设置子菜单的列数
        /// </summary>
        public readonly static string FrameTopLayoutMenusCol = "FrameTopLayoutMenusCol";

        /// <summary>
        /// 菜单布局采用top方式时候使用;  设置子菜单是否自动撑满页面宽度
        /// </summary>
        public readonly static string FrameTopLayoutMenusColFull = "FrameTopLayoutMenusColFull";

        /// <summary>
        /// 菜单布局采用top方式时候使用;  设置子菜单展开的方式  tile = 平铺     folding = 折叠 
        /// </summary>
        public readonly static string FrameTopLayoutMenusType = "FrameTopLayoutMenusType";
        #endregion

    }


}
