using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebIndividual
{
    /// <summary>
    /// 上传文件管理
    /// </summary>
    public class WebDocPageDto
    {
        public WebDocPageDto()
        {
        }

        public virtual Guid Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public virtual string DocTitle { get; set; }
        
        /// <summary>
        /// 副标题
        /// </summary>
        public virtual string DocSubhead { get; set; }

        /// <summary>
        /// 文件正文
        /// </summary>
        public virtual string DocContent { get; set; }

        /// <summary>
        /// 文件Id
        /// </summary>
        public virtual Guid FileId { get; set; }

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


    }
}
