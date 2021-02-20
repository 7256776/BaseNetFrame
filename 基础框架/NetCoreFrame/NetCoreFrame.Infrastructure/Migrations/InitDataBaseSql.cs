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
            string sql = @"CREATE VIEW [dbo].[V_SysFlowRoleToUser] 
                                                    AS
                                                    SELECT rtu.Id, r.Id FlowRoleId, r.FlowRoleName, r.[Description], 
                                                                ua.Id UserId, ua.UserCode, ua.Sex, ua.UserNameCn, ua.EmailAddress, ua.PhoneNumber,
                                                                so.OrgCode, so.OrgName, so.OrgNode, so.OrgType
                                                    FROM   Sys_WorkFlowRole r 
                                                                    INNER JOIN Sys_WorkFlowRoleToUser rtu ON  r.Id = rtu.FlowRoleID
                                                                    INNER JOIN Sys_UserAccounts ua ON  rtu.UserID = ua.Id
                                                                    LEFT JOIN Sys_Org so ON  ua.OrgCode = so.OrgCode";
            migrationBuilder.Sql(sql);
        }

    }
}
