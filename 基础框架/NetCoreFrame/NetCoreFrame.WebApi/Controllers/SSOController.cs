using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.WebApi;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class SSOController : NetCoreFrameWebApiControllerBase
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;

        public SSOController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events
           )
        {
            // if the TestUserStore is not in DI, then we'll just use the global users collection
            // this is where you would plug in your own custom identity management library (e.g. ASP.NET Identity)
            //_users = users ?? new TestUserStore(TestUsers.Users);

            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
        }

        /// <summary>
        /// SSO登录入口
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginInputModel model = new LoginInputModel();
            model.ReturnUrl = returnUrl;

            //单点登录过程中扩展实现
            //通过url地址获取请求上下文, 该过程会调用客户端验证
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            //返回所有当前注册的身份验证方案。
            //var schemes = await _schemeProvider.GetAllSchemesAsync();
            //根据客户id获取授权客户
            //var client = await _clientStore.FindEnabledClientByIdAsync(context.ClientId);

            return  View(model);
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            //配置Cookie
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                IsPersistent = true,                                                                                         //是否记住登录状态
                ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))    //过期时间
            };
            //示例此处设置默认值
            model.SubjectId = "1";
            
            //使用IdentityServer的SignInAsync来进行注册Cookie
            await HttpContext.SignInAsync(model.SubjectId, model.Username);

            //触发登录成功的事件(事件默认处理非业务的重要操作,记录日志等)
            await _events.RaiseAsync(new UserLoginSuccessEvent(model.Username, model.SubjectId, model.Username));

            //使用IIdentityServerInteractionService的IsValidReturnUrl来验证ReturnUrl是否有效
            if (_interaction.IsValidReturnUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            return View();
        }


        /// <summary>
        /// Show logout page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            //根据注销ID返回请求上下文
            var context = await _interaction.GetLogoutContextAsync(logoutId);
            //context?.ShowSignoutPrompt
            
            //
            var vm = new LogoutInputModel
            {
                //AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = context?.PostLogoutRedirectUri,
                SignOutIframeUrl = context?.SignOutIFrameUrl,
                ClientName = context?.ClientName,
                ClientId = context?.ClientId,
                LogoutId = logoutId
            };

            //当前登录状态
            if (User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    //检查我们是否需要在上游身份提供者处触发签出
                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        //如没有当前的注销上下文，从当前登录的用户的信息创建
                        vm.LogoutId = await _interaction.CreateLogoutContextAsync();
                        string url = Url.Action("Logout", new { logoutId = vm.LogoutId });
                        return SignOut(new AuthenticationProperties { RedirectUri = url }, idp);
                    }
                }
         
                // 删除本地认证cookie
                await HttpContext.SignOutAsync();

                //触发注销的事件
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }
            return View(vm);
        }

    }

    /// <summary>
    /// 注销对象
    /// </summary>
    public class LogoutInputModel
    {
        /// <summary>
        /// 注销ID
        /// </summary>
        public string LogoutId { get; set; }

        /// <summary>
        /// 授权客户名称
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 授权客户ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 注销后返回的请求地址
        /// </summary>
        public string PostLogoutRedirectUri { get; set; }

        /// <summary>
        /// 注销服务端的请求地址
        /// </summary>
        public string SignOutIframeUrl { get; set; }
    }

    /// <summary>
    /// 授权登录对象
    /// </summary>
    public class LoginInputModel
    {
        /// <summary>
        /// 授权账号名称
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// 授权账号密码
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 授权账号唯一编码
        /// </summary>
        public string SubjectId { get; set; }

        /// <summary>
        /// 是否记住登录状态
        /// </summary>
        public bool RememberLogin { get; set; }

        /// <summary>
        /// 返回地址
        /// </summary>
        public string ReturnUrl { get; set; }
    }

}