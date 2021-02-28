using Abp.Domain.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Core
{
    [Table("Sys_Dict")]
    public class SysDict : Entity<Guid>
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
        /// 字典内容
        /// </summary>	 
        [Column("DictContent")]
        [Description("字典内容")]
        [Required(ErrorMessage = "请输入字典内容")]
        [StringLength(50)]
        public string DictContent { get; set; }

        /// <summary>
        /// 字典编码
        /// </summary>	 
        [Column("DictCode")]
        [Description("字典编码")]
        [Required(ErrorMessage = "请输入字典编码")]
        [StringLength(50)]
        public string DictCode { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>	 
        [Column("DictValue")]
        [Description("字典值")]
        [StringLength(50)]
        public string DictValue { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>	 
        [Column("IsActive")]
        [Description("是否启用 0=否 1=是")]
        [Required]
        public bool? IsActive { get; set; } = true;


    }
}
