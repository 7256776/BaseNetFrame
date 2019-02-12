using Abp.Web.Models;
using Abp.Web.Security.AntiForgery;
using Frame.Application;
using Frame.Core;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Frame.WebApi
{
    public class AccountController : FrameExtAbpApiController
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        static AccountController()
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
        }


        private readonly IUserInfoAppService _userInfoAppService;
        private readonly IAbpAntiForgeryManager _antiForgeryManager;

        public AccountController( 
            IUserInfoAppService userInfoAppService,
            IAbpAntiForgeryManager antiForgeryManager)
        {
            _userInfoAppService = userInfoAppService;
            _antiForgeryManager = antiForgeryManager;

        }

        [HttpPost]
        public async Task<AjaxResponse> Authenticate(LoginUser model)
        {
            SysLoginResult<UserInfo> result = await _userInfoAppService.LoginAuth(model);
            if (string.IsNullOrEmpty(model.UserNameCn))
            {
                _userInfoAppService.SetAuthenticationProperties(model, result);
            }

            var ticket = new AuthenticationTicket(result.Identity, new AuthenticationProperties());

            var currentUtc = new SystemClock().UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(ConstantConfig.WebApiExpires));

            return new AjaxResponse(OAuthBearerOptions.AccessTokenFormat.Protect(ticket));
        }


        /// <summary>
        /// HttpResponseMessage
        /// HttpResponseHeaders
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        public HttpResponseHeaders GetTokenCookie()
        {
            var response = new HttpResponseMessage();

            _antiForgeryManager.SetCookie(response.Headers);

            var data = response.Headers.GetValues("Set-Cookie");

            return response.Headers;
            //return response;
        }



    }
}
