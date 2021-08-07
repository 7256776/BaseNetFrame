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
    public class AppSolutionInput
    {
        public AppSolutionInput()
        {
            IsActive = true;
        }

        /// <summary>
        /// ID
        /// </summary>	
        public Guid? Id { get; set; }

        /// <summary>
        /// 图片ID
        /// </summary>	
        public Guid? FileId { get; set; }

        /// <summary>
        /// 方案名称
        /// </summary>
        [Required(ErrorMessage = "请输入方案")]
        [StringLength(50)]
        public virtual string SolutionName { get; set; }

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


    }
}
