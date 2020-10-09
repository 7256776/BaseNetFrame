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
    public class FrameLocalizationDictionary : ILocalizationDictionary// LocalizationDictionary
    {
        public CultureInfo CultureInfo { get; private set; }

        public virtual string this[string name]
        {
            get
            {
                var localizedString = GetOrNull(name);
                return localizedString?.Value;
            }
            set
            {
                _dictionary[name] = new LocalizedString(name, value, CultureInfo);
            }
        }

        private readonly Dictionary<string, LocalizedString> _dictionary;


        public FrameLocalizationDictionary(CultureInfo cultureInfo) 
        {
            CultureInfo = cultureInfo;
            _dictionary = new Dictionary<string, LocalizedString>();
        }


        public IReadOnlyList<LocalizedString> GetAllStrings()
        {
            //此处获取所有的语言信息
            List<LocalizedString> list = new List<LocalizedString>();
            return list;
        }

        public LocalizedString GetOrNull(string name)
        {
            //此处获取如果为空时候的语言转义
            return new LocalizedString("","", CultureInfo);
        }

        public IReadOnlyList<LocalizedString> GetStringsOrNull(List<string> names)
        {
            throw new NotImplementedException();
        }


    }
}
