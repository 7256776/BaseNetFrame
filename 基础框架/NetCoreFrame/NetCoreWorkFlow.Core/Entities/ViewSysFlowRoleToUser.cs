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
    [Table("V_SysFlowRoleToUser")]
    public class ViewSysFlowRoleToUser : Entity<Guid>
    {
        /// <summary>
        /// 流程角色ID
        /// </summary>
        [Column("FlowRoleId")]
        public Guid FlowRoleId { get; set; }

        /// <summary>
        /// 流程角色名称
        /// </summary>
        [Column("FlowRoleName")]
        public string FlowRoleName { get; set; }

        /// <summary>
        /// 流程角色描述
        /// </summary>
        [Column("Description")]
        public string Description { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Column("UserId")]
        public string UserId { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        [Column("UserCode")]
        public string UserCode { get; set; }

        /// <summary>
        /// 性别 1=男 0=女
        /// </summary>
        [Column("Sex")]
        public string Sex { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [Column("UserNameCn")]
        public string UserNameCn { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Column("EmailAddress")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        [Column("OrgCode")]
        public string OrgCode { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        [Column("OrgName")]
        public string OrgName { get; set; }

        /// <summary>
        /// 机构节点
        /// </summary>
        [Column("OrgNode")]
        public string OrgNode { get; set; }

        /// <summary>
        /// 机构类型
        /// 1=公司 2=部门
        /// </summary>
        [Column("OrgType")]
        public string OrgType { get; set; }


    }
}
