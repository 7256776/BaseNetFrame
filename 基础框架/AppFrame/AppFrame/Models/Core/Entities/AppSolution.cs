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
    [Table("App_Solution")]
    public class AppSolution : AuditedEntity<Guid> 
    {
        public AppSolution()
        {
            IsActive = true;
        }

        /// <summary>
        /// 方案名称
        /// </summary>
        [Column("FileId")]
        [Description("方案封面图片url")]
        public virtual Guid? FileId { get; set; }

        /// <summary>
        /// 方案名称
        /// </summary>
        [Column("SolutionName")]
        [Description("方案名称")]
        [Required(ErrorMessage = "请输入方案名称")]
        [StringLength(50)] 
        public virtual string SolutionName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>		
        [Column("IsActive")]
        [Description("是否启用")]
        [Required]
        public virtual bool IsActive { get; set; } = true;

        /// <summary>
        /// 类别
        /// </summary>
        [Column("Description")]
        [Description("备注")]
        [StringLength(2000)]
        public virtual string Description { get; set; }

    }
}
