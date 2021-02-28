using Abp.EntityFrameworkCore;
using Abp.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCoreFrame.Core;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace NetCoreFrame.Infrastructure
{
    /// <summary>
    /// 审核流程数据库结构
    /// </summary>
    public class WorkFlowDbContext : NetCoreWorkFlowDbContext
    {
        public WorkFlowDbContext(DbContextOptions<WorkFlowDbContext> options)
                : base(options)
        {
        }

        /// <summary>
        /// 全局记录需要执行的脚本数据
        /// </summary>
        public static List<StringBuilder> tabList = new List<StringBuilder>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //不生成表的数据实体对象
            modelBuilder.Ignore<ViewSysFlowRoleToUser>();
            //初始扩展属性的脚本
            DbSqlInit DbSqlInit = new DbSqlInit();
            tabList = DbSqlInit.SetExtendedProperties(modelBuilder);
            this.SetViewSql();
        }

        /// <summary>
        /// 新增视图
        /// </summary>
        public void SetViewSql()
        {
            StringBuilder viewSql = new StringBuilder();
            viewSql.Append("");
            viewSql.Append("CREATE VIEW[dbo].[V_SysFlowRoleToUser] ");
            viewSql.Append("AS ");
            viewSql.Append("SELECT rtu.Id, ");
            viewSql.Append("r.Id FlowRoleId, ");
            viewSql.Append("r.FlowRoleName, ");
            viewSql.Append("r.[Description], ");
            viewSql.Append("CAST(ua.Id AS NVARCHAR(50)) UserId, ");
            viewSql.Append("ua.UserCode, ");
            viewSql.Append("ua.Sex, ");
            viewSql.Append("ua.UserNameCn, ");
            viewSql.Append("ua.EmailAddress, ");
            viewSql.Append("ua.PhoneNumber, ");
            viewSql.Append("so.OrgCode, ");
            viewSql.Append("so.OrgName, ");
            viewSql.Append("so.OrgNode, ");
            viewSql.Append("so.OrgType ");
            viewSql.Append("FROM   Sys_WorkFlowRole r ");
            viewSql.Append("INNER JOIN Sys_WorkFlowRoleToUser rtu ON r.Id = rtu.FlowRoleID ");
            viewSql.Append("INNER JOIN Sys_UserAccounts ua ON rtu.UserID = ua.Id ");
            viewSql.Append("LEFT JOIN Sys_Org so ON ua.OrgCode = so.OrgCode ");

            tabList.Add(viewSql);
        }

    }
}
