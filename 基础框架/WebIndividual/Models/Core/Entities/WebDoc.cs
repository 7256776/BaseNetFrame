using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebIndividual
{
    /// <summary>
    /// 上传文件管理
    /// </summary>
    [Table("Web_Doc")]
    public class WebDoc : FullAuditedEntity<Guid>
    {
        public WebDoc()
        {
            IsActive = true;
        }


        /// <summary>
        /// 标题
        /// </summary>
        [Column("DocTitle")]
        [Description("标题")]
        [Required(ErrorMessage = "请设置标题")]
        [StringLength(100)]
        public virtual string DocTitle { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Column("DocSubhead")]
        [Description("副标题")]
        [Required(ErrorMessage = "请设置副标题")]
        [StringLength(500)]
        public virtual string DocSubhead { get; set; }

        /// <summary>
        /// 文件正文
        /// </summary>
        [Column("DocContent")]
        [Description("文件正文")]
        [Required(ErrorMessage = "请设置文件正文")]
        public virtual string DocContent { get; set; }
          
        /// <summary>
        /// 是否启用
        /// </summary>		
        [Column("IsActive")]
        [Description("是否启用")]
        [Required]
        public virtual bool IsActive { get; set; } = true;
         

    }
}
