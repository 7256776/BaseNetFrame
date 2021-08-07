using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebIndividual
{
    /// <summary>
    /// 上传文件管理
    /// </summary>
    [AutoMap(typeof(WebFile))]
    public class WebFileInput
    {
        public WebFileInput()
        {
            IsActive = true;
        }

        public virtual Guid? Id { get; set; }

        /// <summary>
        /// 文件别名
        /// </summary>
        [Required(ErrorMessage = "请设置文件别名")]
        [StringLength(100)]
        public virtual string FileAlias { get; set; }

        /// <summary>
        /// 文件原图路径
        /// </summary>
        [Required(ErrorMessage = "请设置文件原图路径")]
        [StringLength(255)]
        public virtual string FilePathOriginal { get; set; }

        /// <summary>
        /// 文件用于预览路径
        /// </summary>
        [Required(ErrorMessage = "请设置文件用于预览路径")]
        [StringLength(255)]
        public virtual string FilePathPreview { get; set; }

        /// <summary>
        /// 文件缩略图路径
        /// </summary>
        [Required(ErrorMessage = "请设置文件缩略图路径")]
        [StringLength(255)]
        public virtual string FilePathThumbnail { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>		
        [Required(ErrorMessage = "请设置文件名")]
        [StringLength(100)]
        public virtual string FileName { get; set; }

        /// <summary>
        /// 文件名后缀
        /// </summary>		
        [Required(ErrorMessage = "请设置文件名后缀")]
        [StringLength(20)]
        public virtual string FileSuffix { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>		
        [Required(ErrorMessage = "请设置文件大小")]
        public virtual long FileSize { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>		
        [Required]
        public virtual bool IsActive { get; set; } = true;

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(2000)]
        public virtual string Description { get; set; }

    }
}
