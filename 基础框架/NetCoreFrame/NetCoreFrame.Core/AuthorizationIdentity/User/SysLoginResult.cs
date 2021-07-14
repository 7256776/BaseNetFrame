using System.Security.Claims;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 登录返回值
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public class SysLoginResult<TUser>
        where TUser : SysUserAccounts
    {
        /// <summary>
        /// 登录验证状态
        /// </summary>
        public LoginResultType Result { get; private set; }

        /// <summary>
        /// 登录用户对象
        /// </summary>
        public TUser User { get; private set; }

        /// <summary>
        /// 登录用户授权凭证
        /// </summary>
        public ClaimsIdentity Identity { get; private set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get;  set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="user"></param>
        public SysLoginResult(LoginResultType result, TUser user = null)
        {
            Result = result;
            User = user;
        }

        /// <summary>
        /// 默认登录授权通过(构造)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="identity"></param>
        public SysLoginResult(TUser user, ClaimsIdentity identity)
            : this(LoginResultType.Success)
        {
            User = user;
            Identity = identity;
        }
    }


    /// <summary>
    /// 登录验证状态枚举
    /// </summary>
    public enum LoginResultType : byte
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,
         
        /// <summary>
        /// 账号或邮箱地址无效
        /// </summary>
        InvalidUserNameOrEmailAddress = 2,

        /// <summary>
        /// 密码无效
        /// </summary>
        InvalidPassword = 3,

        /// <summary>
        /// 用户未激活
        /// </summary>
        UserIsNotActive = 4,

        /// <summary>
        /// 租户无效
        /// </summary>
        InvalidTenancyName = 5,

        /// <summary>
        /// 租户未激活
        /// </summary>
        TenantIsNotActive = 6,

        /// <summary>
        /// 邮箱验证未通过
        /// </summary>
        UserEmailIsNotConfirmed = 7,

        /// <summary>
        /// 未授权外部登录
        /// </summary>
        UnknownExternalLogin = 8,

        /// <summary>
        /// 已锁定
        /// </summary>
        LockedOut = 9,

        /// <summary>
        /// 电话号码未验证
        /// </summary>
        UserPhoneNumberIsNotConfirmed = 10,

        /// <summary>
        /// 身份验证注册失败
        /// </summary>
        AuthenticationRegistrationFailure = 11,

        /// <summary>
        /// 未获取任何信息
        /// </summary>
        UnacquiredInformation = 12,

        /// <summary>
        /// 账号无效
        /// </summary>
        InvalidUserName = 13
    }


}