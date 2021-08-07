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
    public class AppBuildProjectCategoryConfigData
    {
        public AppBuildProjectCategoryConfigData()
        {
        }


        /// <summary>
        /// ID
        /// </summary>	
        public Guid? Id { get; set; }

        /// <summary>
        /// 类别编码
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
        /// 单位
        /// </summary>	
        public virtual string UnitCode { get; set; }

        /// <summary>
        /// 单位
        /// </summary>	
        public virtual string UnitName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>		
        public virtual bool? IsActive { get; set; } = true;



    }
}
