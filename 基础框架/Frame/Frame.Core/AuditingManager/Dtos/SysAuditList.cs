using System;

namespace Frame.Core
{
    /// <summary>
    /// 日志对象
    /// </summary>
    public class SysAuditList 
    {
        public virtual int? Id { get; set; }

        /// <summary>
        /// 租户id.
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// 用户id.
        /// </summary>
        public virtual long? UserId { get; set; }
        
        /// <summary>
        /// 服务(类/接口)的名字
        /// </summary>
        public virtual string ServiceName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public virtual string MethodName { get; set; }

        /// <summary>
        /// 参数.
        /// </summary>
        public virtual string Parameters { get; set; }

        /// <summary>
        /// 方法执行的开始时间
        /// </summary>
        public virtual DateTime ExecutionTime { get; set; }
        
        /// <summary>
        /// 调用时长.
        /// </summary>
        public virtual int ExecutionDuration { get; set; }

        /// <summary>
        /// IP 地址
        /// </summary>
        public virtual string ClientIpAddress { get; set; }

        /// <summary>
        /// 客户端名称.(一般计算机名称)
        /// </summary>
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 浏览器
        /// </summary>
        public virtual string BrowserInfo { get; set; }

        /// <summary>
        /// 异常对象.
        /// </summary>
        public virtual string Exception { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public virtual long? ImpersonatorUserId { get; set; }

        /// <summary>
        /// 租户id
        /// </summary>
        public virtual int? ImpersonatorTenantId { get; set; }

        /// <summary>
        /// 自定义数据
        /// <see cref="AuditInfo.CustomData"/>.
        /// </summary>
        public virtual string CustomData { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public virtual string UserNameCn { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public virtual string UserCode { get; set; }


    }
}