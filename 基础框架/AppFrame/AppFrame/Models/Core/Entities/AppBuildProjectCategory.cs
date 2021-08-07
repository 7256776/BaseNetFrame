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
    [Table("App_BuildProjectCategory")]
    public class AppBuildProjectCategory : AuditedEntity<Guid> 
    {
        public AppBuildProjectCategory()
        {
            IsActive = true;
        }

        [Column("SolutionId")]
        [Description("方案Id")]
        [Required(ErrorMessage = "请输入方案")]
        [StringLength(50)]
        public virtual Guid SolutionId { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        [Column("CategoryName")]
        [Description("类别")]
        [Required(ErrorMessage = "请输入类别名称")]
        [StringLength(50)] 
        public virtual string CategoryName { get; set; }

        /// <summary>
        /// 类别编码
        /// </summary>
        [Column("CategoryCode")]
        [Description("类别编码")]
        [Required(ErrorMessage = "请输入类别编码名称")]
        [StringLength(50)] 
        public virtual string CategoryCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>		
        [Column("ItemName")]
        [Description("项目名称")]
        [Required(ErrorMessage = "请输入项目名称")]
        [StringLength(50)]
        public virtual string ItemName { get; set; }

        /// <summary>
        /// 分类/空间
        /// </summary>	
        [Column("ClassifyName")]
        [Description("分类/空间")]
        [Required(ErrorMessage = "请输入分类/空间")]
        [StringLength(50)]
        public virtual string ClassifyName { get; set; }

        /// <summary>
        /// 单位
        /// </summary>	
        [Column("UnitCode")]
        [Description("单位")]
        [Required(ErrorMessage = "请输入单位")]
        [StringLength(50)]
        public virtual string UnitCode { get; set; }

        /// <summary>
        /// 数量
        /// </summary>	
        [Column("Amount")]
        [Description("数量")]
        public virtual decimal? Amount { get; set; }

        /// <summary>
        /// 单价
        /// </summary>	
        [Column("Price")]
        [Description("单价")]
        public virtual decimal? Price { get; set; }

        /// <summary>
        /// 预估金额
        /// </summary>	
        [Column("BudgetPrice")]
        [Description("数量")]
        public virtual decimal? BudgetPrice { get; set; }

        /// <summary>
        /// 实际金额
        /// </summary>	
        [Column("ActualPrice")]
        [Description("实际金额")]
        public virtual decimal? ActualPrice { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>		
        [Column("IsActive")]
        [Description("是否启用")]
        [Required]
        public virtual bool? IsActive { get; set; } = true;

        /// <summary>
        /// 类别
        /// </summary>
        [Column("Description")]
        [Description("备注")]
        [StringLength(2000)]
        public virtual string Description { get; set; }

    }
}
