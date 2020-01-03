using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace NetCoreFrame.Sample
{
    /// <summary>
    /// ASE 加密解密
    /// </summary>
    public abstract class AesObj : SymmetricAlgorithm
    {
        public override KeySizes[] LegalBlockSizes { get; }
        public override KeySizes[] LegalKeySizes { get; }

        /// <summary>
        /// ASE 加密 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AESEncrypt(string input, string key)
        {
            var encryptKey = Encoding.UTF8.GetBytes(key);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(encryptKey, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor,
                            CryptoStreamMode.Write))

                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result,
                            iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        /// <summary>
        /// ASE 解密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string RSAEncrypt(string input, string key)
        {
            var encryptKey = Encoding.UTF8.GetBytes(key);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(encryptKey, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))

                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

    }

    #region  
    /// <summary>
    /// 数据请求响应对象
    /// </summary>
    public class RSAHelper
    {
        #region 公钥&私有的XML格式示例
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
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public int KeySizeInBits { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AlgName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private string CreateType { get; set; }

        private RSA _RSAProvider;
        /// <summary>
        /// 非对称加密提供者
        /// </summary>
        public RSA RSAProvider
        {
            get
            {
                if (_RSAProvider != null)
                {
                    return _RSAProvider;
                }
                if (CreateType == "1")
                {
                    _RSAProvider = RSA.Create(KeySizeInBits);
                }
                else if (CreateType == "2")
                {
                    _RSAProvider = RSA.Create(AlgName);
                }
                else
                {
                    _RSAProvider = RSA.Create();
                }
                return _RSAProvider;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public RSAHelper() { }

        /// <summary>
        /// 创建秘钥
        /// 设置秘钥长度 通常是1024
        /// </summary>
        /// <param name="keySizeInBits"></param>
        public RSAHelper(int keySizeInBits)
        {
            this.KeySizeInBits = keySizeInBits;
            this.CreateType = "1";
        }

        /// <summary>
        /// 创建秘钥
        /// 设置秘钥ALG名称
        /// </summary>
        /// <param name="algName"></param>
        public RSAHelper(string algName)
        {
            this.AlgName = algName;
            this.CreateType = "2";
        }

        /// <summary>
        /// 未做示例 暂未使用
        /// </summary>
        /// <param name="parameters"></param>
        public RSAHelper(RSAParameters parameters)
        {

        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="publicKey"></param>
        /// <param name="rsaEncryptionPadding"></param>
        /// <returns></returns>
        public byte[] EncryptData(byte[] data, RSAParameters publicKey, RSAEncryptionPadding rsaEncryptionPadding)
        {
            //导入公钥配置  rsa.FromXmlString() 似乎平台兼容有问题采用如下方式
            RSAProvider.ImportParameters(publicKey);
            //加密返回 字节数组
            return RSAProvider.Encrypt(data, rsaEncryptionPadding);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] DecryptData(byte[] data, RSAParameters privateKey, RSAEncryptionPadding rsaEncryptionPadding)
        {
            //导入私钥配置  rsaToXmlString() 似乎平台兼容有问题采用如下方式
            RSAProvider.ImportParameters(privateKey);
            //解密返回 字节数组
            return RSAProvider.Decrypt(data, rsaEncryptionPadding);
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] SignData(byte[] data, RSAParameters privateKey, HashAlgorithmName hashAlgorithmName)
        {
            //导入私钥配置  rsaToXmlString() 似乎平台兼容有问题采用如下方式
            RSAProvider.ImportParameters(privateKey);
            //解密返回 字节数组
            return RSAProvider.SignData(data, hashAlgorithmName, RSASignaturePadding.Pss);
        }

        /// <summary>
        /// 验证签名的消息是否被篡改
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool VerifyData(byte[] stringData, byte[] signData, RSAParameters publicKey, HashAlgorithmName hashAlgorithmName)
        {
            //导入私钥配置  rsaToXmlString() 似乎平台兼容有问题采用如下方式
            RSAProvider.ImportParameters(publicKey);
            //解密返回 字节数组
            return RSAProvider.VerifyData(stringData, signData, hashAlgorithmName, RSASignaturePadding.Pss);
        }

        /// <summary>
        /// 随机生成秘钥（非对称算法）
        /// </summary>
        public RSASecretKey CreateAsymmetricAlgorithmSecretKey()
        {
            RSAParameters PrivateByteKey = RSAProvider.ExportParameters(true);//私钥（Xml格式）
            RSAParameters PublicByteKey = RSAProvider.ExportParameters(false);//公钥（Xml格式）
            //
            return new RSASecretKey()
            {
                PrivateKey = ToCreateKey(PrivateByteKey),//私钥（Xml格式）
                PublicKey = ToCreateKey(PublicByteKey),//公钥（Xml格式）
            };
        }

        /// <summary>
        /// 转换秘钥配置To秘钥对象
        /// </summary>
        /// <param name="rsa"></param>
        /// <param name="includePrivateParameters"></param>
        /// <returns></returns>
        public static RSAKey ToCreateKey(RSAParameters parameters)
        {
            //
            return new RSAKey()
            {
                Modulus = ToByteString(parameters.Modulus),
                Exponent = ToByteString(parameters.Exponent),
                P = ToByteString(parameters.P),
                Q = ToByteString(parameters.Q),
                DP = ToByteString(parameters.DP),
                DQ = ToByteString(parameters.DQ),
                InverseQ = ToByteString(parameters.InverseQ),
                D = ToByteString(parameters.D),
            };
        }

        /// <summary>
        /// 转换秘钥对象To秘钥配置
        /// </summary>
        /// <param name="rsaKey"></param>
        /// <returns></returns>
        public static RSAParameters ToRSAParameters(RSAKey rsaKey)
        {
            return new RSAParameters()
            {
                Modulus = ToStringByte(rsaKey.Modulus),
                Exponent = ToStringByte(rsaKey.Exponent),
                P = ToStringByte(rsaKey.P),
                Q = ToStringByte(rsaKey.Q),
                DP = ToStringByte(rsaKey.DP),
                DQ = ToStringByte(rsaKey.DQ),
                InverseQ = ToStringByte(rsaKey.InverseQ),
                D = ToStringByte(rsaKey.D)
            };
        }

        /// <summary>
        /// 转换字节数组为64位字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToByteString(byte[] data)
        {
            if (data != null)
            {
                return Convert.ToBase64String(data);
            }
            return null;
        }

        /// <summary>
        /// 转换64位字符串为字节数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToStringByte(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return Convert.FromBase64String(str);
            }
            return null;
        }

    }

    /// <summary>
    /// 授权信息对象
    /// </summary>
    public class RSASecretKey
    {
        public RSAKey PrivateKey { get; set; }
        public RSAKey PublicKey { get; set; }
    }

    /// <summary>
    /// 秘钥对象
    /// </summary>
    public class RSAKey
    {
        public string Modulus { get; set; }
        public string Exponent { get; set; }
        public string P { get; set; }
        public string Q { get; set; }
        public string DP { get; set; }
        public string DQ { get; set; }
        public string InverseQ { get; set; }
        public string D { get; set; }
    }

    /// <summary>
    /// 参数示例对象
    /// </summary>
    public class SecretKey
    {
        public string StringData { get; set; }
        public RSAKey PrivateKey { get; set; }
        public RSAKey PublicKey { get; set; }
        public int KeySizeInBits { get; set; }
        public string RsaEncryption { get; set; }
        /// <summary>
        /// 转义加密类型字符串默认 OaepSHA1
        /// </summary>
        public RSAEncryptionPadding RsaEncryptionPadding
        {
            get
            {
                if (this.RsaEncryption == "OaepSHA1")
                {
                    return RSAEncryptionPadding.OaepSHA1;
                }
                else if (this.RsaEncryption == "OaepSHA256")
                {
                    return RSAEncryptionPadding.OaepSHA256;
                }
                else if (this.RsaEncryption == "OaepSHA384")
                {
                    return RSAEncryptionPadding.OaepSHA384;
                }
                else if (this.RsaEncryption == "OaepSHA512")
                {
                    return RSAEncryptionPadding.OaepSHA512;
                }
                else if (this.RsaEncryption == "Pkcs1")
                {
                    return RSAEncryptionPadding.Pkcs1;
                }
                return RSAEncryptionPadding.OaepSHA1;
            }
        }
    }
    #endregion

    #region 参考示例 其中包含了秘钥转换各种格式,可以作为参考进行扩展开发
    /*
    public static class RsaHelper
    {
        /// <summary>
        /// private key sign
        /// </summary>
        /// <param name="preSign"></param>
        /// <param name="isJavaFormatKey"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string RsaSign(string preSign, bool isJavaFormatKey, string privateKey)
        {
            try
            {
                //// net
                //var rsa = new RSACryptoServiceProvider();
                //rsa.FromXmlString(privateKey);
                //byte[] signBytes = rsa.SignData(UTF8Encoding.UTF8.GetBytes(signStr), "md5");
                //return Convert.ToBase64String(signBytes);

                // net core 2.0
                using (var rsa = RSA.Create())
                {
                    if (isJavaFormatKey) privateKey = RsaPrivateKeyJava2DotNet(privateKey);
                    rsa.FromXmlStringExtensions(privateKey);
                    var bytes = rsa.SignData(Encoding.UTF8.GetBytes(preSign), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                    return Convert.ToBase64String(bytes);
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
        /// <summary>
        /// .net => java Md5WithRsa 
        /// </summary>
        /// <param name="preSign"></param>
        /// <param name="isJavaFormatKey"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string RsaSignMd5WithRsa(string preSign, bool isJavaFormatKey, string privateKey)
        {
            try
            {
                //// net
                //var rsa = new RSACryptoServiceProvider();
                //rsa.FromXmlString(privateKey);
                //byte[] signBytes = rsa.SignData(UTF8Encoding.UTF8.GetBytes(signStr), "md5");
                //return Convert.ToBase64String(signBytes);

                // net core 2.0
                using (var rsa = RSA.Create())
                {
                    if (isJavaFormatKey) privateKey = RsaPrivateKeyJava2DotNet(privateKey);
                    rsa.FromXmlStringExtensions(privateKey);
                    var bytes = rsa.SignData(Encoding.UTF8.GetBytes(preSign), HashAlgorithmName.MD5, RSASignaturePadding.Pkcs1);
                    return Convert.ToBase64String(bytes);
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
        /// <summary>
        /// public key sign
        /// </summary>
        /// <param name="preSign"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static string RsaEncrypt(string preSign, string publicKey)
        {
            try
            {
                //// net
                //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                //rsa.FromXmlString(strPublicKey);
                //byte[] bytes = rsa.Encrypt(UTF8Encoding.UTF8.GetBytes(strEncryptInfo), false);
                //return Convert.ToBase64String(bytes);

                // net core 2.0
                using (var rsa = RSA.Create())
                {
                    publicKey = RsaPublicKeyJava2DotNet(publicKey);
                    rsa.FromXmlStringExtensions(publicKey);
                    var bytes = rsa.Encrypt(Encoding.UTF8.GetBytes(preSign), RSAEncryptionPadding.Pkcs1);
                    return Convert.ToBase64String(bytes);
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
        /// <summary>
        /// public key validate
        /// </summary>
        /// <param name="preSign"></param>
        /// <param name="isJavaFormatKey"></param>
        /// <param name="publicKey"></param>
        /// <param name="signedData"></param>
        /// <returns></returns>
        public static bool ValidateRsaSign(string preSign, bool isJavaFormatKey, string publicKey, string signedData)
        {
            try
            {
                //// net
                //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                //rsa.FromXmlString(publicKey);
                //return rsa.VerifyData(UTF8Encoding.UTF8.GetBytes(plainText), "md5", Convert.FromBase64String(signedData));

                // net core 2.0
                using (var rsa = RSA.Create())
                {
                    if (isJavaFormatKey) publicKey = RsaPublicKeyJava2DotNet(publicKey);
                    rsa.FromXmlStringExtensions(publicKey);
                    return rsa.VerifyData(Encoding.UTF8.GetBytes(preSign), Convert.FromBase64String(signedData), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
        /// <summary>
        /// private key ，java->.net
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string RsaPrivateKeyJava2DotNet(string privateKey)
        {
            if (string.IsNullOrEmpty(privateKey))
            {
                return string.Empty;
            }
            var privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));
            return
                $"<RSAKeyValue><Modulus>{Convert.ToBase64String(privateKeyParam.Modulus.ToByteArrayUnsigned())}</Modulus><Exponent>{Convert.ToBase64String(privateKeyParam.PublicExponent.ToByteArrayUnsigned())}</Exponent><P>{Convert.ToBase64String(privateKeyParam.P.ToByteArrayUnsigned())}</P><Q>{Convert.ToBase64String(privateKeyParam.Q.ToByteArrayUnsigned())}</Q><DP>{Convert.ToBase64String(privateKeyParam.DP.ToByteArrayUnsigned())}</DP><DQ>{Convert.ToBase64String(privateKeyParam.DQ.ToByteArrayUnsigned())}</DQ><InverseQ>{Convert.ToBase64String(privateKeyParam.QInv.ToByteArrayUnsigned())}</InverseQ><D>{Convert.ToBase64String(privateKeyParam.Exponent.ToByteArrayUnsigned())}</D></RSAKeyValue>";
        }
        /// <summary>
        /// public key ，java->.net
        /// </summary>
        /// <param name="publicKey"></param>
        /// <returns>格式转换结果</returns>
        public static string RsaPublicKeyJava2DotNet(string publicKey)
        {
            if (string.IsNullOrEmpty(publicKey))
            {
                return string.Empty;
            }

            var publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
            return string.Format(
                "<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
                Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
                Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned())
            );
        }
        /// <summary>
        /// private key ，.net->java
        /// </summary>
        /// <param name="privateKey">.net生成的私钥</param>
        /// <returns></returns>
        public static string RsaPrivateKeyDotNet2Java(string privateKey)
        {
            var doc = new XmlDocument();
            doc.LoadXml(privateKey);
            var m = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Modulus")[0].InnerText));
            var exp = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Exponent")[0].InnerText));
            var d = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("D")[0].InnerText));
            var p = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("P")[0].InnerText));
            var q = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Q")[0].InnerText));
            var dp = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("DP")[0].InnerText));
            var dq = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("DQ")[0].InnerText));
            var qinv = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("InverseQ")[0].InnerText));

            var privateKeyParam = new RsaPrivateCrtKeyParameters(m, exp, d, p, q, dp, dq, qinv);

            var privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKeyParam);
            var serializedPrivateBytes = privateKeyInfo.ToAsn1Object().GetEncoded();
            return Convert.ToBase64String(serializedPrivateBytes);
        }
        /// <summary>
        /// public key ，.net->java
        /// </summary>
        /// <param name="publicKey">.net生成的公钥</param>
        /// <returns></returns>
        public static string RsaPublicKeyDotNet2Java(string publicKey)
        {
            var doc = new XmlDocument();
            doc.LoadXml(publicKey);
            var m = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Modulus")[0].InnerText));
            var p = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Exponent")[0].InnerText));
            var pub = new RsaKeyParameters(false, m, p);

            var publicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pub);
            var serializedPublicBytes = publicKeyInfo.ToAsn1Object().GetDerEncoded();
            return Convert.ToBase64String(serializedPublicBytes);
        }
        // 处理 下面两种方式都会出现的 Operation is not supported on this platform 异常
        // RSA.Create().FromXmlString(privateKey) 
        // new RSACryptoServiceProvider().FromXmlString(privateKey) 
        /// <summary>
        /// 扩展FromXmlString
        /// </summary>
        /// <param name="rsa"></param>
        /// <param name="xmlString"></param>
        private static void FromXmlStringExtensions(this RSA rsa, string xmlString)
        {
            var parameters = new RSAParameters();

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus":
                            parameters.Modulus = (string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText));
                            break;
                        case "Exponent":
                            parameters.Exponent = (string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText));
                            break;
                        case "P":
                            parameters.P = (string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText));
                            break;
                        case "Q":
                            parameters.Q = (string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText));
                            break;
                        case "DP":
                            parameters.DP = (string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText));
                            break;
                        case "DQ":
                            parameters.DQ = (string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText));
                            break;
                        case "InverseQ":
                            parameters.InverseQ = (string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText));
                            break;
                        case "D":
                            parameters.D = (string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText));
                            break;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            rsa.ImportParameters(parameters);
        }

        /// <summary>
        /// 扩展ToXmlString,创建公私钥
        /// </summary>
        /// <param name="includePrivateParameters"></param>
        /// <returns></returns>
        public static string ToCreateKey(bool includePrivateParameters)
        {
            using (var rsa = RSA.Create())
            {
                var parameters = rsa.ExportParameters(includePrivateParameters);
                return
                    $"<RSAKeyValue><Modulus>{(parameters.Modulus != null ? Convert.ToBase64String(parameters.Modulus) : null)}</Modulus><Exponent>{(parameters.Exponent != null ? Convert.ToBase64String(parameters.Exponent) : null)}</Exponent><P>{(parameters.P != null ? Convert.ToBase64String(parameters.P) : null)}</P><Q>{(parameters.Q != null ? Convert.ToBase64String(parameters.Q) : null)}</Q><DP>{(parameters.DP != null ? Convert.ToBase64String(parameters.DP) : null)}</DP><DQ>{(parameters.DQ != null ? Convert.ToBase64String(parameters.DQ) : null)}</DQ><InverseQ>{(parameters.InverseQ != null ? Convert.ToBase64String(parameters.InverseQ) : null)}</InverseQ><D>{(parameters.D != null ? Convert.ToBase64String(parameters.D) : null)}</D></RSAKeyValue>";
            }
        }

        /// <summary>
        /// Generate XML Format RSA Key. Result: Index 0 is the private key and index 1 is the public key
        /// </summary>
        /// <param name="keySize">Key Size.Unit: bits</param>
        /// <returns></returns>
        public static List<string> XmlKey(int keySize)
        {
            using (var rsa = RSA.Create())
            {
                rsa.KeySize = keySize;
                var rsap = rsa.ExportParameters(true);
                var res = new List<string>();
                var privatElement = new XElement("RSAKeyValue");
                //Modulus
                var primodulus = new XElement("Modulus", Convert.ToBase64String(rsap.Modulus));
                //Exponent
                var priexponent = new XElement("Exponent", Convert.ToBase64String(rsap.Exponent));
                //P
                var prip = new XElement("P", Convert.ToBase64String(rsap.P));
                //Q
                var priq = new XElement("Q", Convert.ToBase64String(rsap.Q));
                //DP
                var pridp = new XElement("DP", Convert.ToBase64String(rsap.DP));
                //DQ
                var pridq = new XElement("DQ", Convert.ToBase64String(rsap.DQ));
                //InverseQ
                var priinverseQ = new XElement("InverseQ", Convert.ToBase64String(rsap.InverseQ));
                //D
                var prid = new XElement("D", Convert.ToBase64String(rsap.D));

                privatElement.Add(primodulus);
                privatElement.Add(priexponent);
                privatElement.Add(prip);
                privatElement.Add(priq);
                privatElement.Add(pridp);
                privatElement.Add(pridq);
                privatElement.Add(priinverseQ);
                privatElement.Add(prid);

                //添加私钥
                res.Add(privatElement.ToString());

                var publicElement = new XElement("RSAKeyValue");
                //Modulus
                var pubmodulus = new XElement("Modulus", Convert.ToBase64String(rsap.Modulus));
                //Exponent
                var pubexponent = new XElement("Exponent", Convert.ToBase64String(rsap.Exponent));

                publicElement.Add(pubmodulus);
                publicElement.Add(pubexponent);

                //添加公钥
                res.Add(publicElement.ToString());

                return res;
            }
        }
        /// <summary>
        /// Generate RSA key in Pkcs1 format. Result: Index 0 is the private key and index 1 is the public key
        /// </summary>
        /// <param name="keySize">Key Size.Unit: bits</param>
        /// <param name="format">Whether the format is true If it is standard pem file format</param>
        /// <returns></returns>
        public static List<string> Pkcs1Key(int keySize, bool format = true)
        {
            var res = new List<string>();

            var kpGen = GeneratorUtilities.GetKeyPairGenerator("RSA");
            kpGen.Init(new KeyGenerationParameters(new SecureRandom(), keySize));
            var keyPair = kpGen.GenerateKeyPair();

            var sw = new StringWriter();
            var pWrt = new PemWriter(sw);
            pWrt.WriteObject(keyPair.Private);
            pWrt.Writer.Flush();
            var privateKey = sw.ToString();

            if (!format)
            {
                privateKey = privateKey.Replace("-----BEGIN RSA PRIVATE KEY-----", "").Replace("-----END RSA PRIVATE KEY-----", "").Replace("\r\n", "");
            }

            res.Add(privateKey);

            var swpub = new StringWriter();
            var pWrtpub = new PemWriter(swpub);
            pWrtpub.WriteObject(keyPair.Public);
            pWrtpub.Writer.Flush();
            var publicKey = swpub.ToString();
            if (!format)
            {
                publicKey = publicKey.Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "").Replace("\r\n", "");
            }

            res.Add(publicKey);

            return res;
        }
        /// <summary>
        /// Generate Pkcs8 format RSA key. Result: Index 0 is the private key and index 1 is the public key
        /// </summary>
        /// <param name="keySize">Key Size.Unit: bits</param>
        /// <param name="format">Whether the format is true If it is standard pem file format</param>
        /// <returns></returns>
        public static List<string> Pkcs8Key(int keySize, bool format = true)
        {
            var res = new List<string>();

            var kpGen = GeneratorUtilities.GetKeyPairGenerator("RSA");
            kpGen.Init(new KeyGenerationParameters(new SecureRandom(), keySize));
            var keyPair = kpGen.GenerateKeyPair();

            var swpri = new StringWriter();
            var pWrtpri = new PemWriter(swpri);
            var pkcs8 = new Pkcs8Generator(keyPair.Private);
            pWrtpri.WriteObject(pkcs8);
            pWrtpri.Writer.Flush();
            var privateKey = swpri.ToString();

            if (!format)
            {
                privateKey = privateKey.Replace("-----BEGIN PRIVATE KEY-----", "").Replace("-----END PRIVATE KEY-----", "").Replace("\r\n", "");
            }

            res.Add(privateKey);

            var swpub = new StringWriter();
            var pWrtpub = new PemWriter(swpub);
            pWrtpub.WriteObject(keyPair.Public);
            pWrtpub.Writer.Flush();
            var publicKey = swpub.ToString();
            if (!format)
            {
                publicKey = publicKey.Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "").Replace("\r\n", "");
            }

            res.Add(publicKey);

            return res;
        }
        /// <summary>
        /// Public Key Convert xml->xml
        /// </summary>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static string PublicKeyXmlToPem(string publicKey)
        {
            var root = XElement.Parse(publicKey);
            //Modulus
            var modulus = root.Element("Modulus");
            //Exponent
            var exponent = root.Element("Exponent");

            var rsaKeyParameters = new RsaKeyParameters(false, new BigInteger(1, Convert.FromBase64String(modulus?.Value)), new BigInteger(1, Convert.FromBase64String(exponent?.Value)));

            var sw = new StringWriter();
            var pWrt = new PemWriter(sw);
            pWrt.WriteObject(rsaKeyParameters);
            pWrt.Writer.Flush();
            return sw.ToString();
        }
        /// <summary>
        /// Private Key Convert xml->Pkcs1
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string PrivateKeyXmlToPkcs1(string privateKey)
        {
            var root = XElement.Parse(privateKey);
            //Modulus
            var modulus = root.Element("Modulus");
            //Exponent
            var exponent = root.Element("Exponent");
            //P
            var p = root.Element("P");
            //Q
            var q = root.Element("Q");
            //DP
            var dp = root.Element("DP");
            //DQ
            var dq = root.Element("DQ");
            //InverseQ
            var inverseQ = root.Element("InverseQ");
            //D
            var d = root.Element("D");

            var rsaPrivateCrtKeyParameters = new RsaPrivateCrtKeyParameters(
                new BigInteger(1, Convert.FromBase64String(modulus?.Value)),
                new BigInteger(1, Convert.FromBase64String(exponent?.Value)),
                new BigInteger(1, Convert.FromBase64String(d?.Value)),
                new BigInteger(1, Convert.FromBase64String(p?.Value)),
                new BigInteger(1, Convert.FromBase64String(q?.Value)),
                new BigInteger(1, Convert.FromBase64String(dp?.Value)),
                new BigInteger(1, Convert.FromBase64String(dq?.Value)),
                new BigInteger(1, Convert.FromBase64String(inverseQ?.Value)));

            var sw = new StringWriter();
            var pWrt = new PemWriter(sw);
            pWrt.WriteObject(rsaPrivateCrtKeyParameters);
            pWrt.Writer.Flush();
            return sw.ToString();

        }
        /// <summary>
        /// Format public key
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string PublicKeyFormat(string str)
        {
            if (str.StartsWith("-----BEGIN PUBLIC KEY-----"))
            {
                return str;
            }
            var res = new List<string> { "-----BEGIN PUBLIC KEY-----" };
            var pos = 0;
            while (pos < str.Length)
            {
                var count = str.Length - pos < 64 ? str.Length - pos : 64;
                res.Add(str.Substring(pos, count));
                pos += count;
            }
            res.Add("-----END PUBLIC KEY-----");
            var resStr = string.Join("\r\n", res);
            return resStr;
        }
        /// <summary>
        /// Format Pkcs8 format private key
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Pkcs8PrivateKeyFormat(string str)
        {
            if (str.StartsWith("-----BEGIN PRIVATE KEY-----"))
            {
                return str;
            }
            var res = new List<string> { "-----BEGIN PRIVATE KEY-----" };
            var pos = 0;
            while (pos < str.Length)
            {
                var count = str.Length - pos < 64 ? str.Length - pos : 64;
                res.Add(str.Substring(pos, count));
                pos += count;
            }
            res.Add("-----END PRIVATE KEY-----");
            var resStr = string.Join("\r\n", res);
            return resStr;
        }
        /// <summary>
        /// Format Pkcs1 format private key
        /// Author:Zhiqiang Li
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Pkcs1PrivateKeyFormat(string str)
        {
            if (str.StartsWith("-----BEGIN RSA PRIVATE KEY-----"))
            {
                return str;
            }
            var res = new List<string> { "-----BEGIN RSA PRIVATE KEY-----" };
            var pos = 0;
            while (pos < str.Length)
            {
                var count = str.Length - pos < 64 ? str.Length - pos : 64;
                res.Add(str.Substring(pos, count));
                pos += count;
            }
            res.Add("-----END RSA PRIVATE KEY-----");
            var resStr = string.Join("\r\n", res);
            return resStr;
        }
        /// <summary>
        /// Remove the Pkcs8 format private key format
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Pkcs8PrivateKeyFormatRemove(string str)
        {
            if (!str.StartsWith("-----BEGIN PRIVATE KEY-----"))
            {
                return str;
            }
            return str.Replace("-----BEGIN PRIVATE KEY-----", "").Replace("-----END PRIVATE KEY-----", "")
                .Replace("\r\n", "");
        }
        /// <summary>
        /// public Key pem to xml
        /// </summary>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static string PublicKeyPemToXml(string publicKey)
        {
            publicKey = PublicKeyFormat(publicKey);

            var pr = new PemReader(new StringReader(publicKey));
            var obj = pr.ReadObject();
            if (!(obj is RsaKeyParameters rsaKey))
            {
                throw new Exception("Public key format is incorrect");
            }

            var publicElement = new XElement("RSAKeyValue");
            //Modulus
            var pubmodulus = new XElement("Modulus", Convert.ToBase64String(rsaKey.Modulus.ToByteArrayUnsigned()));
            //Exponent
            var pubexponent = new XElement("Exponent", Convert.ToBase64String(rsaKey.Exponent.ToByteArrayUnsigned()));

            publicElement.Add(pubmodulus);
            publicElement.Add(pubexponent);
            return publicElement.ToString();
        }
        /// <summary>
        /// Private Key Convert Pkcs1->xml
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string PrivateKeyPkcs1ToXml(string privateKey)
        {
            privateKey = Pkcs1PrivateKeyFormat(privateKey);

            var pr = new PemReader(new StringReader(privateKey));
            if (!(pr.ReadObject() is AsymmetricCipherKeyPair asymmetricCipherKeyPair))
            {
                throw new Exception("Private key format is incorrect");
            }
            var rsaPrivateCrtKeyParameters =
                (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(
                    PrivateKeyInfoFactory.CreatePrivateKeyInfo(asymmetricCipherKeyPair.Private));

            var privatElement = new XElement("RSAKeyValue");
            //Modulus
            var primodulus = new XElement("Modulus", Convert.ToBase64String(rsaPrivateCrtKeyParameters.Modulus.ToByteArrayUnsigned()));
            //Exponent
            var priexponent = new XElement("Exponent", Convert.ToBase64String(rsaPrivateCrtKeyParameters.PublicExponent.ToByteArrayUnsigned()));
            //P
            var prip = new XElement("P", Convert.ToBase64String(rsaPrivateCrtKeyParameters.P.ToByteArrayUnsigned()));
            //Q
            var priq = new XElement("Q", Convert.ToBase64String(rsaPrivateCrtKeyParameters.Q.ToByteArrayUnsigned()));
            //DP
            var pridp = new XElement("DP", Convert.ToBase64String(rsaPrivateCrtKeyParameters.DP.ToByteArrayUnsigned()));
            //DQ
            var pridq = new XElement("DQ", Convert.ToBase64String(rsaPrivateCrtKeyParameters.DQ.ToByteArrayUnsigned()));
            //InverseQ
            var priinverseQ = new XElement("InverseQ", Convert.ToBase64String(rsaPrivateCrtKeyParameters.QInv.ToByteArrayUnsigned()));
            //D
            var prid = new XElement("D", Convert.ToBase64String(rsaPrivateCrtKeyParameters.Exponent.ToByteArrayUnsigned()));

            privatElement.Add(primodulus);
            privatElement.Add(priexponent);
            privatElement.Add(prip);
            privatElement.Add(priq);
            privatElement.Add(pridp);
            privatElement.Add(pridq);
            privatElement.Add(priinverseQ);
            privatElement.Add(prid);

            return privatElement.ToString();
        }
        /// <summary>
        /// Private Key Convert Pkcs8->xml
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string PrivateKeyPkcs8ToXml(string privateKey)
        {
            privateKey = Pkcs8PrivateKeyFormatRemove(privateKey);
            var privateKeyParam =
                (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));

            var privatElement = new XElement("RSAKeyValue");
            //Modulus
            var primodulus = new XElement("Modulus", Convert.ToBase64String(privateKeyParam.Modulus.ToByteArrayUnsigned()));
            //Exponent
            var priexponent = new XElement("Exponent", Convert.ToBase64String(privateKeyParam.PublicExponent.ToByteArrayUnsigned()));
            //P
            var prip = new XElement("P", Convert.ToBase64String(privateKeyParam.P.ToByteArrayUnsigned()));
            //Q
            var priq = new XElement("Q", Convert.ToBase64String(privateKeyParam.Q.ToByteArrayUnsigned()));
            //DP
            var pridp = new XElement("DP", Convert.ToBase64String(privateKeyParam.DP.ToByteArrayUnsigned()));
            //DQ
            var pridq = new XElement("DQ", Convert.ToBase64String(privateKeyParam.DQ.ToByteArrayUnsigned()));
            //InverseQ
            var priinverseQ = new XElement("InverseQ", Convert.ToBase64String(privateKeyParam.QInv.ToByteArrayUnsigned()));
            //D
            var prid = new XElement("D", Convert.ToBase64String(privateKeyParam.Exponent.ToByteArrayUnsigned()));

            privatElement.Add(primodulus);
            privatElement.Add(priexponent);
            privatElement.Add(prip);
            privatElement.Add(priq);
            privatElement.Add(pridp);
            privatElement.Add(pridq);
            privatElement.Add(priinverseQ);
            privatElement.Add(prid);

            return privatElement.ToString();
        }
        /// <summary>
        /// Private Key Convert xml->Pkcs8
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string PrivateKeyXmlToPkcs8(string privateKey)
        {
            var root = XElement.Parse(privateKey);
            //Modulus
            var modulus = root.Element("Modulus");
            //Exponent
            var exponent = root.Element("Exponent");
            //P
            var p = root.Element("P");
            //Q
            var q = root.Element("Q");
            //DP
            var dp = root.Element("DP");
            //DQ
            var dq = root.Element("DQ");
            //InverseQ
            var inverseQ = root.Element("InverseQ");
            //D
            var d = root.Element("D");

            var rsaPrivateCrtKeyParameters = new RsaPrivateCrtKeyParameters(
                new BigInteger(1, Convert.FromBase64String(modulus?.Value)),
                new BigInteger(1, Convert.FromBase64String(exponent?.Value)),
                new BigInteger(1, Convert.FromBase64String(d?.Value)),
                new BigInteger(1, Convert.FromBase64String(p?.Value)),
                new BigInteger(1, Convert.FromBase64String(q?.Value)),
                new BigInteger(1, Convert.FromBase64String(dp?.Value)),
                new BigInteger(1, Convert.FromBase64String(dq?.Value)),
                new BigInteger(1, Convert.FromBase64String(inverseQ?.Value)));

            var swpri = new StringWriter();
            var pWrtpri = new PemWriter(swpri);
            var pkcs8 = new Pkcs8Generator(rsaPrivateCrtKeyParameters);
            pWrtpri.WriteObject(pkcs8);
            pWrtpri.Writer.Flush();
            return swpri.ToString();

        }
        /// <summary>
        /// Private Key Convert Pkcs1->Pkcs8
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string PrivateKeyPkcs1ToPkcs8(string privateKey)
        {
            privateKey = Pkcs1PrivateKeyFormat(privateKey);
            var pr = new PemReader(new StringReader(privateKey));

            var kp = pr.ReadObject() as AsymmetricCipherKeyPair;
            var sw = new StringWriter();
            var pWrt = new PemWriter(sw);
            var pkcs8 = new Pkcs8Generator(kp?.Private);
            pWrt.WriteObject(pkcs8);
            pWrt.Writer.Flush();
            var result = sw.ToString();
            return result;
        }
        /// <summary>
        /// Private Key Convert Pkcs8->Pkcs1
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string PrivateKeyPkcs8ToPkcs1(string privateKey)
        {
            privateKey = Pkcs8PrivateKeyFormat(privateKey);
            var pr = new PemReader(new StringReader(privateKey));

            var kp = pr.ReadObject() as RsaPrivateCrtKeyParameters;

            var keyParameter = PrivateKeyFactory.CreateKey(PrivateKeyInfoFactory.CreatePrivateKeyInfo(kp));

            var sw = new StringWriter();
            var pWrt = new PemWriter(sw);
            pWrt.WriteObject(keyParameter);
            pWrt.Writer.Flush();
            var result = sw.ToString();
            return result;
        }
    }
    */

    #endregion

}