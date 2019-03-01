using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAuthService
{
    [Table("SYS_DICT")]
    public class SysDict 
    {

        /// <summary>
        /// </summary>
        [Key]
        [Column("ID")]
        public virtual Guid? Id { get; set; }

        /// <summary>
        /// 字典类型(通常是字母编码)
        /// </summary>	 
        [Column("DICTTYPE")]
        [StringLength(50)]
        public string DictType { get; set; }

        /// <summary>
        /// 字典内容
        /// </summary>	 
        [Column("DICTCONTENT")]
        [StringLength(50)]
        public string DictContent { get; set; }

        /// <summary>
        /// 字典编码
        /// </summary>	 
        [Column("DICTCODE")]
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
        public bool IsActive { get; set; } = true;


    }
}
