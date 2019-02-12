using Abp.AutoMapper;
using System;
using System.Collections.Generic;

namespace Frame.Core
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
        public string DictType { get; set; }

        /// <summary>
        /// 字典类型名称
        /// </summary>
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
