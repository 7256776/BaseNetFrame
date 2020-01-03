using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 注册数据库连接对象
    /// </summary>
    public static class NetCoreFrameDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<NetCoreFrameDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<NetCoreFrameDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
