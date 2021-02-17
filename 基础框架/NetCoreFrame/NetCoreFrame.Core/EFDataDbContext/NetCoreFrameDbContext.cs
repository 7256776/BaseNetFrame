using Abp.EntityFrameworkCore;
using Abp.Notifications;
using Microsoft.EntityFrameworkCore;
using NetCoreFrame.Core;
using System.Data.Common;

namespace NetCoreFrame.Core
{
    public class NetCoreFrameDbContext : AbpDbContext
    {

        //TODO: Define an IDbSet for each Entity...

        #region 系统基础表

        public virtual DbSet<SysOrg> SysOrgs { set; get; }

        public virtual DbSet<SysRoles> SysRoless { set; get; }

        public virtual DbSet<UserInfo> UserInfos { set; get; }

        public virtual DbSet<SysMenus> SysMenuss { set; get; }

        public virtual DbSet<SysMenuAction> SysMenuActions { set; get; }

        public virtual DbSet<SysRoleToMenuAction> SysRoleToMenuActions { set; get; }

        public virtual DbSet<SysRoleToUser> SysRoleToUsers { set; get; }

        public virtual DbSet<SysSetting> SysSettings { set; get; }

        public virtual DbSet<SysDict> SysDicts { set; get; }

        public virtual DbSet<SysDictType> SysDictTypes { set; get; }
        #endregion

        #region 通知订阅
        public virtual DbSet<NotificationInfo> NotificationInfos { set; get; }
        public virtual DbSet<UserNotificationInfo> UserNotificationInfos { set; get; }
        public virtual DbSet<TenantNotificationInfo> TenantNotificationInfos { set; get; }
        public virtual DbSet<NotificationSubscriptionInfo> NotificationSubscriptionInfos { set; get; }
        public virtual DbSet<SysNotificationInfo> SysNotificationInfos { set; get; }
        public virtual DbSet<SysChatRecord> SysChatRecords { set; get; }
        #endregion

        #region 审计日志
        public virtual DbSet<SysAuditLog> SysAuditLogs { set; get; }
        #endregion

        #region Api服务授权
        public virtual DbSet<SysApiResource> SysApiResources { set; get; }
        public virtual DbSet<SysApiClient> SysApiClients { set; get; }
        public virtual DbSet<SysApiAccount> SysApiAccounts { set; get; }
        public virtual DbSet<SysApiResourceToClient> SysApiResourceToClients { set; get; }
        public virtual DbSet<SysApiClienToAccount> SysApiClienToAccounts { set; get; }
        #endregion

        public NetCoreFrameDbContext(DbContextOptions<NetCoreFrameDbContext> options)
         : base(options)
        {
        }

        protected NetCoreFrameDbContext(DbContextOptions options)
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

            #region 

            #region 通知对象

            #region NotificationInfo To SYS_NOTIFICATIONSSEND

            var NotificationInfoContext = modelBuilder.Entity<NotificationInfo>().ToTable(ToDBAttributeCase("Sys_NotificationsSend"));
            NotificationInfoContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            NotificationInfoContext.Property(p => p.CreationTime).HasColumnName(ToDBAttributeCase("CreationTime"));
            NotificationInfoContext.Property(p => p.CreatorUserId).HasColumnName(ToDBAttributeCase("CreatorUserId"));
            NotificationInfoContext.Property(p => p.NotificationName).HasColumnName(ToDBAttributeCase("NotificationName"));
            NotificationInfoContext.Property(p => p.Data).HasColumnName(ToDBAttributeCase("Data"));
            NotificationInfoContext.Property(p => p.DataTypeName).HasColumnName(ToDBAttributeCase("DataTypeName"));
            NotificationInfoContext.Property(p => p.EntityTypeName).HasColumnName(ToDBAttributeCase("EntityTypeName"));
            NotificationInfoContext.Property(p => p.EntityTypeAssemblyQualifiedName).HasColumnName(ToDBAttributeCase("EntityTypeAssemblyName"));
            NotificationInfoContext.Property(p => p.EntityId).HasColumnName(ToDBAttributeCase("EntityId"));
            NotificationInfoContext.Property(p => p.Severity).HasColumnName(ToDBAttributeCase("Severity"));
            NotificationInfoContext.Property(p => p.UserIds).HasColumnName(ToDBAttributeCase("UserIds"));
            NotificationInfoContext.Property(p => p.ExcludedUserIds).HasColumnName(ToDBAttributeCase("ExcludedUserIds"));
            NotificationInfoContext.Property(p => p.TenantIds).HasColumnName(ToDBAttributeCase("TenantIds"));

            #endregion

