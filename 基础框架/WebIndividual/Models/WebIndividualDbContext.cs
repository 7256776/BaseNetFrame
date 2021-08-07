using Abp.EntityFrameworkCore;
using Abp.Notifications;
using Microsoft.EntityFrameworkCore;
using NetCoreFrame.Core;
using System.Data.Common;

namespace WebIndividual
{
    public class WebIndividualDbContext : NetCoreFrameDbContext
    {
        //TODO: Define an IDbSet for each Entity...
        //public virtual DbSet<FieldInfo> FieldInfos { set; get; }

        public virtual DbSet<WebFile> AppFiles { set; get; }
        public virtual DbSet<WebFileToBusiness> AppFileToBusinessIds { set; get; }
        public virtual DbSet<WebDoc> WebDocs { set; get; }

        public WebIndividualDbContext(DbContextOptions<WebIndividualDbContext> options)
         : base(options)
        {
        }

        protected WebIndividualDbContext(DbContextOptions options)
           : base(options)
        {

        }

    }
}
