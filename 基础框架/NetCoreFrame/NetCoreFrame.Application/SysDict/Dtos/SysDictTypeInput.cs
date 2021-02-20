using Abp.AutoMapper;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCoreFrame.Application
{
    [AutoMap(typeof(SysDictType))]
    public class SysDictTypeInput
    {
        /// <summary>
        /// ID
        /// </summary>	
        public Guid? Id { get; set; }

        /// <summary>
        /// 字典类型(通常是字母编码)
        /// </summary>	 
        [Required(ErrorMessage = "请输入字典类型")]
        [StringLength(50, ErrorMessage = "字典类型长度超过50")]
        public string DictType { get; set; }

        /// <summary>
        /// 字典类型名称
        /// </summary>
        [Required(ErrorMessage = "请输入字典类型名称")]
        [StringLength(50, ErrorMessage = "字典类型名称长度超过50")]
        public string DictTypeName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 字典编码列表
        /// </summary>
        public List<Application.SysDictInput> SysDictInputList { get; set; }
    }
}
