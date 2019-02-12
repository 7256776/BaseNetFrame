using Abp.Auditing;
using Abp.Runtime.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreFrame.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TokenAuthController : NetCoreFrameWebApiControllerBase
    {
        private readonly IUserInfoAppService _userInfoAppService;
        private readonly IAccounExtens _accounExtens;
        private readonly AuthConfigurerModel _authConfigurerModel;


        public TokenAuthController(
            IUserInfoAppService userInfoAppService,
            IAccounExtens accounExtens,
            IOptions<AuthConfigurerModel> options
            )
        {
            _userInfoAppService = userInfoAppService;
            _accounExtens = accounExtens;
            _authConfigurerModel = options.Value;
        }

        /// <summary>
        /// 获取token
        /// 扩展原有方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AuthenticateResultModel> AuthenticateAuth([FromBody]LoginUser model)
        {
            return await Authenticate(new AuthenticateModel() { Password = model.Password, UserNameOrEmailAddress = model.UserCode });
        }

        /// <summary>
        /// 获取token
        /// 为了与abp扩展的abp.swagger.js请求的对象保持一致
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AuthenticateResultModel> Authenticate([FromBody]AuthenticateModel model)
        {

            //调用接口实现的登录验证
            SysLoginResult<UserInfo> result = await _accounExtens.LoginRequest(new LoginUser()
            {
                UserCode = model.UserNameOrEmailAddress,
                Password = model.Password
            });

            //创建token
            var accessToken = CreateAccessToken(CreateJwtClaims(result.Identity));

            return new AuthenticateResultModel
            {
                AccessToken = accessToken,
                EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                ExpireInSeconds = _authConfigurerModel.JwtBearer.Expires * 60 * 60,
                ExpireInDate = DateTime.Now.AddHours(_authConfigurerModel.JwtBearer.Expires),
                UserId = result.User.Id
            };
        }

        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="expiration"></param>
        /// <returns></returns>
        private string CreateAccessToken(IEnumerable<Claim> claims)
        {
            var now = DateTime.UtcNow;
            int expires = _authConfigurerModel.JwtBearer.Expires == 0 ? 12 : _authConfigurerModel.JwtBearer.Expires;
            //加密密钥
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfigurerModel.JwtBearer.SecurityKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //设置授权信息
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _authConfigurerModel.JwtBearer.Issuer,
                audience: _authConfigurerModel.JwtBearer.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddHours(expires),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        /// <summary>
        /// 设置授权扩展信息 
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
                //..
            });

            return claims;
        }

        /// <summary>
        ///  加密访问令牌
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        private string GetEncrpyedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, ConstantConfig.DefaultPassPhrase);
        }

    }


}