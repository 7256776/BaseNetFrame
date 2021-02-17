using Abp.EntityFrameworkCore;
using Abp.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCoreFrame.Core;
using NetCoreWorkFlow.Core;
using System;
using System.Data.Common;
using System.Linq;

namespace NetCoreFrame.Infrastructure
{
    /// <summary>
    /// 审核流程数据库结构
    /// </summary>
    public class WorkFlowDbContext : NetCoreWorkFlowDbContext
    {
        public WorkFlowDbContext(DbContextOptions<WorkFlowDbContext> options)
                : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
