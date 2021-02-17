using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace NetCoreWorkFlow.Core
{
    /// <summary>
    /// ע�����ݿ����Ӷ���
    /// </summary>
    public static class NetCoreWorkFlowDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<NetCoreWorkFlowDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<NetCoreWorkFlowDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
