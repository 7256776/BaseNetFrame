using Abp.EntityFrameworkCore;
using Abp.Notifications;
using Microsoft.EntityFrameworkCore;
using NetCoreWorkFlow.Core;
using System.Data.Common;

namespace NetCoreWorkFlow.Core
{
    public class NetCoreWorkFlowDbContext : AbpDbContext
    {

        //TODO: Define an IDbSet for each Entity...

        #region 系统基础表

        public virtual DbSet<SysWorkFlowSetting> SysWorkFlowSettings { set; get; }
        public virtual DbSet<SysWorkFlowConnection> SysWorkFlowConnections { set; get; }
        public virtual DbSet<SysWorkFlowEndpoint> SysWorkFlowEndpoints { set; get; }
        public virtual DbSet<SysWorkFlowRole> SysWorkFlowRoles { set; get; }
        public virtual DbSet<SysWorkFlowRoleToUser> SysWorkFlowRoleToUsers { set; get; }
        public virtual DbSet<SysWorkFlowDataSource> SysWorkFlowDataSources { set; get; }
        public virtual DbSet<SysWorkFlowType> SysWorkFlowTypes { set; get; }

        /// <summary>
        /// 视图
        /// </summary>
        public virtual DbSet<ViewSysFlowRoleToUser> ViewSysFlowRoleToUsers { set; get; }


        #endregion

        public NetCoreWorkFlowDbContext(DbContextOptions<NetCoreWorkFlowDbContext> options)
         : base(options)
        {
        }

        protected NetCoreWorkFlowDbContext(DbContextOptions options)
           : base(options)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string defaultSchema = ConstantConfig.DBSchemaName;
            if (base.Database.GetDbConnection().GetType().Name == "OracleConnection")
            {
                //oracle数据库
                if (string.IsNullOrWhiteSpace(defaultSchema))
                    defaultSchema = "FRAMEDB";
                //一定要大写
                modelBuilder.HasDefaultSchema(defaultSchema.ToUpper());
            }
            if (base.Database.GetDbConnection().GetType().Name == "SqlConnection")
            {
                if (string.IsNullOrWhiteSpace(defaultSchema))
                    defaultSchema = "dbo";
                //
                modelBuilder.HasDefaultSchema(defaultSchema);
            }
            //
            this.SetAnnotation(modelBuilder);
            //
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 设置实体对象的 Table,Column 属性名称
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void SetAnnotation(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // 重置所有表名
                entity.Relational().TableName = this.ToDBAttributeCase(entity.Relational().TableName);
                // 重置所有列名
                foreach (var property in entity.GetProperties())
                {
                    property.Relational().ColumnName = this.ToDBAttributeCase(property.Relational().ColumnName);
                }
                #region 
                // 重置所有主键名
                //foreach (var key in entity.GetKeys())
                //{
                //    key.Relational().Name = key.Relational().Name;
                //}

                // 重置所有外键名
                //foreach (var key in entity.GetForeignKeys())
                //{
                //    key.Relational().Name = key.Relational().Name;
                //}

                // 重置所有索引名
                //foreach (var index in entity.GetIndexes())
                //{
                //    index.Relational().Name = index.Relational().Name;
                //}
                #endregion
            }
        }

        /// <summary>
        /// 根据数据库进行转换字段与表名称的大小写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string ToDBAttributeCase(string str)
        {
            if (base.Database.GetDbConnection().GetType().Name == "OracleConnection")
            {
                return str.ToUpper();
            }
            if (base.Database.GetDbConnection().GetType().Name == "SqlConnection")
            {
                return str;
            }
            return str;
        }


    }
}
