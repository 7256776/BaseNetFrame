using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NetCoreFrame.Core;
using System;
using System.IO;
using System.Linq;

namespace NetCoreFrame.Infrastructure
{

    public class FrameInitialDbBuilder : IDesignTimeDbContextFactory<FrameDbContext>
    {
        /// <summary>
        ///   执行命令
        ///   Add-Migration [-Name] <String> [-OutputDir <String>] [-Context <String>] [-Project <String>] [-StartupProject <String>] [<CommonParameters>]  
        ///   Add-Migration 'InitDataBase' -o 'Migrations/FrameDb' -c FrameDbContext 
        ///   Update-Database -Verbose -c FrameDbContext
        ///   
        ///   init生成的类名称后缀,前缀是日期表示 (如需撤销该操作 Remove-Migration)
        ///   get-help Add-Migration -detailed 查看详细信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public FrameDbContext CreateDbContext(string[] args)
        {
            //根据项目结构查询的web目录的配置文件,如果项目目录结构发生变化需要调整
            var builder = new DbContextOptionsBuilder<FrameDbContext>();
            //此处获取的是默认配置的数据库配置信息
            var configuration = AppConfigurations.Get(Environment.CurrentDirectory);
            //Console.WriteLine("可以打印执行消息");
            //var configuration = AppConfigurations.GetConfigurationCache();

            builder.UseSqlServer(configuration.GetConnectionString(ConstantConfig.DBDefaultName));
            //builder.UseSqlServer("Server=.; Database=FrameDB_a; user id=sa;pwd=sa;");
         
            return new FrameDbContext(builder.Options);
        }

         

    }
}


