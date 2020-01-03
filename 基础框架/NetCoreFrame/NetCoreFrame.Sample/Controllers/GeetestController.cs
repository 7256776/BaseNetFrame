using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCoreFrame.Web;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreFrame.Sample.Controllers
{
    public class GeetestController : NetCoreFrameControllerBase
    {

        private IOptions<GeetestOptions> _geetestOptions;
        private ILoggerFactory _loggerFactory;

        public GeetestController(
            IOptions<GeetestOptions> geetestOptions,
            ILoggerFactory loggerFactory)
        {
            _geetestOptions = geetestOptions;
            _loggerFactory = loggerFactory;
        }


        public IActionResult Index()
        {
            return View();
        }

        #region 极验证组件组件回调函数
        /*
         * 验证逻辑
         * 1. 组件以及极验证js的api集成在Geetest.js文件中
         * 2. 组件在页面加载时候会进行初始化,初始化过程会调用Mvc后台服务 getCaptcha(),相关授权key等配置信息初始化在工具类 GeetestLib.cs完成
         * 3. 页面图形拖拽完成后会触发mvc服务 ValidateCaptcha() 并传回初始化时候带入的验证参数, 然后通过工具类 GeetestLib.cs完成调用极验证服务器进行验证.
         * 4. 验证可以根据极验证服务器状态是否正常进行远程验证,或调用本地验证函数进行.
         * 5. 回调的授权码是一次性完成授权后失效
         */

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> getCaptcha()
        {
            GeetestLib geetest = new GeetestLib(_geetestOptions, _loggerFactory);
            string userID = Guid.NewGuid().ToString("N");
            //初始极验证组件需调用远程服务端
            HttpContext.Session.SetString("USERID", userID);
            //验证初始化预处理  判断极验服务器是否宕机 =1正常   =0不正常
           Byte gtServerStatus = await geetest.preProcess(userID, "web");
            //返回授权结果
            var result = geetest.getResponseStr();
            return Json(result);
        }

        /// <summary>
        /// 二次进行验证码验证
        /// </summary>
        /// <param name="geetestChallenge">本次验证会话的唯一标识</param>
        /// <param name="geetestValidate">拖动完成后server端返回的验证结果标识字符串</param>
        /// <param name="geetestSeccode">验证结果的校验码，如果gt-server返回的不与这个值相等则表明验证失败</param>
        /// <returns></returns>
        public async Task<JsonResult> ValidateCaptcha([FromBody]GeetestData model)
        {
            GeetestLib geetest = new GeetestLib(_geetestOptions, _loggerFactory);
            //获取session的用户id
            string userID = HttpContext.Session.GetString("USERID");
            //验证初始化预处理  判断极验服务器是否宕机 =1正常   =0不正常
            Byte gtServerStatus = await geetest.preProcess(userID, "web");
            int result = 0;
            //如果服务器不正常,可以调用自定义的验证
            if (gtServerStatus == 1)
            {
                //二次远程api验证
                result = await geetest.enhencedValidateRequest(model.GeetestChallenge, model.GeetestValidate, model.GeetestSeccode, userID);
            }
            else
            {
                //服务器挂哒采用了默认的验证,这个验证没有实际意义需要自己添加.(仅做演示)
                result = geetest.failbackValidateRequest(model.GeetestChallenge, model.GeetestValidate, model.GeetestSeccode);
            }
            return Json(result == 1 ? "success" : "cuowu");
        }

        #endregion

        #region 非对称加密解密示例 
        /// <summary>
        /// 获取非对称加密key
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> SignData([FromBody]SecretKey model)
        {
            RSAHelper rsaHelper = new RSAHelper();
            //入参的字符串采用该方式转换成 字节数组
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(model.StringData);

            #region 签名
            //设置私钥配置
            RSAParameters privateParameters = RSAHelper.ToRSAParameters(model.PrivateKey);
            //签名后的数据
            byte[] signDataByte = rsaHelper.SignData(byteArray, privateParameters, HashAlgorithmName.MD5);
            #endregion

            #region 验证前面
            RSAParameters publicParameters = RSAHelper.ToRSAParameters(model.PublicKey);
            bool isVerifyData = rsaHelper.VerifyData(byteArray, signDataByte, publicParameters, HashAlgorithmName.MD5);
            #endregion

            string encryptData = RSAHelper.ToByteString(signDataByte);
            return await Task.FromResult(Json(encryptData));
        }

        /// <summary>
        /// 获取非对称加密key
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> CreateAsymmetricAlgorithmSecretKey([FromBody]SecretKey model)
        {
            RSAHelper rsaHelper = new RSAHelper(model.KeySizeInBits);
            RSASecretKey rsaSecretKey = rsaHelper.CreateAsymmetricAlgorithmSecretKey();
            return await Task.FromResult(Json(rsaSecretKey));
        }
        
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<JsonResult> EncryptData([FromBody]SecretKey model)
        {
            RSAHelper rsaHelper = new RSAHelper();
            //入参的字符串采用该方式转换成 字节数组
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(model.StringData);
            //设置公钥配置
            RSAParameters publicParameters = RSAHelper.ToRSAParameters(model.PublicKey);
            //加密
            var encryptDataByte = rsaHelper.EncryptData(byteArray, publicParameters, model.RsaEncryptionPadding);
            //转换 64进制编码字段,解码需采用同类方式解码
            string encryptData = RSAHelper.ToByteString(encryptDataByte);
            return Task.FromResult(Json(encryptData));
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<JsonResult> DecryptData([FromBody]SecretKey model)
        {
            RSAHelper rsaHelper = new RSAHelper();
            //加密解密过程中需采用  64进制编码字段 进行传递
            var buffer = RSAHelper.ToStringByte(model.StringData);
            //设置私钥配置
            RSAParameters privateParameters = RSAHelper.ToRSAParameters(model.PrivateKey);
            //解密
            var decryptDataByte = rsaHelper.DecryptData(buffer, privateParameters, model.RsaEncryptionPadding);
            //出参需采用如下方式转换成正常的字符串返回
            string decryptData = System.Text.Encoding.Default.GetString(decryptDataByte);
            //该方式效果同上
            //string decryptData = System.Text.Encoding.GetEncoding("utf-8").GetString(decryptDataByte);
            return Task.FromResult(Json(decryptData));
        }

        #endregion



    }
}
