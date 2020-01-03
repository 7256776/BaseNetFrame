using Abp.MultiTenancy;

namespace NetCoreFrame.Web
{
    /// <summary>
    /// 该类被精简过
    /// 目前主要用于返回地址,登录验证通过后调整的地址.
    /// </summary>
    public class LoginFormViewModel
    {
        public string ReturnUrl { get; set; }

    }
}