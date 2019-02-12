using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Frame.Application;
using Frame.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Sample
{
    [AutoMap(typeof(SysUserAccountsExtens))]
    public class SysUserAccountsOut: UserOut
    {
        public virtual string UserNameEx { get; set; }

        public virtual string Job { get; set; }
    }

    /// <summary>
    /// 用户信息表对象
    /// </summary>
    [AutoMap(typeof(SysUserAccountsExtens))]
    public class SysUserAccountsInput
    {
        [StringLength(50)]
        public virtual string UserNameEx { get; set; }

        [StringLength(50)]
        public virtual string Job { get; set; }
    }

    /// <summary>
    /// 用户信息表对象
    /// </summary>

    [Table("SYS_USERACCOUNTS")]
    public class SysUserAccountsExtens : Entity<long>
    //public class SysUserAccountsExtens  : SysUserAccounts
    {
        [Column("UserNameEx")]
        [StringLength(50)]
        public virtual string UserNameEx { get; set; }
         
        [Column("JOB")]
        [StringLength(50)]
        public virtual string Job { get; set; }
    }


}
