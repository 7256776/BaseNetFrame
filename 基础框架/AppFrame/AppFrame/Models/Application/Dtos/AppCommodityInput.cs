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
    public class AppCommodityInput
    {
        public AppCommodityInput()
        {
            IsActive = true;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        public virtual Guid? Id { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        [Required(ErrorMessage = "请输入商品")]
        [StringLength(100)]
        public virtual string Commodity { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [Required(ErrorMessage = "请输入品牌")]
        [StringLength(100)]
        public virtual string Brand { get; set; }

        /// <summary>
        /// 类别编码
        /// </summary>
        [Required(ErrorMessage = "请输入类别编码名称")]
        [StringLength(50)]
        public virtual string CategoryCode { get; set; }

        /// <summary>
        /// 单位
        /// </summary>	
        [Required(ErrorMessage = "请输入单位")]
        [StringLength(50)]
        public virtual string UnitCode { get; set; }

        /// <summary>
        /// 价格
        /// </summary>	
        [Required(ErrorMessage = "请输入价格")]
        public virtual decimal? Price { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>		
        [Required]
        public virtual bool IsActive { get; set; } = true;

        /// <summary>
        /// 类别
        /// </summary>
        [StringLength(2000)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 上传文件列表
        /// </summary>
        public virtual List<string> FileIds { get; set; }

        /// <summary>
        /// 文件Id
        /// </summary>
        public virtual Guid? FileId { get; set; }
    }
}
