using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NetCoreFrame.Core;
using System;
using System.Data.Common;
using System.IO;
using System.Linq;

namespace NetCoreFrame.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class NetCoreFrameInfrastructureConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<InfrastructureDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<InfrastructureDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }

    } 
}


