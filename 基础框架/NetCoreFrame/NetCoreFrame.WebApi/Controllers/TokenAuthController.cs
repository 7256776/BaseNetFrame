using Abp.Auditing;
using Abp.Runtime.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using System;
using System.Collections.Concurrent;
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

        private static ConcurrentDictionary<string, string> _refreshTokens = new ConcurrentDictionary<string, string>();

        private readonly IUserInfoAppService _userInfoAppService;
        private readonly IAccounExtens _accounExtens;
        private readonly AuthConfigurerModel _authConfigurerModel;

        private readonly IConfiguration _configuration;

        public TokenAuthController(
            IUserInfoAppService userInfoAppService,
            IAccounExtens accounExtens,
            IOptions<AuthConfigurerModel> options,
            IConfiguration configuration
            )
        {
            _userInfoAppService = userInfoAppService;
            _accounExtens = accounExtens;
            _authConfigurerModel = options.Value;

            _configuration = configuration;
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

            //
            string refreshToken = GetEncrpyedRefreshToken("模拟生产刷新token");
            _refreshTokens.TryAdd(result.User.UserCode, refreshToken);

            //创建token
            return CreateAccessTokenModel(result.Identity.Claims.ToList());
        }

        /// <summary>
        /// 获取refreshToken
        /// 验证是通过上次的授权token进行,也可以调整其他方式
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public AuthenticateResultModel RefreshJwtToken()
        {
            #region 验证原有token
            if (!HttpContext.Request.Headers.ContainsKey("Authorization"))
                return new AuthenticateResultModel("未获取到授权token", false);

            var tokenBearer = HttpContext.Request.Headers["Authorization"];

            string[] qsAuthTokenArr = tokenBearer.ToString().Split(' ');
            if (qsAuthTokenArr.Length<=1)
                return new AuthenticateResultModel("未获取到授权token", false);

            string qsAuthToken = qsAuthTokenArr[1];
            //
            var jwtHandler = new JwtSecurityTokenHandler();
            //获取token信息对象
            var data = jwtHandler.ReadJwtToken(qsAuthToken);

            #endregion

            if (!HttpContext.Request.Headers.ContainsKey("refresh"))
                return new AuthenticateResultModel("未获取到刷新refreshToken", false);

            #region 验证刷新key

            var token = HttpContext.Request.Headers["refresh"];
            if (string.IsNullOrWhiteSpace(token))
                return new AuthenticateResultModel("未获取到授权token", false);
            //解码刷新token
            string refreshTokens = SimpleStringCipher.Instance.Decrypt(token, ConstantConfig.DefaultPassPhrase);

            #region 复制原有的票据信息
            var claimsName = data.Claims.First(c => c.Type == ClaimTypes.Name);
            var claimsId = data.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);
            var claimsUserNameCn = data.Claims.FirstOrDefault(s => s.Type == "UserNameCn");
            var claimsUserCode = data.Claims.FirstOrDefault(s => s.Type == "UserCode");
            var claimsIsAdmin = data.Claims.FirstOrDefault(s => s.Type == "IsAdmin");
            var claimsUserRoleList = data.Claims.FirstOrDefault(s => s.Type == "UserRoleList");

            List<Claim> claims = new List<Claim>() {
                    claimsName,
                    claimsId,
                    claimsUserNameCn,
                    claimsUserCode,
                    claimsIsAdmin,
                    claimsUserRoleList
                };
            #endregion

            //通过用户账号寻找是否存在刷新key有效
            _refreshTokens.TryGetValue(claimsName.Value, out string refreshKey);
            #endregion

            if (string.IsNullOrWhiteSpace(refreshKey))
                return new AuthenticateResultModel("原有刷新refreshToken不存在", false);
            //创建新的token
            return CreateAccessTokenModel(claims);
        }

        /// <summary>
        /// 返回token对象
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        private AuthenticateResultModel CreateAccessTokenModel(List<Claim> claims)
        {
            var claimsName = claims.First(c => c.Type == ClaimTypes.Name);
            var claimsId = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            //刷新token 此处可以添加个人信息
            string refreshToken = GetEncrpyedRefreshToken("模拟生产刷新token");
            _refreshTokens.TryAdd(claimsName.Value, refreshToken);

            //创建token
            var accessToken = CreateAccessToken(CreateJwtClaims(claims));

            return new AuthenticateResultModel
            {
                AccessToken = accessToken,
                EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                EncryptedRefreshToken = refreshToken,
                ExpireInSeconds = _authConfigurerModel.JwtBearer.Expires * 60 * 60,
                ExpireInDate = DateTime.Now.AddHours(_authConfigurerModel.JwtBearer.Expires),
                UserId = claimsId.Value
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
        /// ClaimsIdentity identity
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        private List<Claim> CreateJwtClaims(List<Claim> claims)
        {
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
        ///  理论上是给get请求使用或其他情况使用
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        private string GetEncrpyedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, ConstantConfig.DefaultPassPhrase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        private string GetEncrpyedRefreshToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, ConstantConfig.DefaultPassPhrase);
        }

    }


}