            #region NotificationSubscriptionInfo To SYS_NOTIFICATIONSUBSCRIPTIONS
            var NotificationSubscriptionInfoContext = modelBuilder.Entity<NotificationSubscriptionInfo>().ToTable(ToDBAttributeCase("Sys_NotificationSubscriptions"));
            NotificationSubscriptionInfoContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            NotificationSubscriptionInfoContext.Property(p => p.TenantId).HasColumnName(ToDBAttributeCase("TenantId"));
            NotificationSubscriptionInfoContext.Property(p => p.UserId).HasColumnName(ToDBAttributeCase("UserId"));
            NotificationSubscriptionInfoContext.Property(p => p.NotificationName).HasColumnName(ToDBAttributeCase("NotificationName"));
            NotificationSubscriptionInfoContext.Property(p => p.EntityTypeName).HasColumnName(ToDBAttributeCase("EntityTypeName"));
            NotificationSubscriptionInfoContext.Property(p => p.EntityTypeAssemblyQualifiedName).HasColumnName(ToDBAttributeCase("EntityTypeAssemblyName"));
            NotificationSubscriptionInfoContext.Property(p => p.EntityId).HasColumnName(ToDBAttributeCase("EntityId"));
            NotificationSubscriptionInfoContext.Property(p => p.CreationTime).HasColumnName(ToDBAttributeCase("CreationTime"));
            NotificationSubscriptionInfoContext.Property(p => p.CreatorUserId).HasColumnName(ToDBAttributeCase("CreatorUserId"));
            #endregion

            #region TenantNotificationInfo To SYS_NOTIFICATIONSTOTENANT

            var TenantNotificationInfoContext = modelBuilder.Entity<TenantNotificationInfo>().ToTable(ToDBAttributeCase("Sys_NotificationsToTenant"));
            TenantNotificationInfoContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            TenantNotificationInfoContext.Property(p => p.TenantId).HasColumnName(ToDBAttributeCase("TenantId"));
            TenantNotificationInfoContext.Property(p => p.NotificationName).HasColumnName(ToDBAttributeCase("NotificationName"));
            TenantNotificationInfoContext.Property(p => p.Data).HasColumnName(ToDBAttributeCase("Data"));
            TenantNotificationInfoContext.Property(p => p.DataTypeName).HasColumnName(ToDBAttributeCase("DataTypeName"));
            TenantNotificationInfoContext.Property(p => p.EntityTypeName).HasColumnName(ToDBAttributeCase("EntityTypeName"));
            TenantNotificationInfoContext.Property(p => p.EntityTypeAssemblyQualifiedName).HasColumnName(ToDBAttributeCase("EntityTypeAssemblyName"));
            TenantNotificationInfoContext.Property(p => p.EntityId).HasColumnName(ToDBAttributeCase("EntityId"));
            TenantNotificationInfoContext.Property(p => p.Severity).HasColumnName(ToDBAttributeCase("Severity"));
            TenantNotificationInfoContext.Property(p => p.CreationTime).HasColumnName(ToDBAttributeCase("CreationTime"));
            TenantNotificationInfoContext.Property(p => p.CreatorUserId).HasColumnName(ToDBAttributeCase("CreatorUserId"));

            #endregion

            #region UserNotificationInfo To SYS_NOTIFICATIONSTOUSER

            var UserNotificationInfoContext = modelBuilder.Entity<UserNotificationInfo>().ToTable(ToDBAttributeCase("Sys_NotificationsToUser"));
            UserNotificationInfoContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            UserNotificationInfoContext.Property(p => p.CreationTime).HasColumnName(ToDBAttributeCase("CreationTime"));
            UserNotificationInfoContext.Property(p => p.TenantId).HasColumnName(ToDBAttributeCase("TenantId"));
            UserNotificationInfoContext.Property(p => p.UserId).HasColumnName(ToDBAttributeCase("UserId"));
            UserNotificationInfoContext.Property(p => p.TenantNotificationId).HasColumnName(ToDBAttributeCase("TenantNotificationId"));
            UserNotificationInfoContext.Property(p => p.State).HasColumnName(ToDBAttributeCase("State"));

            #endregion

            #region SysNotificationInfo

            var SysNotificationInfoContext = modelBuilder.Entity<SysNotificationInfo>().ToTable(ToDBAttributeCase("Sys_NotificationInfo"));
            SysNotificationInfoContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            SysNotificationInfoContext.Property(p => p.CreationTime).HasColumnName(ToDBAttributeCase("CreationTime"));
            SysNotificationInfoContext.Property(p => p.CreatorUserId).HasColumnName(ToDBAttributeCase("CreatorUserId"));

            #endregion

