using Abp.AutoMapper;
using NetCoreFrame.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace NetCoreFrame.Application
{
    [AutoMap(typeof(SysOrg))]
    public class SysOrgInput
    {
        /// <summary>
        /// ID
        /// </summary>	
        public Guid? Id { get; set; }

        /// <summary>
        /// 上级节点主键
        /// </summary>
        public virtual Guid? ParentOrgID { get; set; }

        /// <summary>
        /// 组织机构编码
        /// </summary>
        [Required(ErrorMessage = "请输入组织机构编码")]
        [StringLength(100, ErrorMessage = "组织机构编码长度超过100")]
        public virtual string OrgCode { get; set; }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        [Required(ErrorMessage = "请输入组织机构名称")]
        [StringLength(100, ErrorMessage = "组织机构名称长度超过100")]
        public virtual string OrgName { get; set; }

        /// <summary>
        /// 组织机构类型 
        /// </summary>
        [Required(ErrorMessage = "请输入通知类型名称")]
        public virtual string OrgType { get; set; }

        /// <summary>
        /// 组织机构是否启用
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int? OrderBy { get; set; }

    }
}
