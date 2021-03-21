using Abp.Domain.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreWorkFlow.Core
{
    public class SysDataSourceField: Entity<Guid?>
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        [Description("数据库名称")]
        public string TableCatalog { get; set; }

        /// <summary>
        /// 表对象所有者
        /// </summary>
        [Description("表对象所有者")]
        public string TableSchema { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        [Description("表名称")]
        public string TableName { get; set; }

        /// <summary>
        /// 列名称
        /// </summary>
        [Description("列名称")]
        public string ColumnName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [Description("数据类型")]
        public string DataType { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        [Description("数据长度")]
        public string DataLength { get; set; }

        /// <summary>
        /// 数值精度
        /// </summary>
        [Description("数值精度")]
        public string NumericPrecision { get; set; }

        /// <summary>
        /// 数据刻度
        /// </summary>
        [Description("数据刻度")]
        public string NumericScale { get; set; }


    }
}
