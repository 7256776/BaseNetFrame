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
        ///   Add-Migration init
        ///   Add-Migration InitDataBase
        ///   Update-Database -Verbose
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public InfrastructureDbContext CreateDbContext(string[] args)
        {
            //根据项目结构查询的web目录的配置文件,如果项目目录结构发生变化需要调整
            var builder = new DbContextOptionsBuilder<InfrastructureDbContext>();

            var configuration = AppConfigurations.Get(Environment.CurrentDirectory);
            //var configuration = AppConfigurations.GetConfigurationCache();

            builder.UseSqlServer(configuration.GetConnectionString(ConstantConfig.DBDefaultName));
            //builder.UseSqlServer("Server=.; Database=FrameDB_a; user id=sa;pwd=sa;");

            return new InfrastructureDbContext(builder.Options);
        }

    }
}


