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
    [Table("TEMPTABLE")]
    public class TempTable : Entity<long>
    {
         [Column("TEXTDATA")]
        public virtual string TextData { get; set; }
       
        [Column("TEXTAREADATA")]
        public virtual string TextAreaData { get; set; }
        
        [Column("NUMBERDATA")]
        public virtual decimal?  NumberData { get; set; }

        [Column("DATETIMEDATA")]
        public virtual DateTime? DateTimeData { get; set; }

        [Column("DROPDOWNLISTDATA")]
        public virtual string DropdownListData { get; set; }

        [Column("DROPDOWNMULTIPLELISTDATA")]
        public virtual string DropdownMultipleListData { get; set; }


        [Column("TEXTDATAEX")]
        public virtual string TextDataEx { get; set; }

        [Column("NUMBERDATAEX")]
        public virtual decimal? NumberDataEx { get; set; }

        [Column("TEXTAREADATAEX")]
        public virtual string TextAreaDataEx { get; set; }

    } 
}