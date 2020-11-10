using Abp.Domain.Repositories;
using System;

namespace NetCoreFrame.Core
{
    public interface ISysApiResourceRepository : IRepository<SysApiResource, Guid>
    {
       
    }
}
