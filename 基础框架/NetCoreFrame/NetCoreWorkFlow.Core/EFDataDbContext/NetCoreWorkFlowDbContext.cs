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
                RemoveAnnotation(modelBuilder);
            }

            #region Sys_WorkFlowSetting
            var sysWorkFlowSettingContext = modelBuilder.Entity<SysWorkFlowSetting>().ToTable(ToDBAttributeCase("Sys_WorkFlowSetting"));
            sysWorkFlowSettingContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            sysWorkFlowSettingContext.Property(p => p.CreationTime).HasColumnName(ToDBAttributeCase("CreationTime"));
            sysWorkFlowSettingContext.Property(p => p.CreatorUserId).HasColumnName(ToDBAttributeCase("CreatorUserId"));
            sysWorkFlowSettingContext.Property(p => p.LastModificationTime).HasColumnName(ToDBAttributeCase("LastModificationTime"));
            sysWorkFlowSettingContext.Property(p => p.LastModifierUserId).HasColumnName(ToDBAttributeCase("LastModifierUserId")); 
            #endregion

            #region Sys_WorkFlowConnection
            var sysWorkFlowConnectionContext = modelBuilder.Entity<SysWorkFlowConnection>().ToTable(ToDBAttributeCase("Sys_WorkFlowConnection"));
            sysWorkFlowConnectionContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));

            #endregion

            #region Sys_WorkFlowEndpoint
            var sysWorkFlowEndpointContext = modelBuilder.Entity<SysWorkFlowEndpoint>().ToTable(ToDBAttributeCase("Sys_WorkFlowEndpoint"));
            sysWorkFlowEndpointContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));

            #endregion

            #region Sys_WorkFlowRoles
            var sysWorkFlowRolesContext = modelBuilder.Entity<SysWorkFlowRole>().ToTable(ToDBAttributeCase("Sys_WorkFlowRole"));
            sysWorkFlowRolesContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            sysWorkFlowRolesContext.Property(p => p.CreationTime).HasColumnName(ToDBAttributeCase("CreationTime"));
            sysWorkFlowRolesContext.Property(p => p.CreatorUserId).HasColumnName(ToDBAttributeCase("CreatorUserId"));
            sysWorkFlowRolesContext.Property(p => p.LastModificationTime).HasColumnName(ToDBAttributeCase("LastModificationTime"));
            sysWorkFlowRolesContext.Property(p => p.LastModifierUserId).HasColumnName(ToDBAttributeCase("LastModifierUserId"));
            #endregion


            #region Sys_WorkFlowEndpoint
            var sysWorkFlowRoleToUserContext = modelBuilder.Entity<SysWorkFlowRoleToUser>().ToTable(ToDBAttributeCase("Sys_WorkFlowRoleToUser"));
            sysWorkFlowRoleToUserContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 移除实体对象的 Table,Column 属性名称,动态创建库时根据列的名称产生字段
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void RemoveAnnotation(ModelBuilder modelBuilder)
        {
            var entitys = modelBuilder.Model.GetEntityTypes();
            foreach (var entity in entitys)
            {
                var properties = entity.GetProperties();
                if (properties == null)
                    continue;

                foreach (var pro in properties)
                {
                    if (pro.FindAnnotation("Relational:ColumnName") != null)
                    {
                        pro.RemoveAnnotation("Relational:ColumnName");
                    }
                }
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
