using EntityFramework.DynamicFilters;
using Frame.Core;
using Frame.Infrastructure.Migrations.SeedData;
using System.Data.Entity.Migrations;

namespace Frame.Infrastructure
{
    public sealed class Configuration : DbMigrationsConfiguration<DataDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "FrameLibrary";
            //�����MySql���ݿ����� 
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(DataDbContext context)
        {
            context.DisableAllFilters();
            new InitialDbBuilder(context).Create();
            context.SaveChanges();
        }
    }
}
