using Abp.Dapper.Repositories;
using Abp.Data;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreWorkFlow.Core
{
    /// <summary>
    /// DapperEfRepositoryBase
    /// </summary>
    public class SysDataSourceFieldRepository : DapperEfRepositoryBase<NetCoreWorkFlowDbContext, SysDataSourceField, Guid?>, ISysDataSourceFieldRepository
    {
        private readonly IActiveTransactionProvider _activeTransactionProvider;
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysDataSourceFieldRepository(
            IDbContextProvider<NetCoreWorkFlowDbContext> dbcontext,
            IActiveTransactionProvider activeTransactionProvider,
            ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
            : base(activeTransactionProvider, currentUnitOfWorkProvider)
        {
            _activeTransactionProvider = activeTransactionProvider;
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }

        /// <summary>
        /// 获取表结构对象
        /// </summary>
        /// <param name="tabName"></param>
        /// <returns></returns>
        public List<SysDataSourceField> GetDataStructure(string tabName)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT TABLE_CATALOG TableCatalog, ");
            sqlStr.Append("         TABLE_SCHEMA TableSchema, ");
            sqlStr.Append("         TABLE_NAME TableName, ");
            sqlStr.Append("         COLUMN_NAME ColumnName, ");
            sqlStr.Append("         DATA_TYPE DataType, ");
            sqlStr.Append("         CHARACTER_MAXIMUM_LENGTH DataLength, ");
            sqlStr.Append("         NUMERIC_PRECISION NumericPrecision, ");
            sqlStr.Append("         NUMERIC_SCALE NumericScale ");
            sqlStr.Append("  FROM   INFORMATION_SCHEMA.columns ");
            sqlStr.Append("  WHERE  TABLE_NAME = @TableName");

            var args = new DynamicParameters(new { });
            args.Add("TableName", tabName);

            var data = base.Query(sqlStr.ToString(), args);
            return data.AsList();
        }

        /// <summary>
        /// 数据库类型与基础类型转换
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public (string, string) AdaptiveDataType(string dataType)
        {
            switch (dataType.ToUpper())
            {
                case "BIGINT":
                case "INT":
                    return ("Int", "整数");
                case "BIT":
                    return ("Bool", "布尔");
                case "DATETIME":
                case "DATETIME2":
                    return ("DateTime", "日期");
                case "FLOAT":
                case "NUMERIC":
                    return ("Double", "小数");
                case "NCHAR":
                case "NVARCHAR":
                case "VARCHAR":
                    return ("String", "字符串");
                case "UNIQUEIDENTIFIER":
                    return ("Guid", "唯一标识");
                default:
                    return ("String", "字符串");
            }
        }



    }
}
