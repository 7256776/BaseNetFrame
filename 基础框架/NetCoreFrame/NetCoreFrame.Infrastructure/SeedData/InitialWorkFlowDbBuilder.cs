using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NetCoreFrame.Core;
using System;
using System.IO;
using System.Linq;

namespace NetCoreFrame.Infrastructure
{

    public class InitialWorkFlowDbBuilder : IDesignTimeDbContextFactory<WorkFlowDbContext>
    {
        /// <summary>
        ///   执行命令
        ///   Add-Migration InitDataBase -c WorkFlowDbContext       带入参数 数据库连接上下文对象名称
        ///   Update-Database -Verbose -c WorkFlowDbContext
        ///   
        ///   1. Add-Migration InitDataBase                init生成的类名称后缀,前缀是日期表示 (如需撤销该操作 Remove-Migration)
        ///   2. Update-Database -Verbose
        ///   
        ///  get-help Add-Migration -detailed 查看详细信息
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


