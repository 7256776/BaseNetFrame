using Abp;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Localization;
using Abp.Runtime.Session;
using System.Threading.Tasks;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 构造授权验证
    /// 注: 需要继承接口IIocManagerAccessor 用于处理当前上下文中获取授权实现过程中能够正常进行注入IocManager(否则授权验证过程中获取错误信息时无法正常进行本地化转义)
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public abstract class PermissionCheckerCore<TUser> : IPermissionChecker, ITransientDependency, IIocManagerAccessor
            where TUser : SysUserInfo<TUser>
    {

        //public IIocManager IocManager { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private readonly UserInfoManager _userInfoManager;

        private readonly ILocalizationManager _localizationManager;

        public IAbpSession AbpSession { get; set; }

        /// <summary>
        /// 依赖注入管理
        /// </summary>
        public IIocManager IocManager { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected PermissionCheckerCore(UserInfoManager userInfoManager, ILocalizationManager localizationManager)
        {
            //
            _userInfoManager = userInfoManager;
            _localizationManager = localizationManager;
            AbpSession = NullAbpSession.Instance;

        }

        /// <summary>
        /// 验证授权(当前登录用户)
        /// </summary>
        /// <param name="permissionName">授权名称</param>
        /// <returns></returns>
        public async Task<bool> IsGrantedAsync(string permissionName)
        {
            var isGranted = await _userInfoManager.IsGrantedAsync(permissionName);
            //if (!isGranted)
            //{
            //    该方法是为了转义提示消息的本地化,由于该方法会在启动时候调用,导致抛出异常中断后续代码因此取消该转义
            //    AuthorizationException(permissionName);
            //}
            return isGranted;
        }


        public bool IsGranted(string permissionName)
        {
            bool isGranted = Task.FromResult(_userInfoManager.IsGrantedAsync(permissionName)).Result.Result;
            return isGranted;
        }

        /// <summary>
        /// 验证授权(指定用户)
        /// </summary>
        /// <param name="user">指定用户对象</param>
        /// <param name="permissionName">授权名称</param>
        /// <returns></returns>
        public async Task<bool> IsGrantedAsync(UserIdentifier user, string permissionName)
        {
            bool isGranted = await _userInfoManager.IsGrantedAsync(user, permissionName);
            return isGranted;
        }

        public bool IsGranted(UserIdentifier user, string permissionName)
        {
            bool isGranted = Task.FromResult(_userInfoManager.IsGrantedAsync(user, permissionName)).Result.Result;
            return isGranted;
        }

        /// <summary>
        /// 获取abp系统默认本地化信息
        /// ToFix
        /// 通过 继承IIocManagerAccessor处理,留作备用
        /// </summary>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        public string AuthorizationException(string permissionName)
        {
            throw new AbpAuthorizationException(
                string.Format(
                        _localizationManager.GetString(AbpConsts.LocalizationSourceName, "AllOfThesePermissionsMustBeGranted"),
                        string.Join(", ", permissionName)
                    )
                );
        }



    }
}
