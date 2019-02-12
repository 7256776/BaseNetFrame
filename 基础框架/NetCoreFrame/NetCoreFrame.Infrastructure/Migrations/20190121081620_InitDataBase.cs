using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreFrame.Infrastructure.Migrations
{
    public partial class InitDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_NotificationInfo",
                keyColumn: "Id",
                keyValue: new Guid("0bfb0ddd-bb12-4059-87a1-4e0294643ea4"),
                column: "CreationTime",
                value: new DateTime(2019, 1, 21, 16, 16, 19, 905, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_NotificationSubscriptions",
                keyColumn: "Id",
                keyValue: new Guid("15812862-b2d7-45e2-826e-25545057c334"),
                column: "CreationTime",
                value: new DateTime(2019, 1, 21, 16, 16, 19, 905, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_NotificationSubscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d9aed633-e9ae-40e6-8ad4-4eec083b03b0"),
                column: "CreationTime",
                value: new DateTime(2019, 1, 21, 16, 16, 19, 905, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationTime",
                value: new DateTime(2019, 1, 21, 16, 16, 19, 886, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_Settings",
                keyColumn: "Id",
                keyValue: new Guid("ecf6faa6-27d6-4056-bb32-91357b639824"),
                column: "CreationTime",
                value: new DateTime(2019, 1, 21, 16, 16, 19, 906, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_UserAccounts",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreationTime", "Password" },
                values: new object[] { new DateTime(2019, 1, 21, 16, 16, 19, 887, DateTimeKind.Local), "AQAAAAEAACcQAAAAEGuH7VVstaT5bGnDYAHQvJvleLu/pZa6MHllA5LyC1XMBqIY5whgeVuxTcFif+aAuw==" });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_UserAccounts",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreationTime", "Password" },
                values: new object[] { new DateTime(2019, 1, 21, 16, 16, 19, 898, DateTimeKind.Local), "AQAAAAEAACcQAAAAELoQZQkmRvSJxK+AZUnpk3Agss64KNiUjDQfJw3QDNK6U0K3sPPO2IC1bXGJcyr6sg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_NotificationInfo",
                keyColumn: "Id",
                keyValue: new Guid("0bfb0ddd-bb12-4059-87a1-4e0294643ea4"),
                column: "CreationTime",
                value: new DateTime(2019, 1, 21, 16, 16, 0, 474, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_NotificationSubscriptions",
                keyColumn: "Id",
                keyValue: new Guid("15812862-b2d7-45e2-826e-25545057c334"),
                column: "CreationTime",
                value: new DateTime(2019, 1, 21, 16, 16, 0, 475, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_NotificationSubscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d9aed633-e9ae-40e6-8ad4-4eec083b03b0"),
                column: "CreationTime",
                value: new DateTime(2019, 1, 21, 16, 16, 0, 475, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationTime",
                value: new DateTime(2019, 1, 21, 16, 16, 0, 456, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_Settings",
                keyColumn: "Id",
                keyValue: new Guid("ecf6faa6-27d6-4056-bb32-91357b639824"),
                column: "CreationTime",
                value: new DateTime(2019, 1, 21, 16, 16, 0, 475, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_UserAccounts",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreationTime", "Password" },
                values: new object[] { new DateTime(2019, 1, 21, 16, 16, 0, 457, DateTimeKind.Local), "AQAAAAEAACcQAAAAEF4hRBip796ldF36oefY9VkCwZcx4mPmvbLiqGY0XpI6lSbYXDSfD1+ELnf2JtdSNA==" });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Sys_UserAccounts",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreationTime", "Password" },
                values: new object[] { new DateTime(2019, 1, 21, 16, 16, 0, 466, DateTimeKind.Local), "AQAAAAEAACcQAAAAEAqBPMFMONfSOzesZU7+zDgMIuQtdyL6rjnbn220b4ICIoUyY4fTtbJ9MGE8EGdWHQ==" });
        }
    }
}
