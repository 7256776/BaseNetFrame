using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WebAop.Aop;
using static WebApiAuthService.RSAHelper;

namespace WebApiAuthService.Controllers
{
    [Route("api/[controller]/[action]")]
    //[ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly ICustomService _service;
        private readonly IIgnoreService _lgnoreService;

        public ValuesController(
            AppDbContext context, 
            ICustomService service,
            IIgnoreService lgnoreService)
        {
            _context = context;
            _service = service;
            _lgnoreService = lgnoreService;
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
            var privateKeyRSA = rsAHelper.CreateRsaProviderFromPrivateKey(RSAHelper.privateKey);

            //rsAHelper.MainTest();

            return new JsonResult(true);
        }

        /// <summary>
        /// http://localhost:27749/api/values/GetAopData
        /// </summary>
        /// <returns></returns>
        [CustomInterceptor]
        public JsonResult GetAopData()
        {
            SysUser user = new SysUser() { UserName="名称",UserCode="编码"};
            _lgnoreService.GetA(user);
            _lgnoreService.GetB();
            _lgnoreService.GetC();

            _service.Call();
            //_service.Say();

            return new JsonResult(true);
        }

    }
}
