using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppFrame.Core
{ 
  /// <summary>
  /// 基础指标配置
  /// </summary>
    [Table("App_Commodity")]
    public class AppCommodity : AuditedEntity<Guid> 
    {
        public AppCommodity()
        {
            IsActive = true;
        }

        /// <summary>
        /// 商品
        /// </summary>
        [Column("Commodity")]
        [Description("商品")]
        [Required(ErrorMessage = "请输入商品")]
        [StringLength(100)] 
        public virtual string Commodity { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [Column("Brand")]
        [Description("品牌")]
        [Required(ErrorMessage = "请输入品牌")]
        [StringLength(100)] 
        public virtual string Brand { get; set; }

        /// <summary>
        /// 类别编码
        /// </summary>
        [Column("CategoryCode")]
        [Description("类别编码")]
        [Required(ErrorMessage = "请输入类别编码名称")]
        [StringLength(50)]
        public virtual string CategoryCode { get; set; }

        /// <summary>
        /// 单位
        /// </summary>	
        [Column("UnitCode")]
        [Description("单位")]
        [Required(ErrorMessage = "请输入单位")]
        [StringLength(50)]
        public virtual string UnitCode { get; set; }

        /// <summary>
        /// 价格
        /// </summary>	
        [Column("Price")]
        [Description("价格")]
        public virtual decimal? Price { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>		
        [Column("IsActive")]
        [Description("是否启用")]
        [Required]
        public virtual bool IsActive { get; set; } = true;

        /// <summary>
        /// 类别
        /// </summary>
        [Column("Description")]
        [Description("备注")]
        [StringLength(2000)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 默认商品图片
        /// </summary>
        [Column("FileId")]
        [Description("默认商品图片")]
        public virtual Guid? FileId { get; set; }

    }
}
