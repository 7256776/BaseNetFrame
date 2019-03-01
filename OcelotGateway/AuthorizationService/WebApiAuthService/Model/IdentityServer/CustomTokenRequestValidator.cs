using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAuthService
{
    /// <summary>
    /// 定制令牌端点处的请求参数验证。
    /// </summary>
    public class CustomTokenRequestValidator : ICustomTokenRequestValidator
    {
        public CustomTokenRequestValidator()
        {

        }

        public async Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            ///context.Result = new TokenRequestValidationResult(new ValidatedTokenRequest() { });
            ///


            //context.Result = new TokenRequestValidationResult(new ValidatedTokenRequest() {
            //    AccessTokenLifetime = 31,
            //    RefreshToken = new RefreshToken() { AccessToken=new Token() { AccessTokenType= AccessTokenType .Jwt} },
            //});
            await Task.FromResult(true);
        }
    }



}
