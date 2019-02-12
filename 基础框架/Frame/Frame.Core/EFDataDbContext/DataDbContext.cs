using Abp.EntityFramework;
using Abp.Notifications;
using System.Data.Common;
using System.Data.Entity;

namespace Frame.Core
{

    public class DataDbContext : AbpDbContext
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


        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public DataDbContext()
                : base("Default")
        {

        }

        /* NOTE:
         *   ABP使用这个构造器来传递在 StartModuleCore.preinitialize 中定义的连接字符串。
         *   This constructor is used by ABP to pass connection string defined in FrameDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of FrameDbContext since ABP automatically handles it.
         */
        public DataDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public DataDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public DataDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }

       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            string defaultSchema = ConstantConfig.DBSchemaName;
            if (base.Database.Connection.GetType().Name == "OracleConnection")
            {
                //oracle数据库
                if (string.IsNullOrWhiteSpace(defaultSchema))
                    defaultSchema = "FRAMEDB";
                //一定要大写
                modelBuilder.HasDefaultSchema(defaultSchema.ToUpper());
            }
            if (base.Database.Connection.GetType().Name == "SqlConnection")
            {
                if (string.IsNullOrWhiteSpace(defaultSchema))
                    defaultSchema = "dbo";
                //
                modelBuilder.HasDefaultSchema(defaultSchema);
                //移除实体对象的 Column 属性名称,动态创建库时根据列的名称产生字段
                modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.ColumnAttributeConvention>();
                //modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.TableAttributeConvention>();
            }


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
            NotificationSubscriptionInfoContext.Property(p => p.CreationTime).HasColumnName(ToDBAttributeCase("CreationTime"));
            NotificationSubscriptionInfoContext.Property(p => p.CreatorUserId).HasColumnName(ToDBAttributeCase("CreatorUserId"));
            NotificationSubscriptionInfoContext.Property(p => p.TenantId).HasColumnName(ToDBAttributeCase("TenantId"));
            NotificationSubscriptionInfoContext.Property(p => p.UserId).HasColumnName(ToDBAttributeCase("UserId"));
            NotificationSubscriptionInfoContext.Property(p => p.NotificationName).HasColumnName(ToDBAttributeCase("NotificationName"));
            NotificationSubscriptionInfoContext.Property(p => p.EntityTypeName).HasColumnName(ToDBAttributeCase("EntityTypeName"));
            NotificationSubscriptionInfoContext.Property(p => p.EntityTypeAssemblyQualifiedName).HasColumnName(ToDBAttributeCase("EntityTypeAssemblyName"));
            NotificationSubscriptionInfoContext.Property(p => p.EntityId).HasColumnName(ToDBAttributeCase("EntityId"));
            #endregion

            #region TenantNotificationInfo To SYS_NOTIFICATIONSTOTENANT

            var TenantNotificationInfoContext = modelBuilder.Entity<TenantNotificationInfo>().ToTable(ToDBAttributeCase("Sys_NotificationsToTenant"));
            TenantNotificationInfoContext.Property(p => p.Id).HasColumnName(ToDBAttributeCase("Id"));
            TenantNotificationInfoContext.Property(p => p.CreationTime).HasColumnName(ToDBAttributeCase("CreationTime"));
            TenantNotificationInfoContext.Property(p => p.CreatorUserId).HasColumnName(ToDBAttributeCase("CreatorUserId"));
            TenantNotificationInfoContext.Property(p => p.TenantId).HasColumnName(ToDBAttributeCase("TenantId"));
            TenantNotificationInfoContext.Property(p => p.NotificationName).HasColumnName(ToDBAttributeCase("NotificationName"));
            TenantNotificationInfoContext.Property(p => p.Data).HasColumnName(ToDBAttributeCase("Data"));
            TenantNotificationInfoContext.Property(p => p.DataTypeName).HasColumnName(ToDBAttributeCase("DataTypeName"));
            TenantNotificationInfoContext.Property(p => p.EntityTypeName).HasColumnName(ToDBAttributeCase("EntityTypeName"));
            TenantNotificationInfoContext.Property(p => p.EntityTypeAssemblyQualifiedName).HasColumnName(ToDBAttributeCase("EntityTypeAssemblyName"));
            TenantNotificationInfoContext.Property(p => p.EntityId).HasColumnName(ToDBAttributeCase("EntityId"));
            TenantNotificationInfoContext.Property(p => p.Severity).HasColumnName(ToDBAttributeCase("Severity"));

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

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string ToDBAttributeCase(string str)
        {
            if (base.Database.Connection.GetType().Name == "OracleConnection")
            {
                return str.ToUpper();
            }
            if (base.Database.Connection.GetType().Name == "SqlConnection")
            {
                return str;
            }
            return str;
        }


    }
}
