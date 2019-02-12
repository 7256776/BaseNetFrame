using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Json;
using System.Reflection;

namespace NetCoreFrame.Core
{

    public class NetCoreFrameLocalizationConfigurer
    {

        /// <summary>
        /// 需要提供本地化资源文件所在程序集对象,以及程序集命名空间名称+文件夹名称
        /// 本地嵌入式资源方式获取本地化资源
        /// 注:本地化的资源文件必须是在所提供的程序集下
        /// </summary>
        /// <param name="localizationConfiguration"></param>
        /// <param name="_assembly">通常是调用该方法的 Assembly.GetExecutingAssembly()</param>
        /// <param name="rootNamespace">命名空间的名称+资源文件根目录 ("NetCoreFrame.Web.Localization.XmlSource"或"NetCoreFrame.Web.Localization.JsonSource")</param>
        public static void LocalEmbedded(ILocalizationConfiguration localizationConfiguration, Assembly _assembly, string rootNamespace)
        {
            //获取当前程序集对象
            //Assembly _assembly = Assembly.GetExecutingAssembly(); 

            //xml或json二选一

            //使用本地嵌入式资源文件(Xml)
            //localizationConfiguration.Sources.Add(
            //    new DictionaryBasedLocalizationSource(
            //        ConstantConfig.LocalizationName,
            //        new XmlEmbeddedFileLocalizationDictionaryProvider(
            //            _assembly,
            //            rootNamespace
            //        )
            //    )
            //);

            //使用本地嵌入式资源文件(Json)
            localizationConfiguration.Sources.Add(
               new DictionaryBasedLocalizationSource(
                    ConstantConfig.LocalizationName,
                   new JsonEmbeddedFileLocalizationDictionaryProvider(
                        _assembly,
                       rootNamespace
                       )
                   )
               );
        }

        /// <summary>
        /// 本地文件路径方式获取本地化资源
        /// </summary>
        /// <param name="localizationConfiguration"></param>
        public static void LocalPath(ILocalizationConfiguration localizationConfiguration)
        {
            //xml或json二选一

            //使用本地文件Xml文件
            //localizationConfiguration.Sources.Add(
            //    new DictionaryBasedLocalizationSource(
            //        ConstantConfig.LocalizationName,
            //        new XmlFileLocalizationDictionaryProvider(
            //           ConstantConfig.WebContentRootPath + "/Localization/XmlSource"
            //         )
            //    )
            // );

            //使用本地文件Json文件
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    ConstantConfig.LocalizationName,
                    new JsonFileLocalizationDictionaryProvider(
                       ConstantConfig.WebContentRootPath + "/Localization/JsonSource"
                     )
                )
             );

        }


    }
}
