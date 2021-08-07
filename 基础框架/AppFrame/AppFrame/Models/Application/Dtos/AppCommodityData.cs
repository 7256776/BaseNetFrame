using Abp.AutoMapper;
using AppFrame.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppFrame.Application
{
    /// <summary>
    /// 上传文件管理
    /// </summary>
    [AutoMap(typeof(AppCommodity))]
    public class AppCommodityData
    {
        public AppCommodityData()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        public virtual Guid? Id { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public virtual string Commodity { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public virtual string Brand { get; set; }

        /// <summary>
        /// 类别编码
        /// </summary>
        public virtual string CategoryCode { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public virtual string CategoryName { get; set; }

        /// <summary>
        /// 单位
        /// </summary>	
        public virtual string UnitCode { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>	
        public virtual string UnitName { get; set; }

        /// <summary>
        /// 价格
        /// </summary>	
        public virtual decimal? Price { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>		
        public virtual bool? IsActive { get; set; } 

        /// <summary>
        /// 类别
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
        /// 缩略图地址
        /// </summary>
        public virtual string FilePathThumbnail { get; set; }

        /// <summary>
        /// 文件Id
        /// </summary>
        public virtual Guid? FileId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual List<AppFile> AppFiles { get; set; }

    }
}
