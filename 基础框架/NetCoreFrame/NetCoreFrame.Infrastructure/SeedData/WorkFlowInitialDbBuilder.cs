using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NetCoreFrame.Core;
using System;
using System.IO;
using System.Linq;

namespace NetCoreFrame.Infrastructure
{

    public class WorkFlowInitialDbBuilder : IDesignTimeDbContextFactory<WorkFlowDbContext>
    {
        /// <summary>
        ///   执行命令
        ///   Add-Migration 'InitDataBase' -o 'Migrations/WorkFlowDb' -c WorkFlowDbContext 
        ///   Update-Database -Verbose -c WorkFlowDbContext
        ///   init生成的类名称后缀,前缀是日期表示 (如需撤销该操作 Remove-Migration)
        ///   get-help Add-Migration -detailed 查看详细信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public WorkFlowDbContext CreateDbContext(string[] args)
        {
            //根据项目结构查询的web目录的配置文件,如果项目目录结构发生变化需要调整
            var builder = new DbContextOptionsBuilder<WorkFlowDbContext>();
            //此处获取的是默认配置的数据库配置信息
            var configuration = AppConfigurations.Get(Environment.CurrentDirectory);
            builder.UseSqlServer(configuration.GetConnectionString(ConstantConfig.DBDefaultName));
            return new WorkFlowDbContext(builder.Options);
        }

         

    }
}


