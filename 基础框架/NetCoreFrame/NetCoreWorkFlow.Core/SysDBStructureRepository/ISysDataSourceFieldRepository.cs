using Abp.Dapper.Repositories;
using System;
using System.Collections.Generic;

namespace NetCoreWorkFlow.Core
{
    public interface ISysDataSourceFieldRepository : IDapperRepository<SysDataSourceField, Guid?>
    {
        /// <summary>
        /// 获取表结构对象
        /// </summary>
        /// <param name="tabName"></param>
        /// <returns></returns>
        List<SysDataSourceField> GetDataStructure(string tabName);

        /// <summary>
        /// 数据库类型与基础类型转换
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        (string, string) AdaptiveDataType(string dataType);

    }
}
