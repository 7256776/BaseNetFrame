using Abp.Auditing;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Extensions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Sample
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMap(typeof(FieldInfo))]
    public class FieldInfoDto 
    {
        public virtual Guid? Id { get; set; }

        public virtual string FieldName { get; set; }
       
        public virtual string FieldCode { get; set; }
        
        public virtual string FieldJson { get; set; }

        public virtual Guid? TableId { get; set; }

        public virtual int FieldOrder { get; set; }
        public virtual string TableName { get; set; }

        public virtual string TableCode { get; set; }

        public virtual bool IsField { get; set; }



    }


}