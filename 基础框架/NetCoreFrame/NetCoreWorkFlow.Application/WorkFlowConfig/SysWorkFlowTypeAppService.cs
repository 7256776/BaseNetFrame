using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Web.Models;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWorkFlow.Application
{
    public class SysWorkFlowTypeAppService : NetCoreWorkFlowApplicationBase, ISysWorkFlowTypeAppService
    {
        private readonly ISysWorkFlowTypeRepository _sysWorkFlowTypeRepository;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysWorkFlowTypeAppService(
            ISysWorkFlowTypeRepository sysWorkFlowTypeRepository
            )
        {
            _sysWorkFlowTypeRepository = sysWorkFlowTypeRepository;

        }


    }
}
