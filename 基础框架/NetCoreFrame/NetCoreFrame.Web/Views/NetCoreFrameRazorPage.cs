using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using NetCoreFrame.Core;
using System;
using Abp.Threading;

namespace NetCoreFrame.Web.Views
{
    public abstract class NetCoreFrameRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected NetCoreFrameRazorPage()
        {
            LocalizationSourceName = ConstantConfig.LocalizationName;
        }


        /// <summary>
        /// 获取首页布局风格
        /// 使用配置1
        /// </summary>
        /// <returns></returns>
        public string GetMainLayout(string layoutStyle= "")
        {
            if (!string.IsNullOrEmpty(layoutStyle))
            {
                return "~/Views/Shared/_Layout" + layoutStyle + ".cshtml";
            }
            string f = GetSettingValue(ConstantConfig.FrameLayout);
            if (string.IsNullOrWhiteSpace(f))
            {
                f = "Side";
            }
            return "~/Views/Shared/_Layout" + f + ".cshtml";
        }

        /// <summary>
        /// 获取项目名称
        /// 使用配置2
        /// </summary>
        /// <returns></returns>
        public string InitProjectName()
        {
            string titleName = GetSettingValue(ConstantConfig.ProjectName);
            if (string.IsNullOrWhiteSpace(titleName))
            {
                titleName = "网站首页";
            }
            return titleName;
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetSettingValue(string key)
        {
            string valueStr = "";
            try
            {
                valueStr = AsyncHelper.RunSync(() => SettingManager.GetSettingValueForApplicationAsync(key));
            }
            catch (Exception)
            {
                //throw new UserFriendlyException("配置文件异常", "您设置的["+ key + "]配置信息不存在请检查FrameConfig配置文件!");
            }
            return valueStr;
        }




    }
}
