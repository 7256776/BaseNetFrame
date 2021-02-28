using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreFrame.Infrastructure
{
    public class DbSqlInit
    {
        /// <summary>
        /// 设置字段扩展属性
        /// </summary>
        /// <param name="modelBuilder"></param>
        public List<StringBuilder> SetExtendedProperties(ModelBuilder modelBuilder)
        {
            List<StringBuilder> tabList = new List<StringBuilder>();

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                StringBuilder strFieldsList = new StringBuilder();
                string schema = entity.Relational().Schema;
                // 表名
                string tableName = entity.Relational().TableName;
                // 所有列名
                foreach (var property in entity.GetProperties())
                {
                    //属性字段名称
                    string columnName = property.Relational().ColumnName;
                    //获取属性设置了(DescriptionAttribute)标签的属性信息
                    string description = "";
                    var pdata = property.PropertyInfo.GetCustomAttributesData().Where(w => w.AttributeType.Name == "DescriptionAttribute").FirstOrDefault();
                    if (pdata != null && pdata.ConstructorArguments.Any())
                    {
                        var descriptionTmp = pdata.ConstructorArguments[0].Value;
                        description = descriptionTmp == null ? "" : descriptionTmp.ToString();
                    }
                    //设置默认字段的说明信息
                    description = this.SetDefaultDescription(columnName, description);
                    #region 
                    StringBuilder strFields = new StringBuilder();
                    strFields.Append("EXEC sys.sp_addextendedproperty ");
                    strFields.Append("@name = 'MS_Description', ");
                    strFields.Append("@level0type = N'SCHEMA', ");
                    strFields.Append("@level1type = N'TABLE', ");
                    strFields.Append("@level2type = N'COLUMN', ");
                    //
                    strFields.AppendFormat("@level0name = N'{0}',  ", schema);
                    strFields.AppendFormat("@level1name = N'{0}', ", tableName);
                    strFields.AppendFormat("@level2name = N'{0}', ", columnName);
                    strFields.AppendFormat("@value = N'{0}' ", description);
                    #endregion
                    strFieldsList.Append(strFields);
                }

                #region 
                //判断扩展信息是否存在(此处只对该表是否存在扩展信息做验证,不对每个字段做验证)
                StringBuilder strExist = new StringBuilder();
                strExist.Append("IF NOT EXISTS(");
                strExist.Append("SELECT 1 FROM sys.extended_properties ep ");
                strExist.Append("   INNER JOIN sys.tables AS t ON ep.major_id = t.object_id ");
                //strExist.Append("   INNER JOIN sys.columns AS c ON ep.major_id = c.object_id AND ep.minor_id = c.column_id ");
                strExist.AppendFormat("WHERE t.NAME='{0}' ", tableName);
                //strExist.Append("   AND c.NAME='UserName' ");
                strExist.Append(") ");
                strExist.Append("BEGIN ");
                strExist.Append(strFieldsList);
                strExist.Append("END ");
                #endregion
                //
                tabList.Add(strExist);
            }
            return tabList; 
        }

        private string SetDefaultDescription(string columnName, string description)
        {
            if (!string.IsNullOrEmpty(description))
            {
                return description;
            }
            switch (columnName.ToUpper())
            {
                case "ID":
                    return "主键ID";
                case "CREATIONTIME":
                    return "创建日期";
                case "CREATORUSERID":
                    return "创建人ID";
                case "LASTMODIFICATIONTIME":
                    return "最后更新日期";
                case "LASTMODIFIERUSERID":
                    return "最后更新人ID";
                case "DELETIONTIME":
                    return "删除日期";
                case "DELETERUSERID":
                    return "删除人ID";
                case "ISACTIVE":
                    return "是否启用 0=否 1=是";
                default:
                    return description;
            }
        }



    }
}
