using System;

namespace NetCoreFrame.Web
{
    /// <summary>
    /// �������ö���
    /// </summary>
    public class StartupOptionModel
    {
        public StartupOptionModel() { }

        /// <summary>
        /// ���õ�¼ҳ��·��
        /// </summary>
        /// <param name="loginPath"></param>
        public StartupOptionModel(string loginPath)
        {
            LoginPath = loginPath;
        }

        public string LoginPath { get; set; } = "/SysLogin/Login";

      
    }
}