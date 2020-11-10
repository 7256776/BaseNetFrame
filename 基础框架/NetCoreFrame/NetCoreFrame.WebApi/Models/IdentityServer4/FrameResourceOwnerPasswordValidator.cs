using Abp.Domain.Uow;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using NetCoreFrame.Core;
using System;
using System.Threading.Tasks;

namespace NetCoreFrame.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class FrameResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {

        protected SignInManager _sysSignInManager { get; }

        public FrameResourceOwnerPasswordValidator(SignInManager sysSignInManager)
        {
            _sysSignInManager = sysSignInManager;
        }

        /// <summary>
        /// 验证授权信息
        /// 授权方式 passwork 通过账号以及密码验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            string secret = context.Request.Secret.Credential.ToString();
            string clientId = context.Request.Secret.Id;
            if (string.IsNullOrEmpty(context.UserName) || string.IsNullOrEmpty(context.Password))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient, "请提供授权账号与密码");
                return;
            }
            //授权验证
            SysLoginResult<UserInfo> resLoginUser = await _sysSignInManager.LoginAuth(new UserInfo() { UserCode = context.UserName, Password = context.Password });
            if (resLoginUser != null)
            {
                context.Result = new GrantValidationResult(
                        subject: resLoginUser.User.Id.ToString(),                                   //用户唯一标识 ,用户id
                        authenticationMethod: context.Request.GrantType,                 //描述自定义授权类型的认证方法 
                        authTime: DateTime.Now,                                                         // 授权时间
                        claims: resLoginUser.Identity.Claims                                         //授权凭证
                    );
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient, "账号 " + context.UserName + " 未获取到有效授权");
            }
        }


    }
}
