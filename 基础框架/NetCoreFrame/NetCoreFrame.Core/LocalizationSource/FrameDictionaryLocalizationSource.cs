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
    public class FrameDictionaryLocalizationSource : IDictionaryBasedLocalizationSource
    {
        public ILocalizationDictionaryProvider DictionaryProvider { get; }
        //protected ILocalizationConfiguration LocalizationConfiguration { get; private set; }

        public string Name { get; set; }


        public FrameDictionaryLocalizationSource(string name, ILocalizationDictionaryProvider dictionaryProvider)
        {
            Name = name;
            DictionaryProvider = dictionaryProvider;
        }


        public void Initialize(ILocalizationConfiguration configuration, IIocResolver iocResolver)
        {
            //初始化 数据提供者
            DictionaryProvider.Initialize(Name);
        }


        public void Extend(ILocalizationDictionary dictionary)
        {
         
        }

        public IReadOnlyList<LocalizedString> GetAllStrings(bool includeDefaults = true)
        {
            List<LocalizedString> list = new List<LocalizedString>();
            return list;
        }

        public IReadOnlyList<LocalizedString> GetAllStrings(CultureInfo culture, bool includeDefaults = true)
        {
            List<LocalizedString> list = new List<LocalizedString>();
            return list;
        }

        public string GetString(string name)
        {
            return name;
        }

        public string GetString(string name, CultureInfo culture)
        {
            return name;
        }

        public string GetStringOrNull(string name, bool tryDefaults = true)
        {
            return name;
        }

        public string GetStringOrNull(string name, CultureInfo culture, bool tryDefaults = true)
        {
            return name;
        }


    }
}
