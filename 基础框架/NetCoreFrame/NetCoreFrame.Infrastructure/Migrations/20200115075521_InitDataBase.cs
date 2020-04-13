using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreFrame.Infrastructure.Migrations
{
    public partial class InitDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Sys_AuditLogs",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: true),
                    ServiceName = table.Column<string>(maxLength: 256, nullable: true),
                    MethodName = table.Column<string>(maxLength: 256, nullable: true),
                    Parameters = table.Column<string>(maxLength: 1024, nullable: true),
                    ExecutionTime = table.Column<DateTime>(nullable: false),
                    ExecutionDuration = table.Column<int>(nullable: false),
                    ClientIpAddress = table.Column<string>(maxLength: 64, nullable: true),
                    ClientName = table.Column<string>(maxLength: 128, nullable: true),
                    BrowserInfo = table.Column<string>(maxLength: 256, nullable: true),
                    Exception = table.Column<string>(maxLength: 2000, nullable: true),
                    ImpersonatorUserId = table.Column<long>(nullable: true),
                    ImpersonatorTenantId = table.Column<int>(nullable: true),
                    CustomData = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_ChatRecord",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    ChatDetailed = table.Column<string>(maxLength: 1000, nullable: true),
                    SenderUserId = table.Column<long>(nullable: false),
                    ReceiveUserId = table.Column<long>(nullable: false),
                    ChatState = table.Column<int>(nullable: false),
                    SendOrReceive = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_ChatRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Dict",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DictType = table.Column<string>(maxLength: 50, nullable: false),
                    DictContent = table.Column<string>(maxLength: 50, nullable: false),
                    DictCode = table.Column<string>(maxLength: 50, nullable: false),
                    DictValue = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Dict", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_DictType",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DictType = table.Column<string>(maxLength: 50, nullable: false),
                    DictTypeName = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_DictType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Menus",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentID = table.Column<long>(nullable: true),
                    MenuDisplayName = table.Column<string>(maxLength: 50, nullable: true),
                    MenuName = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    CustomData = table.Column<string>(maxLength: 1000, nullable: true),
                    PermissionName = table.Column<string>(maxLength: 100, nullable: true),
                    RequiresAuthModel = table.Column<string>(maxLength: 10, nullable: true),
                    Url = table.Column<string>(maxLength: 100, nullable: true),
                    Icon = table.Column<string>(maxLength: 50, nullable: true),
                    OrderBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_NotificationInfo",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    NotificationDisplayName = table.Column<string>(maxLength: 100, nullable: true),
                    NotificationName = table.Column<string>(maxLength: 100, nullable: true),
                    NotificationDescribe = table.Column<string>(maxLength: 100, nullable: true),
                    NotificationType = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_NotificationInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_NotificationsSend",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    NotificationName = table.Column<string>(maxLength: 96, nullable: false),
                    Data = table.Column<string>(maxLength: 1048576, nullable: true),
                    DataTypeName = table.Column<string>(maxLength: 512, nullable: true),
                    EntityTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    EntityTypeAssemblyName = table.Column<string>(maxLength: 512, nullable: true),
                    EntityId = table.Column<string>(maxLength: 96, nullable: true),
                    Severity = table.Column<byte>(nullable: false),
                    UserIds = table.Column<string>(maxLength: 131072, nullable: true),
                    ExcludedUserIds = table.Column<string>(maxLength: 131072, nullable: true),
                    TenantIds = table.Column<string>(maxLength: 131072, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_NotificationsSend", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_NotificationsToTenant",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    NotificationName = table.Column<string>(maxLength: 96, nullable: false),
                    Data = table.Column<string>(maxLength: 1048576, nullable: true),
                    DataTypeName = table.Column<string>(maxLength: 512, nullable: true),
                    EntityTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    EntityTypeAssemblyName = table.Column<string>(maxLength: 512, nullable: true),
                    EntityId = table.Column<string>(maxLength: 96, nullable: true),
                    Severity = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_NotificationsToTenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_NotificationsToUser",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    TenantNotificationId = table.Column<Guid>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_NotificationsToUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_NotificationSubscriptions",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    NotificationName = table.Column<string>(maxLength: 96, nullable: true),
                    EntityTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    EntityTypeAssemblyName = table.Column<string>(maxLength: 512, nullable: true),
                    EntityId = table.Column<string>(maxLength: 96, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_NotificationSubscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Org",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParentOrgID = table.Column<Guid>(nullable: true),
                    OrgCode = table.Column<string>(maxLength: 100, nullable: false),
                    OrgName = table.Column<string>(maxLength: 100, nullable: false),
                    OrgNode = table.Column<string>(maxLength: 1000, nullable: false),
                    OrgType = table.Column<string>(maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Org", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Roles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    TenantId = table.Column<long>(nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    RoleName = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_RoleToMenuAction",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleID = table.Column<long>(nullable: false),
                    MenuID = table.Column<long>(nullable: false),
                    MenuActionID = table.Column<long>(nullable: true),
                    IsMenu = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_RoleToMenuAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Settings",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_UserAccounts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    UserCode = table.Column<string>(maxLength: 100, nullable: false),
                    UserNameCn = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 200, nullable: false),
                    TenantId = table.Column<long>(nullable: true),
                    ImageUrl = table.Column<string>(maxLength: 100, nullable: true),
                    Sex = table.Column<string>(maxLength: 20, nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    OrgCode = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_UserAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_MenuAction",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MenuID = table.Column<long>(nullable: false),
                    ActionDisplayName = table.Column<string>(maxLength: 50, nullable: true),
                    ActionName = table.Column<string>(maxLength: 50, nullable: true),
                    PermissionName = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    RequiresAuthModel = table.Column<string>(maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_MenuAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sys_MenuAction_Sys_Menus_MenuID",
                        column: x => x.MenuID,
                        principalSchema: "dbo",
                        principalTable: "Sys_Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sys_RoleToUser",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleID = table.Column<long>(nullable: false),
                    UserID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_RoleToUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sys_RoleToUser_Sys_Roles_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "dbo",
                        principalTable: "Sys_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sys_RoleToUser_Sys_UserAccounts_UserID",
                        column: x => x.UserID,
                        principalSchema: "dbo",
                        principalTable: "Sys_UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Sys_Menus",
                columns: new[] { "Id", "CustomData", "Description", "Icon", "IsActive", "MenuDisplayName", "MenuName", "OrderBy", "ParentID", "PermissionName", "RequiresAuthModel", "Url" },
                values: new object[,]
                {
                    { 7L, null, null, "fa-university", true, "组织机构", "sys-org", 6, 1L, "OrgManager", "2", "/Views/SysOrg/Index" },
                    { 1L, null, null, "fa-list-ol", true, "系统设置", "sys-config", 1, null, "", "1", "" },
                    { 2L, null, null, "fa-list-ol", true, "菜单管理", "sys-menus", 1, 1L, "MenusManager", "3", "/Views/SysMenus/Index" },
                    { 3L, null, null, "fa-users", true, "用户管理", "sys-account", 2, 1L, "UserInfoManager", "3", "/Views/SysAccount/index" },
                    { 4L, null, null, "fa-vcard", true, "角色管理", "sys-roles", 3, 1L, "RoleManager", "3", "/Views/SysRole/Index" },
                    { 5L, null, null, "fa-bullhorn", true, "消息通知", "sys-notifications", 5, 1L, "NotificationsManager", "3", "/Views/SysNotifications/Index" },
                    { 6L, null, null, "fa-book", true, "日志管理", "sys-auditlogs", 7, 1L, "LogManager", "3", "/Views/SysAuditLogs/Index" },
                    { 8L, null, null, "fa-bookmark", true, "字典管理", "sys-dict", 7, 1L, "DictManager", "3", "/Views/SysDict/Index" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Sys_NotificationInfo",
                columns: new[] { "Id", "CreationTime", "CreatorUserId", "NotificationDescribe", "NotificationDisplayName", "NotificationName", "NotificationType" },
                values: new object[] { new Guid("0bfb0ddd-bb12-4059-87a1-4e0294643ea4"), new DateTime(2020, 1, 15, 15, 55, 21, 424, DateTimeKind.Local).AddTicks(3913), null, "提供系统默认提示消息", "系统通知", "system", "sms" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Sys_NotificationSubscriptions",
                columns: new[] { "Id", "CreationTime", "CreatorUserId", "EntityId", "EntityTypeAssemblyName", "EntityTypeName", "NotificationName", "TenantId", "UserId" },
                values: new object[,]
                {
                    { new Guid("d9aed633-e9ae-40e6-8ad4-4eec083b03b0"), new DateTime(2020, 1, 15, 15, 55, 21, 424, DateTimeKind.Local).AddTicks(6967), null, null, null, null, "system", 1, 1L },
                    { new Guid("15812862-b2d7-45e2-826e-25545057c334"), new DateTime(2020, 1, 15, 15, 55, 21, 424, DateTimeKind.Local).AddTicks(7887), null, null, null, null, "system", 1, 2L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Sys_RoleToMenuAction",
                columns: new[] { "Id", "IsMenu", "MenuActionID", "MenuID", "RoleID" },
                values: new object[,]
                {
                    { 17L, false, 9L, 4L, 1L },
                    { 18L, false, 10L, 4L, 1L },
                    { 19L, false, 11L, 4L, 1L },
                    { 20L, false, 12L, 4L, 1L },
                    { 21L, false, 13L, 7L, 1L },
                    { 22L, false, 14L, 7L, 1L },
                    { 27L, false, 19L, 8L, 1L },
                    { 24L, false, 16L, 8L, 1L },
                    { 25L, false, 17L, 8L, 1L },
                    { 26L, false, 18L, 8L, 1L },
                    { 16L, false, 8L, 4L, 1L },
                    { 28L, false, 20L, 8L, 1L },
                    { 23L, false, 15L, 7L, 1L },
                    { 15L, false, 7L, 3L, 1L },
                    { 11L, false, 3L, 2L, 1L },
                    { 13L, false, 5L, 3L, 1L },
                    { 1L, true, null, 1L, 1L },
                    { 2L, true, null, 2L, 1L },
                    { 3L, true, null, 3L, 1L },
                    { 4L, true, null, 4L, 1L },
                    { 14L, false, 6L, 3L, 1L },
                    { 6L, true, null, 6L, 1L },
                    { 5L, true, null, 5L, 1L },
                    { 8L, true, null, 8L, 1L },
                    { 9L, false, 1L, 2L, 1L },
                    { 10L, false, 2L, 2L, 1L },
                    { 12L, false, 4L, 3L, 1L },
                    { 7L, true, null, 7L, 1L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Sys_Roles",
                columns: new[] { "Id", "CreationTime", "CreatorUserId", "Description", "IsActive", "LastModificationTime", "LastModifierUserId", "RoleName", "TenantId" },
                values: new object[] { 1L, new DateTime(2020, 1, 15, 15, 55, 21, 404, DateTimeKind.Local).AddTicks(4031), null, "动态生成的角色", true, null, null, "admin角色", null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Sys_Settings",
                columns: new[] { "Id", "CreationTime", "CreatorUserId", "LastModificationTime", "LastModifierUserId", "Name", "TenantId", "UserId", "Value" },
                values: new object[] { new Guid("ecf6faa6-27d6-4056-bb32-91357b639824"), new DateTime(2020, 1, 15, 15, 55, 21, 425, DateTimeKind.Local).AddTicks(40), null, null, null, "Abp.Localization.DefaultLanguageName", null, null, "zh-Hans" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Sys_UserAccounts",
                columns: new[] { "Id", "CreationTime", "CreatorUserId", "DeleterUserId", "DeletionTime", "Description", "EmailAddress", "ImageUrl", "IsActive", "IsAdmin", "IsDeleted", "LastLoginTime", "LastModificationTime", "LastModifierUserId", "OrgCode", "Password", "PhoneNumber", "Sex", "TenantId", "UserCode", "UserNameCn" },
                values: new object[,]
                {
                    { 1L, new DateTime(2020, 1, 15, 15, 55, 21, 405, DateTimeKind.Local).AddTicks(6849), null, null, null, null, null, "m", true, true, false, null, null, null, null, "AQAAAAEAACcQAAAAEEZj3esmovxvE/P0QOhehC+pTI5kwWgw4lpJIUdTy3aJ/vqmujkGkAtLQTV/p2FCBQ==", null, "1", null, "sys", "管理员" },
                    { 2L, new DateTime(2020, 1, 15, 15, 55, 21, 415, DateTimeKind.Local).AddTicks(6895), null, null, null, null, null, "2", true, true, false, null, null, null, null, "AQAAAAEAACcQAAAAEOKiUruIxawStevooYqGq9J0t9qWPy7GY0lKrgZ5Wu2abaHiE/GtCzu/A/MQmcncTA==", null, "0", null, "admin", "管理员" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Sys_MenuAction",
                columns: new[] { "Id", "ActionDisplayName", "ActionName", "Description", "IsActive", "MenuID", "PermissionName", "RequiresAuthModel" },
                values: new object[,]
                {
                    { 1L, "新增", "btnAdd", null, true, 2L, "", "3" },
                    { 19L, "新增字典编码", "btnAddCode", null, true, 8L, "", "3" },
                    { 18L, "保存", "btnSave", null, true, 8L, "DictManager.SaveDict", "3" },
                    { 17L, "删除类型", "btnDelType", null, true, 8L, "DictManager.DelDict", "3" },
                    { 16L, "新增类型", "btnAddType", null, true, 8L, "", "3" },
                    { 15L, "删除", "btnDel", null, true, 7L, "OrgManager.DelSysOrg", "2" },
                    { 14L, "保存", "btnSave", null, true, 7L, "OrgManager.SaveSysOrg", "2" },
                    { 13L, "新增", "btnAdd", null, true, 7L, "", "2" },
                    { 12L, "用户授权", "btnUser", null, true, 4L, "RoleManager.SaveRoleToUser", "3" },
                    { 20L, "删除字典编码", "btnDelCode", null, true, 8L, "DictManager.DeleteDict", "3" },
                    { 11L, "模块授权", "btnMenu", null, true, 4L, "RoleManager.SaveRoleToMenu", "3" },
                    { 9L, "编辑", "BtnEdit", null, true, 4L, "RoleManager.SaveRole", "3" },
                    { 8L, "新增", "btnAdd", null, true, 4L, "RoleManager.SaveRole", "3" },
                    { 7L, "重置密码", "btnResetPass", null, true, 3L, "UserInfoManager.ResetPass", "3" },
                    { 6L, "删除", "btnDel", null, true, 3L, "UserInfoManager.DelUser", "3" },
                    { 5L, "编辑", "btnEdit", null, true, 3L, "UserInfoManager.SaveUser", "3" },
                    { 4L, "新增", "btnAdd", null, true, 3L, "UserInfoManager.SaveUser", "3" },
                    { 3L, "删除", "btnDel", null, true, 2L, "MenusManager.DelMenus", "3" },
                    { 2L, "保存", "btnSave", null, true, 2L, "MenusManager.SaveMenus", "3" },
                    { 10L, "删除", "btnDel", null, true, 4L, "RoleManager.DelRole", "3" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Sys_RoleToUser",
                columns: new[] { "Id", "RoleID", "UserID" },
                values: new object[] { 1L, 1L, 2L });

            migrationBuilder.CreateIndex(
                name: "IX_Sys_MenuAction_MenuID",
                schema: "dbo",
                table: "Sys_MenuAction",
                column: "MenuID");

            migrationBuilder.CreateIndex(
                name: "IX_Sys_RoleToUser_RoleID",
                schema: "dbo",
                table: "Sys_RoleToUser",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Sys_RoleToUser_UserID",
                schema: "dbo",
                table: "Sys_RoleToUser",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_AuditLogs",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_ChatRecord",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_Dict",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_DictType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_MenuAction",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_NotificationInfo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_NotificationsSend",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_NotificationsToTenant",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_NotificationsToUser",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_NotificationSubscriptions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_Org",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_RoleToMenuAction",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_RoleToUser",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_Settings",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_Menus",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_Roles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_UserAccounts",
                schema: "dbo");
        }
    }
}
