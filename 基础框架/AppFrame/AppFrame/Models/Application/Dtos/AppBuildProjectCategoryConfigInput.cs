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

    [AutoMap(typeof(AppBuildProjectCategoryConfig))]
    public class AppBuildProjectCategoryConfigInput
    {
        public AppBuildProjectCategoryConfigInput()
        {
            IsActive = true;
        }

        /// <summary>
        /// ID
        /// </summary>	
        public Guid? Id { get; set; }

        /// <summary>
        /// 类别编码
        /// </summary>
        [Required(ErrorMessage = "请输入类别编码")]
        [StringLength(50)]
        public virtual string CategoryCode { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        [Required(ErrorMessage = "请输入类别名称")]
        [StringLength(50)] 
        public virtual string CategoryName { get; set; }

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
        /// 单位
        /// </summary>	
        [Required(ErrorMessage = "请输入单位")]
        [StringLength(50)]
        public virtual string UnitCode { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>		
        [Required]
        public virtual bool? IsActive { get; set; } = true;


    }
}
