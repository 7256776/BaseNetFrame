using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NetCoreFrame.Core;
using System;
using System.IO;
using System.Linq;

namespace NetCoreFrame.Infrastructure
{

    public class InitialDbBuilder : IDesignTimeDbContextFactory<InfrastructureDbContext>
    {
        /// <summary>
        ///   执行命令
        ///   1. Add-Migration init                 (如需撤销该操作 Remove-Migration)
        ///   2. Add-Migration InitDataBase
        ///   3. Update-Database -Verbose
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public InfrastructureDbContext CreateDbContext(string[] args)
        {
            //根据项目结构查询的web目录的配置文件,如果项目目录结构发生变化需要调整
            var builder = new DbContextOptionsBuilder<InfrastructureDbContext>();
            //此处获取的是默认配置的数据库配置信息
            var configuration = AppConfigurations.Get(Environment.CurrentDirectory);
            //Console.WriteLine("可以打印执行消息");
            //var configuration = AppConfigurations.GetConfigurationCache();

            builder.UseSqlServer(configuration.GetConnectionString(ConstantConfig.DBDefaultName));
            //builder.UseSqlServer("Server=.; Database=FrameDB_a; user id=sa;pwd=sa;");

            return new InfrastructureDbContext(builder.Options);
        }

    }
}


