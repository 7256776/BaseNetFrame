using Abp.Localization;
using System;
using System.Configuration;

namespace NetCoreWorkFlow.Core
{
    /// <summary>
    /// 常量设置对象
    /// </summary>
    public static class ConstantConfig
    {
        /// <summary>
        /// 默认本地化资源名称
        /// 该名称必须与本地化资源文件名称的前缀一致
        /// </summary>
        public static string LocalizationName = "FrameLocalization";

        /// <summary>
        /// 转义需要转换的本地化语言
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ILocalizableString ToLocalizable(this string name)
        {
            return new LocalizableString(name, LocalizationName);
        }

        /// <summary>
        /// 默认数据库配置 名称
        /// </summary>
        public static string DBDefaultName = "Default";

        /// <summary>
        /// 数据库对象名称 SqlServer通常是 dbo; Oracle数据库通常是用户名称
        /// ConfigurationManager.AppSettings["DBSchemaName"];
        /// </summary>
        public static string DBSchemaName = ""; 

        

    }


}
