using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frame.Core
{
    [Table("SYS_DICT")]
    public class SysDict : Entity<Guid>
    { 
        /// <summary>
        /// 字典类型(通常是字母编码)
        /// </summary>	 
        [Column("DICTTYPE")]
        [Required]
        [StringLength(50)]
        public string DictType { get; set; }

        /// <summary>
        /// 字典内容
        /// </summary>	 
        [Column("DICTCONTENT")]
        [Required]
        [StringLength(50)]
        public string DictContent { get; set; }

        /// <summary>
        /// 字典编码
        /// </summary>	 
        [Column("DICTCODE")]
        [Required]
        [StringLength(50)]
        public string DictCode { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>	 
        [Column("DICTVALUE")]
        [StringLength(50)]
        public string DictValue { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>	 
        [Column("ISACTIVE")]
        [Required]
        public bool IsActive { get; set; }


    }
}
