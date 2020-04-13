using System;

namespace NetCoreFrame.Web
{
    /// <summary>
    /// 启动设置对象
    /// </summary>
    public class StartupOptionModel
    {
        public StartupOptionModel() { }

        /// <summary>
        /// 设置登录页面路径
        /// </summary>
        /// <param name="loginPath"></param>
        public StartupOptionModel(string loginPath)
        {
            LoginPath = loginPath;
        }

        public string LoginPath { get; set; } = "/SysLogin/Login";

      
    }
}