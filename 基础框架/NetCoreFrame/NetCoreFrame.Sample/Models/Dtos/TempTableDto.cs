using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Sample
{
    /// <summary>
    /// 
    /// </summary>
    public class TempTableDto
    {
        public virtual long? Id { get; set; }

        public virtual string TextData { get; set; }
       
        public virtual string TextAreaData { get; set; }

        
        public virtual string NumberData { get; set; }

        //[JsonConverter(typeof(LongDateTimeConvert))]
        public virtual DateTime DateTimeData { get; set; }

        public virtual string DropdownListData { get; set; }

        public virtual string DropdownMultipleListData { get; set; }

        public virtual string TextDataEx { get; set; }

        public virtual decimal? NumberDataEx { get; set; }

        public virtual string TextAreaDataEx { get; set; }

    }

    public class LongDateTimeConvert : IsoDateTimeConverter
    {
        public LongDateTimeConvert() : base()
        {
            base.DateTimeFormat = "yyyy/MM/dd HH:mm";
        }
    }

}