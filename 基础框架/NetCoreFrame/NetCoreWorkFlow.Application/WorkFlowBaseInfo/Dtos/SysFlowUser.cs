using Abp.AutoMapper;
using System;

namespace NetCoreWorkFlow.Application
{
    /// <summary>
    /// 用户对象信息
    /// </summary>
    public class SysFlowUser
    {

        /// <summary>
        /// 用户主键ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public virtual string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>		
        public virtual string UserNameCn { get; set; }

        /// <summary>
        /// 性别
        /// </summary>		
        public virtual string Sex { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>		
        public virtual string EmailAddress { get; set; }

        /// <summary>
        /// 电话
        /// </summary>		
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// 是否超级管理员
        /// </summary>		
        public virtual bool IsAdmin { get; set; } = false;

        /// <summary>
        /// 所属部门
        /// </summary>		
        public string OrgCode { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }

        /// <summary>
        /// 机构类型
        /// 1=公司
        /// 2=部门
        /// </summary>
        public string OrgType { get; set; }

        /// <summary>
        /// 机构节点
        /// </summary>
        public string OrgNode { get; set; }

        
        
           

    }
}
