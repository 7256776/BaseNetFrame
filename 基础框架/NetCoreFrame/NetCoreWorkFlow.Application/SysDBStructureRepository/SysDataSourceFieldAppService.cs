using Abp.Authorization;
using Abp.Data;
using Abp.Domain.Entities;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWorkFlow.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class SysDataSourceFieldAppService : NetCoreWorkFlowApplicationBase, ISysDataSourceFieldAppService
    {
        private readonly ISysDataSourceFieldRepository _sysDataSourceFieldSqlServerRepository;
        private readonly ISysWorkFlowDataSourceItemRepository _sysWorkFlowDataSourceItemRepository;
        private readonly ISysWorkFlowDataSourceRepository _sysWorkFlowDataSourceRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysDataSourceFieldRepository"></param>
        /// <param name="sysWorkFlowDataSourceItemRepository"></param>
        public SysDataSourceFieldAppService(
            ISysDataSourceFieldRepository sysDataSourceFieldSqlServerRepository,
            ISysWorkFlowDataSourceItemRepository sysWorkFlowDataSourceItemRepository,
            ISysWorkFlowDataSourceRepository sysWorkFlowDataSourceRepository)
        {
            _sysDataSourceFieldSqlServerRepository = sysDataSourceFieldSqlServerRepository;
            _sysWorkFlowDataSourceItemRepository = sysWorkFlowDataSourceItemRepository;
            _sysWorkFlowDataSourceRepository = sysWorkFlowDataSourceRepository;
        }

        /// <summary>
        /// 获取表字段集合
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public List<SysWorkFlowDataSourceItemData> GetDataStructure(string dataSourceId)
        {
            //
            var dataSource = _sysWorkFlowDataSourceRepository.Get(new Guid(dataSourceId));
            //
            List<SysDataSourceField> fieldData = _sysDataSourceFieldSqlServerRepository.GetDataStructure(dataSource.DataSourceName);
            //
            List<SysWorkFlowDataSourceItem> ItemData = _sysWorkFlowDataSourceItemRepository.GetAllList(w => w.DataSourceId == dataSource.Id);

            var dataList = from field in fieldData
                           join item in ItemData
                           on field.ColumnName equals item.FieldName into itemJoin
                           from itemTmp in itemJoin.DefaultIfEmpty()
                           select new SysWorkFlowDataSourceItemData
                           {
                               Id = itemTmp?.Id,
                               FieldAliasName = itemTmp == null ? field.ColumnName : itemTmp.FieldAliasName,
                               FieldName = itemTmp == null ? field.ColumnName : itemTmp.FieldName,
                               FieldDataType = itemTmp == null ? field.DataType : itemTmp.FieldDataType,
                               FieldDataDisabled = false,
                               IsActive = itemTmp == null ? false : itemTmp.IsActive.Value,
                               DataSourceId = dataSourceId,
                               Description = itemTmp == null ? "" : itemTmp.Description,
                               States = itemTmp == null ? "ADD" : "NONE",
                           };
            return dataList.ToList();
        }

        /// <summary>
        /// 保存数据源字段
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task SaveWorkFlowDataSourceItem(List<SysWorkFlowDataSourceItemInput> list)
        {
            foreach (var model in list)
            {
                if (model.States.ToUpper() == "ADD" || model.States.ToUpper() == "EDIT")
                {
                    //映射数据对象
                    SysWorkFlowDataSourceItem updatData = ObjectMapper.Map<SysWorkFlowDataSourceItem>(model);
                    //提交修改
                    await _sysWorkFlowDataSourceItemRepository.InsertOrUpdateAsync(updatData);
                }
            }
        }




    }
}
