using Abp.Auditing;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Sample
{
    /// <summary>
    /// 
    /// </summary>
    public class FormParam
    {
        public virtual string TableName { get; set; }
       
        public virtual IDictionary<string, object> Fields { get; set; }


    }
}