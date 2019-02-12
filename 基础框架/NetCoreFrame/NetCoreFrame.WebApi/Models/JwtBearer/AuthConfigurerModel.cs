namespace NetCoreFrame.WebApi
{
    /*
      参数配置列表
     "Authentication": {
         "JwtBearer": {
             "IsEnabled": "true",
             "SecurityKey": "密钥",
             "Issuer": "颁发者名称",
             "Audience": "颁发给目标名称"
             "Expires": "10" //设置token有效期 单位/小时
         }
     },
     */
    public class AuthConfigurerModel
    {
        /// <summary>
        /// 
        /// </summary>
        public JwtBearerModel JwtBearer { get; set; }
    }

    public class JwtBearerModel
    {
        /// <summary>
        /// 是否开启jwt验证
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string SecurityKey { get; set; }

        /// <summary>
        /// 颁发者名称
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 颁发给目标名称
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 设置token有效期 单位/小时
        /// </summary>
        public int Expires { get; set; }

    }

  
}
