using Abp.Localization;
using System;
using System.Configuration;

namespace NetCoreFrame.Core
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
        /// 默认本地化资源名称
        /// 该名称必须与本地化资源文件名称的前缀一致
        /// </summary>
        public static string LocalizationName = "FrameLocalization";

        /// <summary>
        /// 项目wwwroot物理路径
        /// </summary>
        public static string WebWWWrootPath = "";

        /// <summary>
        /// 项目根目录物理路径
        /// </summary>
        public static string WebContentRootPath = "";

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
        /// 默认数据配置 名称
        /// </summary>
        public static string DBDefaultName = "Default";

        /// <summary>
        /// 数据库对象名称 SqlServer通常是 dbo; Oracle数据库通常是用户名称
        /// </summary>
        public static string DBSchemaName = "";// ConfigurationManager.AppSettings["DBSchemaName"];

        /// <summary>
        /// Jwt密钥加密使用 DefaultPassPhrase
        /// 这货似乎是吧token加密一一次,便于那种需要在网络间传输token的应用
        /// gsKxGZ012HLL3MI5
        /// </summary>
        public static string DefaultPassPhrase = "jiamishiyongdekey";

        #region Setting配置信息名称

        /// <summary>
        /// 公司名称
        /// </summary>
        public readonly static string CompanyName = "ProjectName";

        /// <summary>
        /// 公司网站
        /// </summary>
        public readonly static string CompanyWWW = "CompanyWWW";

        /// <summary>
        /// 项目名称
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