            #endregion

            #region 设置审计日志字符串长度 AuditLog
            //审计日志
            var SysAuditLogContext = modelBuilder.Entity<SysAuditLog>().ToTable(ToDBAttributeCase("Sys_AuditLogs"));
            SysAuditLogContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            //设置字段长度
            SysAuditLogContext.Property(e => e.ServiceName).HasMaxLength(SysAuditLog.MaxServiceNameLength);
            SysAuditLogContext.Property(e => e.MethodName).HasMaxLength(SysAuditLog.MaxMethodNameLength);
            SysAuditLogContext.Property(e => e.Parameters).HasMaxLength(SysAuditLog.MaxParametersLength);
            SysAuditLogContext.Property(e => e.ClientIpAddress).HasMaxLength(SysAuditLog.MaxClientIpAddressLength);
            SysAuditLogContext.Property(e => e.ClientName).HasMaxLength(SysAuditLog.MaxClientNameLength);
            SysAuditLogContext.Property(e => e.BrowserInfo).HasMaxLength(SysAuditLog.MaxBrowserInfoLength);
            SysAuditLogContext.Property(e => e.Exception).HasMaxLength(SysAuditLog.MaxExceptionLength);
            SysAuditLogContext.Property(e => e.CustomData).HasMaxLength(SysAuditLog.MaxCustomDataLength);

            #endregion

            #region 系统基础表

            //字典管理
            var SysDictContext = modelBuilder.Entity<SysDict>().ToTable(ToDBAttributeCase("Sys_Dict"));
            SysDictContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            var SysDictTypeContext = modelBuilder.Entity<SysDictType>().ToTable(ToDBAttributeCase("Sys_DictType"));
            SysDictTypeContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            //组织管理
            var SysOrgContext = modelBuilder.Entity<SysOrg>().ToTable(ToDBAttributeCase("Sys_Org"));
            SysOrgContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            SysOrgContext.Property(p => p.CreationTime).HasColumnName(ToDBAttributeCase("CreationTime"));
            SysOrgContext.Property(p => p.CreatorUserId).HasColumnName(ToDBAttributeCase("CreatorUserId"));
            SysOrgContext.Property(p => p.LastModificationTime).HasColumnName(ToDBAttributeCase("LastModificationTime"));
            SysOrgContext.Property(p => p.LastModifierUserId).HasColumnName(ToDBAttributeCase("LastModifierUserId"));
            //模块管理
            var SysMenusContext = modelBuilder.Entity<SysMenus>().ToTable(ToDBAttributeCase("Sys_Menus"));
            SysMenusContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            //模块动作管理 
            var SysMenuActionContext = modelBuilder.Entity<SysMenuAction>().ToTable(ToDBAttributeCase("Sys_MenuAction"));
            SysMenuActionContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            //角色管理
            var SysRolesContext = modelBuilder.Entity<SysRoles>().ToTable(ToDBAttributeCase("Sys_Roles"));
            SysRolesContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            SysRolesContext.Property(p => p.CreationTime).HasColumnName(ToDBAttributeCase("CreationTime"));
            SysRolesContext.Property(p => p.CreatorUserId).HasColumnName(ToDBAttributeCase("CreatorUserId"));
            SysRolesContext.Property(p => p.LastModificationTime).HasColumnName(ToDBAttributeCase("LastModificationTime"));
            SysRolesContext.Property(p => p.LastModifierUserId).HasColumnName(ToDBAttributeCase("LastModifierUserId"));
            //用户设置
            var SysSettingContext = modelBuilder.Entity<SysSetting>().ToTable(ToDBAttributeCase("Sys_Settings"));
            SysSettingContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            SysSettingContext.Property(p => p.CreationTime).HasColumnName(ToDBAttributeCase("CreationTime"));
            SysSettingContext.Property(p => p.CreatorUserId).HasColumnName(ToDBAttributeCase("CreatorUserId"));
            SysSettingContext.Property(p => p.LastModificationTime).HasColumnName(ToDBAttributeCase("LastModificationTime"));
            SysSettingContext.Property(p => p.LastModifierUserId).HasColumnName(ToDBAttributeCase("LastModifierUserId"));
            //用户管理
            var SysUserAccountsContext = modelBuilder.Entity<UserInfo>().ToTable(ToDBAttributeCase("Sys_UserAccounts"));
            SysUserAccountsContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            SysUserAccountsContext.Property(p => p.CreationTime).HasColumnName(ToDBAttributeCase("CreationTime"));
            SysUserAccountsContext.Property(p => p.CreatorUserId).HasColumnName(ToDBAttributeCase("CreatorUserId"));
            SysUserAccountsContext.Property(p => p.LastModificationTime).HasColumnName(ToDBAttributeCase("LastModificationTime"));
            SysUserAccountsContext.Property(p => p.LastModifierUserId).HasColumnName(ToDBAttributeCase("LastModifierUserId"));
            SysUserAccountsContext.Property(p => p.DeletionTime).HasColumnName(ToDBAttributeCase("DeletionTime"));
            SysUserAccountsContext.Property(p => p.DeleterUserId).HasColumnName(ToDBAttributeCase("DeleterUserId"));
            SysUserAccountsContext.Property(p => p.IsDeleted).HasColumnName(ToDBAttributeCase("IsDeleted"));
            //角色与模块
            var SysRoleToMenuActionContext = modelBuilder.Entity<SysRoleToMenuAction>().ToTable(ToDBAttributeCase("Sys_RoleToMenuAction"));
            SysRoleToMenuActionContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            //角色与用户
            var SysRoleToUserContext = modelBuilder.Entity<SysRoleToUser>().ToTable(ToDBAttributeCase("Sys_RoleToUser"));
            SysRoleToUserContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            //聊天记录
            var SysChatRecordContext = modelBuilder.Entity<SysChatRecord>().ToTable(ToDBAttributeCase("Sys_ChatRecord"));
            SysChatRecordContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            SysChatRecordContext.Property(p => p.CreationTime).HasColumnName(ToDBAttributeCase("CreationTime"));
            SysChatRecordContext.Property(p => p.CreatorUserId).HasColumnName(ToDBAttributeCase("CreatorUserId"));

