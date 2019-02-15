using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
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
        [StringLength(100)]
        public virtual string FieldName { get; set; }

       
        [Column("FieldCode")]
        [StringLength(100)]
        public virtual string FieldCode { get; set; }

        
        [Column("FieldJson")]
        [StringLength(4000)]
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