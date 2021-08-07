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
    [Table("App_BuildProjectCategoryToCommodity")]
    public class AppBuildProjectCategoryToCommodity : Entity<Guid> 
    {
        public AppBuildProjectCategoryToCommodity()
        {
        }

        /// <summary>
        /// 类别
        /// </summary>
        [Column("BuildProjectCategoryId")]
        [Description("项目指标ID")]
        [Required(ErrorMessage = "项目指标ID")]
        [StringLength(50)] 
        public virtual Guid BuildProjectCategoryId { get; set; }

        /// <summary>
        /// 类别编码
        /// </summary>
        [Column("CommodityId")]
        [Description("商品ID")]
        [Required(ErrorMessage = "关联商品ID")]
        public virtual Guid CommodityId { get; set; }


    }
}
