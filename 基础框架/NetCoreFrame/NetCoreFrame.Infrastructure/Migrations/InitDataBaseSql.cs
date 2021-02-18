using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreFrame.Infrastructure.Migrations
{
    public partial class InitDataBaseSql : Migration
    {
        /// <summary>
        /// ToDo可通过该方式执行脚本
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"CREATE VIEW [dbo].[V_SysUserAccount] 
                                    AS

                                    SELECT ua.Id,
                                           ua.UserCode,
                                           ua.UserNameCn,
                                           ua.TenantId,
                                           ua.ImageUrl,
                                           ua.Sex,
                                           ua.EmailAddress,
                                           ua.PhoneNumber,
                                           ua.LastLoginTime,
                                           ua.[Description],
                                           ua.IsActive,
                                           ua.IsAdmin,
                                           so.OrgCode,
                                           so.OrgName,
                                           so.OrgNode,
                                           so.OrgType
                                    FROM   Sys_UserAccounts ua 
			                                    INNER JOIN Sys_Org so ON  ua.OrgCode = so.OrgCode ";
            migrationBuilder.Sql(sql);
        }

    }
}
