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
    [AutoMap(typeof(WebDoc))]
    public class WebDocDto
    {
        public WebDocDto()
        {
            IsActive = true;
        }


        public virtual Guid? Id { get; set; }

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
        /// 是否启用
        /// </summary>		
        public virtual bool IsActive { get; set; } = true;

        /// <summary>
        /// 是否启用
        /// </summary>		
        public virtual string[] FileItem { get; set; } = new string[] { };
         
        /// <summary>
        /// 文件名称
        /// </summary>
        public virtual string FileName { get; set; }

    }
}
