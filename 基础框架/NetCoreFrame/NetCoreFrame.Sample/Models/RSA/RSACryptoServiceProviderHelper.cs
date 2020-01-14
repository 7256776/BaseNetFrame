using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreFrame.Sample
{
    #region 仅适用于.NetFrame的框架

    /*示例代码
     * //生成秘钥
     * var secretKey = RSAHelper.CreateAsymmetricAlgorithmSecretKey();
     * 
     * string key = "你好加密文件";
     * //加密
     * byte[] byteArray = System.Text.Encoding.Default.GetBytes(key);
     * var en = RSAHelper.EncryptData(byteArray, secretKey.PublicKey);
     * //解密
     * var dec = RSAHelper.DecryptData(en, secretKey.PrivateKey);
     * string str = System.Text.Encoding.Default.GetString(dec);
     */

    /// <summary>
    /// 数据请求响应对象
    /// </summary>
    public class RSACryptoServiceProviderHelper
    {
        #region 公钥&私有的XML格式示例
        /*
        static string PublicKey = @"<RSAKeyValue>
                                        <Modulus>
                                            nwbjN1znmyL2KyOIrRy/PbWZpTi+oekJIoGNc6jHCl0JNZLFHNs70fyf7y44BH8L8MBkSm5sSwCZfLm5nAsDNOmuEV5Uab5DuWYSE4R2Z3NkKexJJ4bnmXEZYXPMzTbXIpyvU2y9YVrz1BjjRPeHsb6daVdrBgjs4+2b/ok9myM=
                                        </Modulus>
                                        <Exponent>AQAB</Exponent>
                                    </RSAKeyValue>";

        static string PrivateKey = @"<RSAKeyValue>      
                                        <Modulus>
                                            nwbjN1znmyL2KyOIrRy/PbWZpTi+oekJIoGNc6jHCl0JNZLFHNs70fyf7y44BH8L8MBkSm5sSwCZfLm5nAsDNOmuEV5Uab5DuWYSE4R2Z3NkKexJJ4bnmXEZYXPMzTbXIpyvU2y9YVrz1BjjRPeHsb6daVdrBgjs4+2b/ok9myM=
                                        </Modulus>
                                        <Exponent>AQAB</Exponent>
                                        <P>2PfagXD2zKzUGLkAXpC+04u0xvESpO1PbTUOGA2m8auviEMNz8VempJ/reOlJjEO2q2nrUsbtqKd0m96Cxz0Fw==</P>
                                        <Q>u6Kiit1XhIgRD9jQnQh36y28LOmku2Gqn9KownMSVGhWzkkHQPw77A2h1OirQiKe6aOIO/yxdwTI/9Ds4Kwc1Q==</Q>
                                        <DP>GfwtPj1yQXcde8yEX88EG7/qqbzrl7cYQSMOihDwgpcmUbJ+L/kaaHbNNd1CxT0w4z3TDC0np4r4TeCuBDC2hw==</DP>
                                        <DQ>hl8I0jOC2klrFpMpilunLUeaa/uCWiKuQzhkXKR1qvbxu1b3F+XKr9hvXX6mLn2GmkDfbj4fhOFrZC/lg1weZQ==</DQ>
                                        <InverseQ>P1y+6el2+1LsdwL14hYCILvsTKGokGSKD35N7HakLmHNjXiU05hN1cnXMsGIZGg+pNHmz/yuPmgNLJoNZCQiCg==</InverseQ>
                                        <D>D27DriO99jg2W4lfQi2AAaUV/Aq9tUjAMjEQYSEH7+GHe0N7DYnZDE/P1Y5OsWEC76I8GV0N9Vlhi9EaSiJndRvUgphTL2YuAjrVr59Il+lwh/LUBN46AX3cmQm3cFf1F1FXKj4S+QCx/qrCH4mmKpynuQsPL/1XiQSWpugI30E=</D>
                                    </RSAKeyValue>";
         */
        #endregion

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] EncryptData(byte[] data, string publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            //将公钥导入到RSA对象中，准备加密；
            rsa.FromXmlString(publicKey);
            //对数据data进行加密，并返回加密结果；
            //第二个参数用来选择Padding的格式
            byte[] buffer = rsa.Encrypt(data, false);
            return buffer;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] DecryptData(byte[] data, string privateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            //将私钥导入RSA中，准备解密；
            rsa.FromXmlString(privateKey);
            //对数据进行解密，并返回解密结果；
            return rsa.Decrypt(data, false);
        }

        /// <summary>
        /// 随机生成秘钥（非对称算法）
        /// </summary>
        public static RSACryptoKey CreateAsymmetricAlgorithmSecretKey()
        {
            using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(1024))
            {
                return new RSACryptoKey()
                {
                    PublicKey = provider.ToXmlString(false),//公钥（Xml格式）
                    PrivateKey = provider.ToXmlString(true)//私钥（Xml格式）
                };
            }
        }

    }

    /// <summary>
    /// 秘钥对象
    /// </summary>
    public class RSACryptoKey
    {
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
    }

    #endregion
}
