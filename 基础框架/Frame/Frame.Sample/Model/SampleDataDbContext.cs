using Abp.EntityFramework;
using Abp.Notifications;
using Frame.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Frame.Sample
{
    public class SampleDataDbContext : DataDbContext
    {

        //TODO: Define an IDbSet for each Entity...

        #region 实体对象数据对象
   
        public virtual DbSet<SysUserAccountsExtens> SysUserAccountsExtenss { set; get; }

        #endregion

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public SampleDataDbContext()
                : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in FrameDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of FrameDbContext since ABP automatically handles it.
         */
        public SampleDataDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public SampleDataDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public SampleDataDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }

      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

 

    }
}
