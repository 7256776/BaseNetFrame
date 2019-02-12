using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Runtime.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetCoreFrame.Core
{
    public class SysUserClaimsPrincipalFactory<TUser> : UserClaimsPrincipalFactory<TUser>, ITransientDependency
        where TUser : SysUserInfo<TUser>
    {
        public SysUserClaimsPrincipalFactory(
            SysUserInfoManager<TUser> userManager,
            IOptions<IdentityOptions> optionsAccessor
            ) : base(userManager, optionsAccessor)
        {

        }

        /// <summary>
        /// ������Ȩƾ֤Ʊ��
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override async  Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            #region 
            //var identity = new ClaimsIdentity(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);
            //identity.AddClaim(new Claim("UserNameCn", user.UserNameCn));
            //identity.AddClaim(new Claim("UserCode", user.UserCode));
            //identity.AddClaim(new Claim("IsAdmin", user.IsAdmin.ToString()));
            ////identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            ////�û������Ľ�ɫID
            //if (user.SysRoleToUserList != null && user.SysRoleToUserList.Any())
            //{
            //    List<long> roleList = new List<long>();
            //    foreach (var item in user.SysRoleToUserList)
            //        roleList.Add(item.RoleID);
            //    string strList = string.Join(",", roleList.ToArray());
            //    identity.AddClaim(new Claim("UserRoleList", strList));
            //}
            //ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            //return Task.FromResult(principal); 
            #endregion

            ClaimsPrincipal principal = await base.CreateAsync(user);

            #region ��Ȩƾ֤����Զ��������

            principal.Identities.First().AddClaims(new[]{
                                                new Claim("UserNameCn", user.UserNameCn),       //�û�����
                                                new Claim("UserCode", user.UserCode),                  //�û��˺�
                                                new Claim("IsAdmin", user.IsAdmin.ToString())        //�Ƿ񳬼�����Ա
                                                });

            //�û������Ľ�ɫID
            if (user.SysRoleToUserList != null && user.SysRoleToUserList.Any())
            {
                List<long> roleList = new List<long>();
                foreach (var item in user.SysRoleToUserList)
                    roleList.Add(item.RoleID);
                string strList = string.Join(",", roleList.ToArray());
                principal.Identities.First().AddClaim(new Claim("UserRoleList", strList));
            }
            #endregion
            return principal;
        }


    }
}