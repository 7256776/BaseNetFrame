﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreWorkFlow.Core
{
    /// <summary>
    /// 节点关系集合
    /// </summary>
    [Table("SYS_WORKFLOWTYPE")]
    public class SysWorkFlowType : AuditedEntity<Guid>
    {

        /// <summary>
        /// 流程类型
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "请输入流程类型")]
        [Column("FLOWTYPENAME")]
        public string FlowTypeName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(2000)]
        [Column("DESCRIPTION")]
        public string Description { get; set; }

        /// <summary>
        /// 是否只读
        /// </summary>
        [Column("ISREADONLY")]
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
        [Column("ISACTIVE")]
        public bool IsActive { get; set; }



    }
}
