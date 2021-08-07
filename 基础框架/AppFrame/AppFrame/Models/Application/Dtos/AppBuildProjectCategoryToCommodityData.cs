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
    /// 基础指标关联商品
    /// </summary>
    [AutoMap(typeof(AppBuildProjectCategoryToCommodity))]
    public class AppBuildProjectCategoryToCommodityData
    {
        public Guid BuildProjectCategoryId { get; set; }
        public Guid CommodityId { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string Commodity { get; set; }
        public string Brand { get; set; }
        public string UnitCode { get; set; }
        public decimal? Price { get; set; }

    }

}
