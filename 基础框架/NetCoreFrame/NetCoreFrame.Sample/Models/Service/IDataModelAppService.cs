using Abp.Application.Services;
using Abp.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.Sample
{
    public interface IDataModelAppService : IApplicationService
    {
        #region 基础设置
        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <returns></returns>
        List<TableInfo> GetTableList(string tabType);

        /// <summary>
        /// 获取表与字段信息
        /// </summary>
        /// <returns></returns>
        List<TableInfo> GetTableAndeFieldList();

        /// <summary>
        /// 或字段对象
        /// 根据字段id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FieldInfo GetFieldInfoModel(Guid id);

        /// <summary>
        /// 获取字段对象
        /// 根据表id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<FieldInfo> GetFieldInfoByTableModel(Guid id);

        /// <summary>
        /// 保存字段信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<AjaxResponse> SaveMetaModel(FieldInfoDto model);

        #endregion

        #region EntityFramework
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<AjaxResponse> SaveFormModel(TempTableDto model);

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        List<TempTable> GetTempTableDto();

        #endregion

        #region Dapper
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<AjaxResponse> SaveFormDapperModel(TempTableDto model);

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        List<TempTable> GetTempTableDapperDto();

        #endregion

        #region Sql
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        AjaxResponse SaveFormSqlModel(dynamic model);

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        List<dynamic> GetTempTableSqlDto(string tableName);

        #endregion
    }
}
