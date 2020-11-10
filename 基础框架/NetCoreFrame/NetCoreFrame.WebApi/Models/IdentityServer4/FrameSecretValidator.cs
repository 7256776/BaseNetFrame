using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.WebApi
{
    /// <summary>
    /// 授权客户端的验证
    /// </summary>
    public class FrameSecretValidator : ISecretValidator
    {
        public Task<SecretValidationResult> ValidateAsync(IEnumerable<Secret> secrets, ParsedSecret parsedSecret)
        {
            SecretValidationResult secretValidationResult = new SecretValidationResult();
            secretValidationResult.Success = true;
            secretValidationResult.IsError = false;
            return Task.FromResult(secretValidationResult);
        }



    }
}
