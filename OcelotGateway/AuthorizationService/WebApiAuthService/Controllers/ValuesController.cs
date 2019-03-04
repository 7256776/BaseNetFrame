using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static WebApiAuthService.RSAHelper;

namespace WebApiAuthService.Controllers
{
    [Route("api/[controller]/[action]")]
    //[ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly AppDbContext _context;
         
        public ValuesController(AppDbContext context)
        {
            _context = context;
        }


        // GET http://localhost:27749/api/values/GetData
        [HttpGet]
        public JsonResult GetData()
        {
            //var data = _context.SysDicts.ToList();

            RSAHelper rsAHelper = new RSAHelper(RSAType.RSA2, Encoding.UTF8, RSAHelper.privateKey, RSAHelper.publicKey);

            //var signStr = rsAHelper.Sign("zjfnihaodiao");
            //var isValid = rsAHelper.Verify("zjfnihaodiao", signStr);

            //var encryptStr = rsAHelper.Encrypt("wode加密文件");
            //var decryptStr = rsAHelper.Decrypt(encryptStr);

            //var publicKeyRSA = rsAHelper.CreateRsaProviderFromPublicKey(RSAHelper.publicKey);
            //var privateKeyRSA = rsAHelper.CreateRsaProviderFromPrivateKey(RSAHelper.privateKey);

            rsAHelper.MainTest();

            return new JsonResult(true);
        }



      

    }
}
