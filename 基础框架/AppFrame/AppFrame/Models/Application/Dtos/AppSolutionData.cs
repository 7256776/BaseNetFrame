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

    [AutoMap(typeof(AppSolution))]
    public class AppSolutionData
    {
        public AppSolutionData()
        {
            IsActive = true;
        }

        /// <summary>
        /// ID
        /// </summary>	
        public Guid? Id { get; set; }

        public virtual string SolutionName { get; set; }

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
        /// 
        /// </summary>
        public virtual List<AppBuildProjectCategoryData> AppBuildProjectCategoryList { get; set; }

        /// <summary>
        /// 方案图片文件id
        /// </summary>
        public virtual Guid? FileId { get; set; }

        /// <summary>
        /// 方案预览图片
        /// </summary>
        public virtual string FilePathPreview { get; set; }

    }
}
