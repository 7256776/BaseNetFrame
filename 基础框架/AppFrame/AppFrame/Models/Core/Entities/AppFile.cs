using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppFrame.Core
{
    /// <summary>
    /// 上传文件管理
    /// </summary>
    [Table("App_File")]
    public class AppFile : FullAuditedEntity<Guid>
    {
        public AppFile()
        {
            IsActive = true;
        }

        /// <summary>
        /// 文件别名
        /// </summary>
        [Column("FileAlias")]
        [Description("文件别名")]
        [Required(ErrorMessage = "请设置文件别名")]
        [StringLength(100)]
        public virtual string FileAlias { get; set; }

        /// <summary>
        /// 文件原图路径
        /// </summary>
        [Column("FilePathOriginal")]
        [Description("文件相对路径")]
        [Required(ErrorMessage = "请设置文件原图路径")]
        [StringLength(255)]
        public virtual string FilePathOriginal { get; set; }

        /// <summary>
        /// 文件用于预览路径
        /// </summary>
        [Column("FilePathPreview")]
        [Description("文件用于预览路径")]
        [Required(ErrorMessage = "请设置文件用于预览路径")]
        [StringLength(255)]
        public virtual string FilePathPreview { get; set; }

        /// <summary>
        /// 文件缩略图路径
        /// </summary>
        [Column("FilePathThumbnail")]
        [Description("文件缩略图路径")]
        [Required(ErrorMessage = "请设置文件缩略图路径")]
        [StringLength(255)]
        public virtual string FilePathThumbnail { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>		
        [Column("FileName")]
        [Description("文件名")]
        [Required(ErrorMessage = "请设置文件名")]
        [StringLength(100)]
        public virtual string FileName { get; set; }

        /// <summary>
        /// 文件名后缀
        /// </summary>		
        [Column("FileSuffix")]
        [Description("文件名后缀")]
        [Required(ErrorMessage = "请设置文件名后缀")]
        [StringLength(20)]
        public virtual string FileSuffix { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>		
        [Column("FileSize")]
        [Description("文件大小")]
        [Required(ErrorMessage = "请设置文件大小")]
        public virtual long FileSize { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>		
        [Column("IsActive")]
        [Description("是否启用")]
        [Required]
        public virtual bool IsActive { get; set; } = true;

        /// <summary>
        /// 备注
        /// </summary>
        [Column("Description")]
        [Description("备注")]
        [StringLength(2000)]
        public virtual string Description { get; set; }

    }
}
