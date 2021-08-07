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
    public class AppBuildProjectCategoryData
    {
        public AppBuildProjectCategoryData()
        {
            IsActive = true;
        }

        /// <summary>
        /// ID
        /// </summary>	
        public Guid? Id { get; set; }

        /// <summary>
        /// //
        /// </summary>
        public virtual Guid SolutionId { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public virtual string CategoryCode { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public virtual string CategoryName { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>		
        public virtual string ItemName { get; set; }

        /// <summary>
        /// 分类/空间
        /// </summary>	
        public virtual string ClassifyName { get; set; }

        /// <summary>
        /// 单位编码
        /// </summary>	
        public virtual string UnitCode { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>	
        public virtual string UnitName { get; set; }

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
        public virtual bool? IsActive { get; set; } = true;

        /// <summary>
        /// 描述
        /// </summary> 
        public virtual string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual long? CreatorUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual long? LastModifierUserId { get; set; }

        /// <summary>
        /// 方案各个指标关联的商品
        /// </summary>
        public virtual List<AppBuildProjectCategoryToCommodityData> AppBuildProjectCategoryToCommodityList { get; set; }

    }



}
