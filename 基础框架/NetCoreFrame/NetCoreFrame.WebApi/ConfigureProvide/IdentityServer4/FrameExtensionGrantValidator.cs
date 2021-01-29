using Abp.Domain.Uow;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using NetCoreFrame.Core;
using System;
using System.Threading.Tasks;

namespace NetCoreFrame.WebApi
{
    /// <summary>
    /// 自定义验证
    /// </summary>
    public class FrameExtensionGrantValidator : IExtensionGrantValidator
    {
        protected SignInManager _sysSignInManager { get; }

        public FrameExtensionGrantValidator(SignInManager sysSignInManager)
        {
            _sysSignInManager = sysSignInManager;
        }

        public string GrantType => "demo_validation";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var userName = context.Request.Raw.GetValues("UserName")[0];
            var password = context.Request.Raw.GetValues("Password")[0];
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient, "请提供授权账号与密码");
                return;
            }

            //授权验证
            SysLoginResult<UserInfo> resLoginUser = await _sysSignInManager.LoginAuth(new UserInfo() { UserCode = userName, Password = password });
            if (resLoginUser != null)
            {
                context.Result = new GrantValidationResult(
                        subject: resLoginUser.User.Id.ToString(),                                   //用户唯一标识 ,用户id 用户表是int因此此处需要采用int类型
                        authenticationMethod: context.Request.GrantType,                //描述自定义授权类型的认证方法 
                        authTime: DateTime.Now,                                                         // 授权时间
                        claims: resLoginUser.Identity.Claims                                         //授权凭证
                    );
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient, "账号 " + userName + " 未获取到有效授权");
            }
        }

    }
}
