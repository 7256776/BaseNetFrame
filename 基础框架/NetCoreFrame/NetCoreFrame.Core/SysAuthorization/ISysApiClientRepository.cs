using Abp.Domain.Repositories;
using System;

namespace NetCoreFrame.Core
{
    public interface ISysApiClientRepository : IRepository<SysApiClient, Guid>
    {
       
    }
}
