using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreFrame.Infrastructure.Migrations.FrameDb
{
    public partial class InitDataBase_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_NotificationInfo",
                keyColumn: "Id",
                keyValue: new Guid("0bfb0ddd-bb12-4059-87a1-4e0294643ea4"),
                column: "CreationTime",
                value: new DateTime(2021, 3, 31, 21, 10, 32, 75, DateTimeKind.Local).AddTicks(9966));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_NotificationSubscriptions",
                keyColumn: "Id",
                keyValue: new Guid("15812862-b2d7-45e2-826e-25545057c334"),
                column: "CreationTime",
                value: new DateTime(2021, 3, 31, 21, 10, 32, 76, DateTimeKind.Local).AddTicks(3893));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_NotificationSubscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d9aed633-e9ae-40e6-8ad4-4eec083b03b0"),
                column: "CreationTime",
                value: new DateTime(2021, 3, 31, 21, 10, 32, 76, DateTimeKind.Local).AddTicks(2913));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationTime",
                value: new DateTime(2021, 3, 31, 21, 10, 32, 69, DateTimeKind.Local).AddTicks(686));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_Setting",
                keyColumn: "Id",
                keyValue: new Guid("ecf6faa6-27d6-4056-bb32-91357b639824"),
                column: "CreationTime",
                value: new DateTime(2021, 3, 31, 21, 10, 32, 76, DateTimeKind.Local).AddTicks(6029));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_UserAccounts",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreationTime", "Password" },
                values: new object[] { new DateTime(2021, 3, 31, 21, 10, 32, 69, DateTimeKind.Local).AddTicks(6811), "AQAAAAEAACcQAAAAENW/McWYou7Mn9vZeGnGr955cmZBuTT4tkFHhe3D7dkzQyh+9Kz9FWhAlV/1mY9HGQ==" });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_UserAccounts",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreationTime", "Password" },
                values: new object[] { new DateTime(2021, 3, 31, 21, 10, 32, 74, DateTimeKind.Local).AddTicks(1412), "AQAAAAEAACcQAAAAEPOVRBVv2XyBcCoPHB6tSsEw0JFBd6YoNRHExqqPMmDjFf01GMdU9jMoJUJyboLZrg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_NotificationInfo",
                keyColumn: "Id",
                keyValue: new Guid("0bfb0ddd-bb12-4059-87a1-4e0294643ea4"),
                column: "CreationTime",
                value: new DateTime(2021, 3, 31, 20, 28, 51, 89, DateTimeKind.Local).AddTicks(1800));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_NotificationSubscriptions",
                keyColumn: "Id",
                keyValue: new Guid("15812862-b2d7-45e2-826e-25545057c334"),
                column: "CreationTime",
                value: new DateTime(2021, 3, 31, 20, 28, 51, 89, DateTimeKind.Local).AddTicks(6894));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_NotificationSubscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d9aed633-e9ae-40e6-8ad4-4eec083b03b0"),
                column: "CreationTime",
                value: new DateTime(2021, 3, 31, 20, 28, 51, 89, DateTimeKind.Local).AddTicks(5728));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationTime",
                value: new DateTime(2021, 3, 31, 20, 28, 51, 80, DateTimeKind.Local).AddTicks(8469));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_Setting",
                keyColumn: "Id",
                keyValue: new Guid("ecf6faa6-27d6-4056-bb32-91357b639824"),
                column: "CreationTime",
                value: new DateTime(2021, 3, 31, 20, 28, 51, 89, DateTimeKind.Local).AddTicks(9305));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_UserAccounts",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreationTime", "Password" },
                values: new object[] { new DateTime(2021, 3, 31, 20, 28, 51, 81, DateTimeKind.Local).AddTicks(4560), "AQAAAAEAACcQAAAAEL4Jvdbmz2uKeI7blzH90zphO6Ej86+WwhTSgUTh5XCf3Uv5FetLJBl+qNcLOgEVAw==" });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_UserAccounts",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreationTime", "Password" },
                values: new object[] { new DateTime(2021, 3, 31, 20, 28, 51, 87, DateTimeKind.Local).AddTicks(208), "AQAAAAEAACcQAAAAEAvAFT2taVzbRxD/Nv0A8T1xtWThUKXhlnulkg8IWPuUvg5q3SR/HZQuktKqec0yvA==" });
        }
    }
}
