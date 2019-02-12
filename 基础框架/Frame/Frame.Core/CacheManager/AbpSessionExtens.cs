using Abp.Configuration.Startup;
using Abp.MultiTenancy;
using Abp.Runtime;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frame.Core
{
    /// <summary>
    /// ABPSession扩展的具体实现
    /// </summary>
    public class AbpSessionExtens : ClaimsAbpSession, IAbpSessionExtens
    {
        private readonly ICacheManagerExtens _cacheManagerExtens;

        public AbpSessionExtens(
            IPrincipalAccessor principalAccessor, 
            IMultiTenancyConfig multiTenancy, 
            ITenantResolver tenantResolver, 
            IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider,
            ICacheManagerExtens cacheManagerExtens)
           : base(principalAccessor, multiTenancy, tenantResolver, sessionOverrideScopeProvider)
         {
            _cacheManagerExtens = cacheManagerExtens;
        }

        public string UserNameCn => GetClaimUserNameValue("UserNameCn");
        public string UserCode => GetClaimValue("UserCode");
        public bool IsAdmin => GetClaimBoolValue("IsAdmin");
        public List<string> UserRoleList => GetClaimUserRoleListValue("UserRoleList");

        /// <summary>
        /// 扩展的用户授权信息
        /// </summary>
        public string UserAuthInfoExt => GetClaimValue("UserAuthInfoExt");

        //string IAbpSessionExtens.UserNameCn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //private List<string> userRoleList ;
        //public List<string> UserRoleList
        //{
        //    get { return GetClaimListValue("UserRoleList"); ; }
        //    set
        //    {
        //        userRoleList = value;
        //    }
        //}


        /// <summary>
        /// 获取自定义属性值 String
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns></returns>
        private string GetClaimValue(string claimType)
        { 
            var claimsPrincipal = PrincipalAccessor.Principal;
            var claim = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == claimType);
            if (string.IsNullOrEmpty(claim?.Value))
                return null;
            return claim.Value;
        }

        /// <summary>
        /// 获取自定义属性值 bool
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns></returns>
        private bool GetClaimBoolValue(string claimType)
        {
            string res = GetClaimValue(claimType);
            //return bool.Parse(res);
            return Convert.ToBoolean(res);
        }

        /// <summary>
        /// 获取自定义属性值 String[]
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns></returns>
        private List<string> GetClaimListValue(string claimType)
        {
            string roleList = GetClaimValue(claimType);
            if (string.IsNullOrEmpty(roleList))
                return new List<string>();
            return roleList.Split(',').ToList();
        }


        /// <summary>
        /// 由于用户姓名是可变的因此优先从缓存中获取
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns></returns>
        private string GetClaimUserNameValue(string claimType)
        {
            if (base.UserId != null)
            {
                var user = _cacheManagerExtens.GetUserInfoCache(base.UserId.Value);
                return user.UserNameCn;
            }

            return GetClaimValue(claimType);
        }

        /// <summary>
        /// 由于用户角色是可变的因此优先从缓存中获取
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns></returns>
        private List<string> GetClaimUserRoleListValue(string claimType)
        {
            if (base.UserId != null)
            {
                var user = _cacheManagerExtens.GetUserInfoCache(base.UserId.Value);
                return user.SysRoleToUserList.Select(s => s.RoleID.ToString()).ToList();
            }

            string roleList = GetClaimValue(claimType);
            if (string.IsNullOrEmpty(roleList))
                return new List<string>();
            return roleList.Split(',').ToList();
        }


    }
}
