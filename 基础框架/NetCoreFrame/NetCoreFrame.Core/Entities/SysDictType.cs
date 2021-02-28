using Abp.Domain.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    //Sys_DictType
    [Table("Sys_DictType")]
    public class SysDictType: Entity<Guid>
    { 
        /// <summary>
        /// 字典类型(通常是字母编码)
        /// </summary>	 
        [Column("DictType")]
        [Description("字典类型(通常是字母编码)")]
        [Required(ErrorMessage = "请输入字典类型")]
        [StringLength(50)]
        public string DictType { get; set; }
         
        /// <summary>
        /// 字典类型名称
        /// </summary>	 
        [Column("DictTypeName")]
        [Description("字典类型名称")]
        [Required(ErrorMessage = "请输入字典类型名称")]
        [StringLength(50)]
        public string DictTypeName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>	 
        [Column("IsActive")]
        [Required]
        public bool IsActive { get; set; } = true;


    }
}
