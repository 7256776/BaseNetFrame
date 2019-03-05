using AspectCore.DynamicProxy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAuthService
{
    [NonAspect]
    public class AppDbContext : DbContext
    {
       
        public virtual DbSet<SysAuditLog> SysAuditLogs { set; get; }

      
        public virtual DbSet<SysDict> SysDicts { set; get; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //public AppDbContext(DbContextOptions options) : base(options)
        //{

        //}



        //public AppDbContext() : base()
        //{

        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //配置mariadb连接字符串
        //    optionsBuilder.UseSqlServer("Server=.; Database=FrameDB; user id=sa;pwd=sa;");
        //}


    }
}
