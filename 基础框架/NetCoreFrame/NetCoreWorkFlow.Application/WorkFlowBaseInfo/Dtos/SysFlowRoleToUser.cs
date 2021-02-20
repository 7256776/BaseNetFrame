using Abp.AutoMapper;
using System;

namespace NetCoreWorkFlow.Application
{
    public class SysFlowRoleToUser
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
        /// 审核角色名称
        /// </summary>		
        public string FlowRoleName { get; set; }

        /// <summary>
        /// 审核角色ID
        /// </summary>		
        public Guid FlowRoleId { get; set; }
        
        /// <summary>
        /// 组织机构节点
        /// </summary>
        public virtual string OrgNode { get; set; }

        /// <summary>
        /// 组织机构类型 
        /// 公司 = 1;  部门 = 2
        /// </summary>
        public virtual string OrgType { get; set; }
    }
}
