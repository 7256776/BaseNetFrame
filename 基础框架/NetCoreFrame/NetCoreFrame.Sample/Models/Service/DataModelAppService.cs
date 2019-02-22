using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dapper.Repositories;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Web.Models;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreFrame.Sample
{
    [Audited]
    public class DataModelAppService : NetCoreFrameApplicationBase, IDataModelAppService
    {
        private readonly IRepository<FieldInfo, Guid> _repositoryFieldInfo;
        private readonly IRepository<TableInfo, Guid> _repositoryTableInfo;
        private readonly IRepository<TempTable, long> _repositoryTempTable;

        private readonly IDapperRepository<TempTable, long> _dapperRepositoryTableInfo; 
        private readonly IUnitOfWorkManager _unitOfWorkManager;



        public DataModelAppService(
                IRepository<TempTable, long> repositoryTempTable,
                IRepository<FieldInfo, Guid> repositoryFieldInfo,
                IRepository<TableInfo, Guid> repositoryTableInfo,
                IDapperRepository<TempTable, long> dapperRepositoryTableInfo,
                IUnitOfWorkManager unitOfWorkManager)
        {
            //注入Ef仓储
            _repositoryTempTable = repositoryTempTable;
            _repositoryTableInfo = repositoryTableInfo;
            _repositoryFieldInfo = repositoryFieldInfo;
            //注入Dapper 仓储
            _dapperRepositoryTableInfo = dapperRepositoryTableInfo;

            _unitOfWorkManager = unitOfWorkManager;

        }

        #region 基础设置
        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <returns></returns>
        public List<TableInfo> GetTableList(string tabType)
        { 
            var data = _repositoryTableInfo.GetAll().Where(w=>w.TableType == tabType || tabType=="");
            return data.ToList();
        }

        /// <summary>
        /// 获取表与字段信息
        /// </summary>
        /// <returns></returns>
        public List<TableInfo> GetTableAndeFieldList()
        {
            var data = _repositoryTableInfo.GetAllIncluding(x => x.FieldInfoList);

            return data.ToList();
        }

        /// <summary>
        /// 或字段对象
        /// 根据字段id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FieldInfo GetFieldInfoModel(Guid id)
        {
            var data = _repositoryFieldInfo.Get(id);
            return data;
        }

        /// <summary>
        /// 获取字段对象集合
        /// 根据表id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<FieldInfo> GetFieldInfoByTableModel(Guid id)
        {
            var data = _repositoryFieldInfo.GetAllList(w => w.TableId == id).OrderBy(o => o.FieldOrder);
            return data.ToList();
        }

        /// <summary>
        /// 保存字段信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AjaxResponse> SaveMetaModel(FieldInfoDto model)
        {
            if (model.Id == null)
            {
                model.Id = await _repositoryFieldInfo.InsertAndGetIdAsync(model.MapTo<FieldInfo>());
            }
            else
            {
                //获取需要更新的数据
                var data = _repositoryFieldInfo.Get(model.Id.Value);
                //映射需要修改的数据对象
                var m = ObjectMapper.Map(model, data);
                //提交修改
                await _repositoryFieldInfo.UpdateAsync(m);
            }
            return new AjaxResponse { Success = true, Result = model.Id };
        }

        #endregion

        #region EntityFramework

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AjaxResponse> SaveFormModel(TempTableDto model)
        {
            if (model.Id == null)
            {
                model.Id = await _repositoryTempTable.InsertAndGetIdAsync(model.MapTo<TempTable>());
            }
            else
            {
                //获取需要更新的数据
                var data = _repositoryTempTable.Get(model.Id.Value);
                //映射需要修改的数据对象
                var m = ObjectMapper.Map(model, data);
                //提交修改
                await _repositoryTempTable.UpdateAsync(m);
            }
            return new AjaxResponse { Success = true, Result = model.Id };
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<TempTable> GetTempTableDto()
        {
            return _repositoryTempTable.GetAllList();
        }

        #endregion

        #region Dapper
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AjaxResponse> SaveFormDapperModel(TempTableDto model)
        {
            if (model.Id == null)
            {
                model.Id = await _dapperRepositoryTableInfo.InsertAndGetIdAsync(model.MapTo<TempTable>());
            }
            else
            {
                //获取需要更新的数据
                var data = _dapperRepositoryTableInfo.Get(model.Id.Value);
                //映射需要修改的数据对象
                var m = ObjectMapper.Map(model, data);
                //提交修改
                await _dapperRepositoryTableInfo.UpdateAsync(m);
            }
            return new AjaxResponse { Success = true, Result = model.Id };
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<TempTable> GetTempTableDapperDto()
        {
            return _dapperRepositoryTableInfo.GetAll().ToList();
        }

        #endregion

        #region Sql
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AjaxResponse SaveFormSqlModel(dynamic model)
        {
            string sqlStr = BuildSql(model);

            var row = _dapperRepositoryTableInfo.Execute(sqlStr);

            return new AjaxResponse { Success = true };
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<dynamic> GetTempTableSqlDto(string tableName)
        {
            var result = _dapperRepositoryTableInfo.Query<dynamic>("SELECT* FROM " + tableName);

            return result.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string BuildSql(dynamic model)
        {
            string jsonTem = JsonConvert.SerializeObject(model);
            FormParam formParam = JsonConvert.DeserializeObject<FormParam>(jsonTem);

            IDictionary<string, object> dict = formParam.Fields;

            StringBuilder sql = new StringBuilder();

            bool isAdd = true;

            object id = null;
            if (dict.TryGetValue("id", out id))
            {
                isAdd = id == null ? true : false;
            }
            else
            {
                isAdd = true;
            }


            StringBuilder sqlValue = new StringBuilder();
            StringBuilder sqlField = new StringBuilder();
            StringBuilder sqlWhere = new StringBuilder();
            if (isAdd)
            {
                #region Insert
                foreach (var item in dict)
                {
                    if (item.Value == null || string.IsNullOrWhiteSpace(item.Value.ToString()))
                        continue;
                    if (item.Key.ToUpper() == "ID")
                        continue;
                    //
                    sqlField.Append("," + item.Key);
                    sqlValue.Append(",'" + item.Value + "'");
                }

                sql.Append("INSERT INTO " + formParam.TableName + " (");
                sql.Append(sqlField.ToString().Substring(1));
                sql.Append(") ");
                sql.Append(" VALUES(");
                sql.Append(sqlValue.ToString().Substring(1));
                sql.Append(" ) ");
                #endregion
            }
            else
            {
                #region update
                foreach (var item in dict)
                {
                    if (item.Value == null || string.IsNullOrWhiteSpace(item.Value.ToString()))
                        continue;
                    if (item.Key.ToUpper() == "ID")
                    {
                        sqlWhere.Append(" ID = '" + item.Value + "' ");
                        continue;
                    }
                    sqlValue.Append("," + item.Key + " = '" + item.Value + "' ");

                }
                sql.Append("UPDATE " + formParam.TableName + "  SET ");
                sql.Append(sqlValue.ToString().Substring(1));
                sql.Append(" WHERE ");
                sql.Append(sqlWhere.ToString());
                #endregion
            }

            return sql.ToString();
        }
       

        private string BuildSelectSql(dynamic model)
        {
            var jsonTem = JsonConvert.SerializeObject(model);
            IDictionary<string, object> dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(jsonTem);

            StringBuilder sql = new StringBuilder();



            StringBuilder sqlValue = new StringBuilder();
            StringBuilder sqlField = new StringBuilder();
            StringBuilder sqlWhere = new StringBuilder();


            return sql.ToString();
        }

        #endregion




    }
}
