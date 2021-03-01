using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NetCoreFrame.Core;
using System;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;

namespace NetCoreFrame.Infrastructure
{
    /// <summary>
    /// 数据库注入配置信息
    /// </summary>
    public class DbContextConfigurer<TDbContext> where TDbContext : AbpDbContext
    {
        /// <summary>
        ///  注入配置信息
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="options"></param>
        public static void Configure(AbpDbContextConfiguration<TDbContext> options)   
        {
            if (options.ExistingConnection != null)
            {
                options.DbContextOptions.UseSqlServer(options.ExistingConnection);
            }
            else
            {
                options.DbContextOptions.UseSqlServer(options.ConnectionString);
            }
        }



    }
}


