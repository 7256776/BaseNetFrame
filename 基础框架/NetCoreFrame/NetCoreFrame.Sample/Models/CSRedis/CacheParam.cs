using Abp.Auditing;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Extensions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Sample
{
    public class CacheParam
    {
        public string KeyName { get; set; }
        public string Message { get; set; }

    }


}