using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreFrame.Infrastructure.Migrations.WorkFlowDb
{
    public partial class InitDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Sys_WorkFlowConnection",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WorkFlowSettingID = table.Column<Guid>(nullable: false),
                    SourceId = table.Column<string>(maxLength: 50, nullable: false),
                    TargetId = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_WorkFlowConnection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_WorkFlowEndpoint",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WorkFlowSettingID = table.Column<Guid>(maxLength: 50, nullable: false),
                    UID = table.Column<string>(maxLength: 50, nullable: false),
                    EndpointText = table.Column<string>(maxLength: 50, nullable: false),
                    EndpointType = table.Column<string>(maxLength: 20, nullable: false),
                    OffsetTop = table.Column<int>(nullable: false),
                    OffsetLeft = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_WorkFlowEndpoint", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_WorkFlowRole",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    FlowRoleName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_WorkFlowRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_WorkFlowRoleToUser",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FlowRoleID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_WorkFlowRoleToUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_WorkFlowSetting",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    WorkFlowName = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_WorkFlowSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_WorkFlowType",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    FlowTypeName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    IsReadOnly = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_WorkFlowType", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_WorkFlowConnection",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_WorkFlowEndpoint",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_WorkFlowRole",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_WorkFlowRoleToUser",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_WorkFlowSetting",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sys_WorkFlowType",
                schema: "dbo");
        }
    }
}
