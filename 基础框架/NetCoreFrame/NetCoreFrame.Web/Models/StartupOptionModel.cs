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

        /// <summary>
        /// ��¼��ַ
        /// </summary>
        public string LoginPath { get; set; } = "/SysLogin/Login";

        /// <summary>
        /// Ĭ��·�� ���������
        /// </summary>
        public string MapRouteController { get; set; } = "SysHome";

        /// <summary>
        /// Ĭ��·�� �����ַ
        /// </summary>
        public string MapRouteAction { get; set; } = "index";


    }
}