using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using AppFrame.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppFrame.Application
{
    /// <summary>
    /// 基础指标配置
    /// </summary>

    [AutoMap(typeof(AppBuildProjectCategory))]
    public class AppBuildProjectCategoryInput
    {
        public AppBuildProjectCategoryInput()
        {
            IsActive = true;
        }

        /// <summary>
        /// ID
        /// </summary>	
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "请输入方案")]
        public virtual Guid SolutionId { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        [Required(ErrorMessage = "请输入类别名称")]
        [StringLength(50)] 
        public virtual string CategoryName { get; set; }

        /// <summary>
        /// 类别编码
        /// </summary>
        [Required(ErrorMessage = "请输入类别名称")]
        [StringLength(50)]
        public virtual string CategoryCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>		
        [Required(ErrorMessage = "请输入项目名称")]
        [StringLength(50)]
        public virtual string ItemName { get; set; }

        /// <summary>
        /// 分类/空间
        /// </summary>	
        [Required(ErrorMessage = "请输入分类/空间")]
        [StringLength(50)]
        public virtual string ClassifyName { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>	
        [Required(ErrorMessage = "请输入单位")]
        [StringLength(50)]
        public virtual string UnitCode { get; set; }

        /// <summary>
        /// 数量
        /// </summary>	
        public virtual decimal? Amount { get; set; }

        /// <summary>
        /// 单价
        /// </summary>	
        public virtual decimal? Price { get; set; }

        /// <summary>
        /// 预估金额
        /// </summary>	
        public virtual decimal? BudgetPrice { get; set; }

        /// <summary>
        /// 实际金额
        /// </summary>	
        public virtual decimal? ActualPrice { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>		
        [Required]
        public virtual bool? IsActive { get; set; } = true;

        /// <summary>
        /// 描述
        /// </summary> 
        [StringLength(2000)]
        public virtual string Description { get; set; }
        
        /// <summary>
        /// 方案各个指标关联的商品
        /// </summary>
        public virtual List<string> commodityList { get; set; }


    }
}
