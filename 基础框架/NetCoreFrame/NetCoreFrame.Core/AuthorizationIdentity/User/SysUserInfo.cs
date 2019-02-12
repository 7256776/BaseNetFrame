//using Microsoft.AspNet.Identity;

using Abp.Domain.Entities.Auditing;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 重构用户信息对象
    /// </summary>
    public abstract class SysUserInfo<TUser> : SysUserAccounts//, IUser<long> //, IFullAudited<TUser>
    where TUser : SysUserInfo<TUser>
    {
        protected SysUserInfo()
        {
        }


        //取消该字段,不生成外键关系
        //public virtual TUser DeleterUser { get; set; }
        //public virtual TUser CreatorUser { get; set; }
        //public virtual TUser LastModifierUser { get; set; }


        //long IUser<long>.Id
        //{
        //    get
        //    {
        //        return this.Id;
        //    }
        //}

        //string IUser<long>.UserName
        //{
        //    get
        //    {
        //        return this.UserCode;
        //    }

        //    set
        //    {
        //        this.UserCode = value;
        //    }
        //}

    }


}
