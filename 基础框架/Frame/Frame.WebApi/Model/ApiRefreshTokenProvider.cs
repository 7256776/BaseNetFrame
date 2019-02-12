using Abp.Dependency;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Frame.WebApi
{
    public class ApiRefreshTokenProvider : IAuthenticationTokenProvider, ITransientDependency
    {

        private static ConcurrentDictionary<string, AuthenticationTicket> _refreshTokens = new ConcurrentDictionary<string, AuthenticationTicket>();

        public Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            //生成刷新token
            RandomNumberGenerator cryptoRandomDataGenerator = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[60];
            cryptoRandomDataGenerator.GetBytes(buffer);
            var refreshTokenId = Convert.ToBase64String(buffer).TrimEnd('=').Replace('+', '-').Replace('/', '_');
            //
            var refreshTokenProperties = new AuthenticationProperties(context.Ticket.Properties.Dictionary)
            {
                //发布时间
                IssuedUtc = context.Ticket.Properties.IssuedUtc,
                //设置有效时间
                ExpiresUtc = DateTime.UtcNow.AddYears(1)
            };
            var refreshTokenTicket = new AuthenticationTicket(context.Ticket.Identity, refreshTokenProperties);
            //保存刷新token到集合
            _refreshTokens.TryAdd(refreshTokenId, refreshTokenTicket);
            //刷新token写入请求上下文
            context.SetToken(refreshTokenId);
            return Task.FromResult<object>(null);
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            AuthenticationTicket ticket;
            if (_refreshTokens.TryRemove(context.Token, out ticket))
            {
                context.SetTicket(ticket);
            }
            return Task.FromResult<object>(null);
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}

 