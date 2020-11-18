using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.WebApi
{
    /// <summary>
    /// 授权客户端秘钥验证
    /// </summary>
    public class FrameSecretValidator : ISecretValidator
    {
        /// <summary>
        /// 设置秘钥类型后会触发该实现进行验证秘钥
        /// ClientSecrets = new[] {                                                                                       
        ///      new Secret()
        ///      {
        ///          Value = client.ClientSecrets,                                                                       
        ///          Type = IdentityServerConstants.SecretTypes.SharedSecret       
        ///      }
        ///  }
        /// </summary>
        /// <param name="secrets"></param>
        /// <param name="parsedSecret"></param>
        /// <returns></returns>
        public Task<SecretValidationResult> ValidateAsync(IEnumerable<Secret> secrets, ParsedSecret parsedSecret)
        {
            SecretValidationResult secretValidationResult = new SecretValidationResult();
            secretValidationResult.Success = true;
            secretValidationResult.IsError = false;
            return Task.FromResult(secretValidationResult);
        }



    }
}
