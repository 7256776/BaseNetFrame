﻿using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreWorkFlow.Core
{
    public interface ISysWorkFlowDataSourceRepository : IRepository<SysWorkFlowDataSource, Guid>
    {
    }
}
