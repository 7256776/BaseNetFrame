using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Sample
{
    /// <summary>
    /// 
    /// </summary>
    [Table("TABLEINFO")]
    public class TableInfo : Entity<Guid>
    {
     
        [Column("TableName")]
        public virtual string TableName { get; set; }

        [Column("TableCode")]
        public virtual string TableCode { get; set; }

        [Column("TableType")]
        public virtual string TableType { get; set; }

        [ForeignKey("TableId")]
        public virtual ICollection<FieldInfo> FieldInfoList { get; set; }

    }
}
