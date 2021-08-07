using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebIndividual
{ 
  /// <summary>
  /// 基础指标配置
  /// </summary>
    [Table("Web_FileToBusiness")]
    public class WebFileToBusiness : Entity<Guid> 
    {
        public WebFileToBusiness()
        {
        }

        /// <summary>
        /// 商品
        /// </summary>
        [Column("FileId")]
        [Description("文件ID")]
        [Required(ErrorMessage = "请设置文件ID")]
        public virtual Guid FileId { get; set; }

        /// <summary>
        /// 业务主键ID
        /// </summary>
        [Column("BusinessId")]
        [Description("业务主键ID")]
        [Required(ErrorMessage = "请设置业务主键ID")]
        public virtual Guid BusinessId { get; set; }


    }
}
