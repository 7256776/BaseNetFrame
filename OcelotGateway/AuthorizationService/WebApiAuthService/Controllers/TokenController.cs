using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace WebApiAuthService.Controllers
{
    [Produces("application/json")]
    [Route("api/Token/[action]/{id?}")]
    public class TokenController : Controller
    {
        [HttpPost]
        public async Task<JObject> GetClientCredentials()
        {
            var disco = await DiscoveryClient.GetAsync($"{Request.Scheme}://{Request.Host}");
            if (disco.IsError) {
                Console.WriteLine(disco.Error);
                return null;
            }
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");

            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("apiA");
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return null;
            }
            return tokenResponse.Json;
        }


        [HttpPost]
        public async Task<JObject> GetTokenOwnerPassword()
        {
            var disco = await DiscoveryClient.GetAsync($"{Request.Scheme}://{Request.Host}");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return null;
            }
            var tokenClient = new TokenClient(disco.TokenEndpoint, "clientCode", "secretPass");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("zjf", "zjf", "apiA");
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return null;
            }
            return tokenResponse.Json;
        }

        [HttpPost]
        public async Task<JObject> GetTokenRefreshToken([FromBody]string id)
        {
            var disco = await DiscoveryClient.GetAsync($"{Request.Scheme}://{Request.Host}");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return null;
            }
            var tokenClient = new TokenClient(disco.TokenEndpoint, "clientRefreshTokens", "secretPass");
            var tokenResponse = await tokenClient.RequestRefreshTokenAsync(id);
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return null;
            }
            return tokenResponse.Json;
        }





    }
}
