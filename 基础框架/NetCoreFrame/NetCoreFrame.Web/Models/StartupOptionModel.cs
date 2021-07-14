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

        /// <summary>
        /// 登录地址
        /// </summary>
        public string LoginPath { get; set; } = "/SysLogin/Login";

        /// <summary>
        /// 默认路由 请求控制器
        /// </summary>
        public string MapRouteController { get; set; } = "SysHome";

        /// <summary>
        /// 默认路由 请求地址
        /// </summary>
        public string MapRouteAction { get; set; } = "index";


    }
}