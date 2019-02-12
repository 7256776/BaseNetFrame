namespace NetCoreFrame.WebApi
{
    /*
      ���������б�
     "Authentication": {
         "JwtBearer": {
             "IsEnabled": "true",
             "SecurityKey": "��Կ",
             "Issuer": "�䷢������",
             "Audience": "�䷢��Ŀ������"
             "Expires": "10" //����token��Ч�� ��λ/Сʱ
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
        /// �Ƿ���jwt��֤
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// ��Կ
        /// </summary>
        public string SecurityKey { get; set; }

        /// <summary>
        /// �䷢������
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// �䷢��Ŀ������
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// ����token��Ч�� ��λ/Сʱ
        /// </summary>
        public int Expires { get; set; }

    }

  
}
