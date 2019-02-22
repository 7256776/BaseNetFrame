using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// Represents a setting for a tenant or user.
    /// </summary>
    [AutoMap(typeof(SysSetting))]
    public class SysSettingDto
    {
        /// <summary>
        /// 设置的名称.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 设置的值
        /// </summary>
        public virtual string Value { get; set; }

      
    }
}
