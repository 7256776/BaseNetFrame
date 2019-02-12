using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Extensions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Sample
{
    /// <summary>
    /// 
    /// </summary>
    [Table("FIELDINFO")]
    public class FieldInfo : Entity<Guid>
    {
       
        [Column("FieldName")]
        public virtual string FieldName { get; set; }

       
        [Column("FieldCode")]
        public virtual string FieldCode { get; set; }

        
        [Column("FieldJson")]
        public virtual string FieldJson { get; set; }


        [Column("TABLEID")]
        public virtual Guid TableId { get; set; }

        [Column("FIELDORDER")]
        public virtual int FieldOrder { get; set; }

        [NotMapped]
        public virtual string TableName { get; set; }

        [NotMapped]
        public virtual string TableCode { get; set; }

        [NotMapped]
        public virtual bool IsField { get; set; }


    }


}