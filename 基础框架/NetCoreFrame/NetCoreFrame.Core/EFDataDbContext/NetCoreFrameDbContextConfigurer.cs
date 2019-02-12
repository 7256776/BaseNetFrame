using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace NetCoreFrame.Core
{
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
