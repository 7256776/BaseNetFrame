using Abp.EntityFrameworkCore;
using Abp.Notifications;
using Microsoft.EntityFrameworkCore;
using NetCoreFrame.Core;
using System.Data.Common;

namespace NetCoreFrame.Sample
{
    public class SampleFrameDbContext : NetCoreFrameDbContext
    {

        //TODO: Define an IDbSet for each Entity...

        public virtual DbSet<FieldInfo> FieldInfos { set; get; }

        public virtual DbSet<TableInfo> TableInfos { set; get; }
        public virtual DbSet<TempTable> TempTables { set; get; }


        public SampleFrameDbContext(DbContextOptions<SampleFrameDbContext> options)
         : base(options)
        {
        }

        protected SampleFrameDbContext(DbContextOptions options)
           : base(options)
        {

        }

        

    }
}
