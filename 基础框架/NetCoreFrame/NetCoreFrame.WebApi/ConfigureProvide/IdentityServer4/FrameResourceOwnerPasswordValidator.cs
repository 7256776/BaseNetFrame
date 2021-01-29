using Abp.Domain.Uow;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;

namespace NetCoreFrame.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class FrameResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {

        private ISysIdentityServerCacheAppService _sysIdentityServerCacheAppService;

        protected SignInManager _sysSignInManager { get; }

        public FrameResourceOwnerPasswordValidator(SignInManager sysSignInManager, ISysIdentityServerCacheAppService sysIdentityServerCacheAppService)
        {
            _sysSignInManager = sysSignInManager;
            _sysIdentityServerCacheAppService = sysIdentityServerCacheAppService;
        }

        /// <summary>
        /// 验证授权信息
        /// 授权方式 passwork 通过账号以及密码验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [UnitOfWork]
        public  Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            //
            if (context.Request == null || context.Request.Secret == null || context.Request.Secret.Credential == null || string.IsNullOrEmpty(context.Request.Secret.Credential.ToString()) || string.IsNullOrEmpty(context.Request.Secret.Id.ToString()))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient, "请提供授权客户与秘钥");
                return Task.CompletedTask;
            }
            //
            if (string.IsNullOrEmpty(context.UserName) || string.IsNullOrEmpty(context.Password))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient, "请提供授权账号与密码");
               return  Task.CompletedTask;
            }
            //获取账号信息
            List<SysApiClienToAccountData> sysApiClienToAccountData = _sysIdentityServerCacheAppService.GetClientAndAccountCache();
            var result = sysApiClienToAccountData.Where(w => w.UserName == context.UserName);
            if (!result.Any())
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient, "账号 " + context.UserName + " 未获取到有效授权");
            }
            //此处扩展密码的解密验证
            string clientSecrets = context.Request.Secret.Credential.ToString();
            string clientId = context.Request.Secret.Id.ToString();
            var sysApiClienToAccount = result.ToList()[0];

            if (sysApiClienToAccount.Password == context.Password && sysApiClienToAccount.ClientId == clientId && sysApiClienToAccount.ClientSecrets == clientSecrets)
            {
                var identity = new ClaimsIdentity();
                identity.AddClaims(new[]{
                                                new Claim("UserName", context.UserName),                   
                                                new Claim("ClientSecrets", clientSecrets),      
                                                new Claim("ClientId", clientId) 
                                                });

                context.Result = new GrantValidationResult(
                      subject: "123456789",//context.UserName,                                    //用户唯一标识 ,用户id 用户表是int因此此处需要采用int类型
                      authenticationMethod: context.Request.GrantType,                      //描述授权类型的认证方法 
                      authTime: DateTime.Now,                                                               // 授权时间
                      claims: identity.Claims
                  );
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient, "账号 " + context.UserName + " 未获取到有效授权");
            }
            return Task.CompletedTask;
        }


    }
}
