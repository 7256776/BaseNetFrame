using Abp.Configuration;

namespace Frame.Core
{
    /// <summary>
    /// 配置管理扩展类
    /// </summary>
    public static class FrameSettingExtensions
    {
        /// <summary>
        /// 创建新的配置对象 <see cref="Setting"/> object from given <see cref="SettingInfo"/> object.
        /// </summary>
        public static SysSetting ToSetting(this SettingInfo settingInfo)
        {
            return settingInfo == null
                ? null
                : new SysSetting(settingInfo.TenantId, settingInfo.UserId, settingInfo.Name, settingInfo.Value);
        }

        /// <summary>
        /// 创建新的配置对象<see cref="SettingInfo"/> object from given <see cref="Setting"/> object.
        /// </summary>
        public static SettingInfo ToSettingInfo(this SysSetting setting)
        {
            return setting == null
                ? null
                : new SettingInfo(setting.TenantId, setting.UserId, setting.Name, setting.Value);
        }

        


    }
}
