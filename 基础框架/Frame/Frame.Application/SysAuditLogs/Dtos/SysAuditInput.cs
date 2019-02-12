using Frame.Core;
using System;
using System.Collections.Generic;

namespace Frame.Application
{
    /// <summary>
    /// 日志
    /// </summary>
    public class SysAuditInput
    {
        /// <summary>
        /// 操作人
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public virtual string ServiceName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public virtual string MethodName { get; set; }
 
        /// <summary>
        /// 日期区间集合
        /// </summary>
        public virtual List<DateTime> ExecutionTime { get; set; }
    
        /// <summary>
        /// 日期区间text
        /// </summary>
        public virtual string DateRange { get; set; }


        /// <summary>
        /// 日期区间text
        /// </summary>
        public virtual PagingDto PagingModel { get; set; }
        

    }
}
