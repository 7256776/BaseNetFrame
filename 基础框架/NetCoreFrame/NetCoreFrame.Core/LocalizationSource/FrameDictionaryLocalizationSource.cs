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

        public void Extend(ILocalizationDictionary dictionary)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<LocalizedString> GetAllStrings(bool includeDefaults = true)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<LocalizedString> GetAllStrings(CultureInfo culture, bool includeDefaults = true)
        {
            throw new NotImplementedException();
        }

        public string GetString(string name)
        {
            throw new NotImplementedException();
        }

        public string GetString(string name, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public string GetStringOrNull(string name, bool tryDefaults = true)
        {
            throw new NotImplementedException();
        }

        public string GetStringOrNull(string name, CultureInfo culture, bool tryDefaults = true)
        {
            throw new NotImplementedException();
        }

        public List<string> GetStrings(List<string> names)
        {
            throw new NotImplementedException();
        }

        public List<string> GetStrings(List<string> names, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public List<string> GetStringsOrNull(List<string> names, bool tryDefaults = true)
        {
            throw new NotImplementedException();
        }

        public List<string> GetStringsOrNull(List<string> names, CultureInfo culture, bool tryDefaults = true)
        {
            throw new NotImplementedException();
        }

        public void Initialize(ILocalizationConfiguration configuration, IIocResolver iocResolver)
        {
            throw new NotImplementedException();
        }
    }
}
