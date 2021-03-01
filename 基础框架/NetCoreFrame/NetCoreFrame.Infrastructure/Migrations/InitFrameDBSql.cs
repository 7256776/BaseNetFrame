using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreFrame.Infrastructure.Migrations
{
    public partial class InitFrameDBSql : Migration
    {
        /// <summary>
        /// ToDo可通过该方式执行脚本
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //执行sql脚本
            foreach (var item in FrameDbContext.tabList)
            {
                migrationBuilder.Sql(item.ToString());
            }
        }


    }
}
