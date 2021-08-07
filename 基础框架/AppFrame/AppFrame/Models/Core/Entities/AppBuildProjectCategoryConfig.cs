using Abp.Domain.Entities;
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
    [Table("App_BuildProjectCategoryConfig")]
    public class AppBuildProjectCategoryConfig : Entity<Guid> 
    {
        public AppBuildProjectCategoryConfig()
        {
            IsActive = true;
        }

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
        [Required(ErrorMessage = "请输入类别编码")]
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
        /// 扩展数据
        /// </summary>	
        [Column("UnitCode")]
        [Description("单位")]
        [Required(ErrorMessage = "请输入单位")]
        [StringLength(50)]
        public virtual string UnitCode { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>		
        [Column("IsActive")]
        [Description("是否启用")]
        [Required]
        public virtual bool IsActive { get; set; } = true;


    }
}
