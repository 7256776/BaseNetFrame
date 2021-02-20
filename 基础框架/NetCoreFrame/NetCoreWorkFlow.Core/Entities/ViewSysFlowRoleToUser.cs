using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreWorkFlow.Core
{
    /// <summary>
    /// 节点关系集合
    /// </summary>
    [Table("V_SYSFLOWROLETOUSER")]
    public class ViewSysFlowRoleToUser : Entity<Guid>
    {
        /// <summary>
        /// 流程角色ID
        /// </summary>
        [Column("FLOWROLEID")]
        public Guid FlowRoleId { get; set; }

        /// <summary>
        /// 流程角色名称
        /// </summary>
        [Column("FLOWROLENAME")]
        public string FlowRoleName { get; set; }

        /// <summary>
        /// 流程角色描述
        /// </summary>
        [Column("DESCRIPTION")]
        public string Description { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Column("USERID")]
        public string UserId { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        [Column("USERCODE")]
        public string UserCode { get; set; }

        /// <summary>
        /// 性别 1=男 0=女
        /// </summary>
        [Column("SEX")]
        public string Sex { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [Column("USERNAMECN")]
        public string UserNameCn { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Column("EMAILADDRESS")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [Column("PHONENUMBER")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        [Column("ORGCODE")]
        public string OrgCode { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        [Column("ORGNAME")]
        public string OrgName { get; set; }

        /// <summary>
        /// 机构节点
        /// </summary>
        [Column("ORGNODE")]
        public string OrgNode { get; set; }

        /// <summary>
        /// 机构类型
        /// 1=公司 2=部门
        /// </summary>
        [Column("ORGTYPE")]
        public string OrgType { get; set; }


    }
}