            #endregion

            #region Api服务授权
            var SysApiResourceContext = modelBuilder.Entity<SysApiResource>().ToTable(ToDBAttributeCase("Sys_ApiResource"));
            SysApiResourceContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            SysApiResourceContext.Property(p => p.CreationTime).HasColumnName(ToDBAttributeCase("CreationTime"));
            SysApiResourceContext.Property(p => p.CreatorUserId).HasColumnName(ToDBAttributeCase("CreatorUserId"));
            SysApiResourceContext.Property(p => p.LastModificationTime).HasColumnName(ToDBAttributeCase("LastModificationTime"));
            SysApiResourceContext.Property(p => p.LastModifierUserId).HasColumnName(ToDBAttributeCase("LastModifierUserId"));

            var SysApiClientContext = modelBuilder.Entity<SysApiClient>().ToTable(ToDBAttributeCase("Sys_ApiClient"));
            SysApiClientContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            SysApiClientContext.Property(p => p.CreationTime).HasColumnName(ToDBAttributeCase("CreationTime"));
            SysApiClientContext.Property(p => p.CreatorUserId).HasColumnName(ToDBAttributeCase("CreatorUserId"));
            SysApiClientContext.Property(p => p.LastModificationTime).HasColumnName(ToDBAttributeCase("LastModificationTime"));
            SysApiClientContext.Property(p => p.LastModifierUserId).HasColumnName(ToDBAttributeCase("LastModifierUserId"));

            var SysApiAccountContext = modelBuilder.Entity<SysApiAccount>().ToTable(ToDBAttributeCase("Sys_ApiAccount"));
            SysApiAccountContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            SysApiAccountContext.Property(p => p.CreationTime).HasColumnName(ToDBAttributeCase("CreationTime"));
            SysApiAccountContext.Property(p => p.CreatorUserId).HasColumnName(ToDBAttributeCase("CreatorUserId"));
            SysApiAccountContext.Property(p => p.LastModificationTime).HasColumnName(ToDBAttributeCase("LastModificationTime"));
            SysApiAccountContext.Property(p => p.LastModifierUserId).HasColumnName(ToDBAttributeCase("LastModifierUserId"));

            var SysApiResourceToClientContext = modelBuilder.Entity<SysApiResourceToClient>().ToTable(ToDBAttributeCase("Sys_ApiResourceToClient"));
            SysApiResourceToClientContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));

            var SysApiClienToAccountContext = modelBuilder.Entity<SysApiClienToAccount>().ToTable(ToDBAttributeCase("Sys_ApiClienToAccount"));
            SysApiClienToAccountContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));

            #endregion

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
                //ToDo移除实体对象的Table标签(由于实体类与表名格式不同因此都进行了转义因此此处暂时省略
                //if (entity.FindAnnotation("Relational:TableName") != null)
                //{
                //    //entity.RemoveAnnotation("Relational:TableName");
                //    //entity.SetAnnotation("Relational:TableName", entity.FindAnnotation("Relational:TableName").Value.ToString().ToLower());
                //}

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
