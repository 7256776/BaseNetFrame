using EntityFramework.DynamicFilters;
using Frame.Core;

namespace Frame.Infrastructure.Migrations.SeedData
{
    public class InitialDbBuilder
    {
        private readonly DataDbContext _context;

        public InitialDbBuilder(DataDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            /*
             执行命令
             add-migration 20180329001
             update-database -Verbose
             */
            //Migration jdb
            _context.DisableAllFilters();
            //创建默认数据库
            new DefaultFrameDB(_context).Create();
        }
    }
}


