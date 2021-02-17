using Abp.AutoMapper;
using NetCoreFrame.Core;
using System;

namespace NetCoreFrame.Application
{
    [AutoMap(typeof(SysDict))]
    public class SysDictInput
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
        /// 字典内容
        /// </summary>	 
        public string DictContent { get; set; }

        /// <summary>
        /// 字典编码
        /// </summary>	 
        public string DictCode { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>	 
        public string DictValue { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>	 
        public bool IsActive { get; set; }

        /// <summary>
        /// 行数据对象状态
        ///  ADD  = 新增
        ///  MODIFY = 修改
        /// </summary>
        public string EditState { get; set; } = "";
    }
}
