using Abp.Notifications;

namespace Frame.Core
{
    public class FrameNotificationProvider : NotificationProvider
    {
        /// <summary>
        /// 配置的订阅信息
        /// 如需要使用设置到启动类 Configuration.Notifications.Providers.Add<FrameNotificationProvider>();
        /// </summary>
        /// <param name="context"></param>
        public override void SetNotifications(INotificationDefinitionContext context)
        {
            //context.Manager.Add(
            //    new NotificationDefinition(
            //        "订阅名称",
            //        displayName: new LocalizableString("订阅显示名称", ConstantConfig.LocalizationName)
            //        )
            //    );
        }


    }
}
