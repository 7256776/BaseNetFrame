using Abp.Collections.Extensions;
using Abp.EntityFrameworkCore;
using Abp.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NetCoreFrame.Core;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace AppFrame.Core
{
    public class AppFrameDbContext : NetCoreFrameDbContext
    {

        //TODO: Define an IDbSet for each Entity...
        public virtual DbSet<AppBuildProjectCategoryConfig> AppBuildProjectCategoryConfigs { set; get; }
        public virtual DbSet<AppBuildProjectCategory> AppBuildProjectCategorys { set; get; }
        public virtual DbSet<AppBuildProjectCategoryToCommodity> AppBuildProjectCategoryByCommoditys { set; get; }
        public virtual DbSet<AppSolution> AppSolutions { set; get; }
        public virtual DbSet<AppFile> AppFiles { set; get; }
        public virtual DbSet<AppFileToBusiness> AppFileToBusinessIds { set; get; }
        public virtual DbSet<AppCommodity> AppCommoditys { set; get; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public AppFrameDbContext(DbContextOptions<AppFrameDbContext> options)
         : base(options)
        {
        }

        protected AppFrameDbContext(DbContextOptions options)
           : base(options)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


     


    }
}
