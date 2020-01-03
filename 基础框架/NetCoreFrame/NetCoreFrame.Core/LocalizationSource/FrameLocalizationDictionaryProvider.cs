using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Sources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// ToDevelop 
    /// 数据库获取多语言(待完成)
    /// </summary>
    public class FrameLocalizationDictionaryProvider : ILocalizationDictionaryProvider
    {
        public ILocalizationDictionary DefaultDictionary { get; set; }

        public IDictionary<string, ILocalizationDictionary> Dictionaries { get; private set; }


        public FrameLocalizationDictionaryProvider()
        {
            Dictionaries = new Dictionary<string, ILocalizationDictionary>();

            CultureInfo cc = CultureInfo.CurrentCulture;
            DefaultDictionary = new FrameLocalizationDictionary(cc);
        }

        public void Extend(ILocalizationDictionary dictionary)
        {
            //扩展方法
        }

        public void Initialize(string sourceName)
        {
            //初始化语言集合
            DefaultDictionary.GetAllStrings();
        }

    









    }
}
