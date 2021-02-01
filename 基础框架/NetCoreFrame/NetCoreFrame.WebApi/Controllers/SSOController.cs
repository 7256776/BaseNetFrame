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
        /// SSO��¼���
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginInputModel model = new LoginInputModel();
            model.ReturnUrl = returnUrl;

            //�����¼��������չʵ��
            //ͨ��url��ַ��ȡ����������, �ù��̻���ÿͻ�����֤
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            //�������е�ǰע��������֤������
            //var schemes = await _schemeProvider.GetAllSchemesAsync();
            //���ݿͻ�id��ȡ��Ȩ�ͻ�
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
            //����Cookie
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                IsPersistent = true,                                                                                         //�Ƿ��ס��¼״̬
                ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))    //����ʱ��
            };
            //ʾ���˴�����Ĭ��ֵ
            model.SubjectId = "1";
            
            //ʹ��IdentityServer��SignInAsync������ע��Cookie
            await HttpContext.SignInAsync(model.SubjectId, model.Username);

            //������¼�ɹ����¼�(�¼�Ĭ�ϴ����ҵ�����Ҫ����,��¼��־��)
            await _events.RaiseAsync(new UserLoginSuccessEvent(model.Username, model.SubjectId, model.Username));

            //ʹ��IIdentityServerInteractionService��IsValidReturnUrl����֤ReturnUrl�Ƿ���Ч
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
            //����ע��ID��������������
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

            //��ǰ��¼״̬
            if (User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    //��������Ƿ���Ҫ����������ṩ�ߴ�����ǩ��
                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        //��û�е�ǰ��ע�������ģ��ӵ�ǰ��¼���û�����Ϣ����
                        vm.LogoutId = await _interaction.CreateLogoutContextAsync();
                        string url = Url.Action("Logout", new { logoutId = vm.LogoutId });
                        return SignOut(new AuthenticationProperties { RedirectUri = url }, idp);
                    }
                }
         
                // ɾ��������֤cookie
                await HttpContext.SignOutAsync();

                //����ע�����¼�
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }
            return View(vm);
        }

    }

    /// <summary>
    /// ע������
    /// </summary>
    public class LogoutInputModel
    {
        /// <summary>
        /// ע��ID
        /// </summary>
        public string LogoutId { get; set; }

        /// <summary>
        /// ��Ȩ�ͻ�����
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// ��Ȩ�ͻ�ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// ע���󷵻ص������ַ
        /// </summary>
        public string PostLogoutRedirectUri { get; set; }

        /// <summary>
        /// ע������˵������ַ
        /// </summary>
        public string SignOutIframeUrl { get; set; }
    }

    /// <summary>
    /// ��Ȩ��¼����
    /// </summary>
    public class LoginInputModel
    {
        /// <summary>
        /// ��Ȩ�˺�����
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// ��Ȩ�˺�����
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// ��Ȩ�˺�Ψһ����
        /// </summary>
        public string SubjectId { get; set; }

        /// <summary>
        /// �Ƿ��ס��¼״̬
        /// </summary>
        public bool RememberLogin { get; set; }

        /// <summary>
        /// ���ص�ַ
        /// </summary>
        public string ReturnUrl { get; set; }
    }

}