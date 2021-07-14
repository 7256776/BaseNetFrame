using System.Data.Common;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 注册数据库连接对象
    /// </summary>
    public static class NetCoreFrameDbContextConfigurer<TDbContext> where TDbContext : AbpDbContext
    {
        //public static void Configure(DbContextOptionsBuilder<TDbContext> builder, string connectionString)
        //{
        //    builder.UseSqlServer(connectionString);
        //}

        //public static void Configure(DbContextOptionsBuilder<TDbContext> builder, DbConnection connection)
        //{
        //    builder.UseSqlServer(connection);
        //}

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
