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
    /// 方案
    /// </summary>

    public class AppSolutionGroupData
    {
        public AppSolutionGroupData()
        {
        }

        /// <summary>
        /// ID
        /// </summary>	
        public AppSolution AppSolutionData { get; set; }


        /// <summary>
        /// 文件原图路径
        /// </summary>
        public virtual string FilePathOriginal { get; set; }

        /// <summary>
        /// 文件用于预览路径
        /// </summary>
        public virtual string FilePathPreview { get; set; }

        /// <summary>
        /// 文件缩略图路径
        /// </summary>
        public virtual string FilePathThumbnail { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string UserNameCn { get; set; }

        /// <summary>
        /// 数量
        /// </summary>	
        public virtual decimal? Amount { get; set; }

        /// <summary>
        /// 预估金额
        /// </summary>	
        public virtual decimal? BudgetPrice { get; set; }

        /// <summary>
        /// 实际金额
        /// </summary>	
        public virtual decimal? ActualPrice { get; set; }


    }
}
