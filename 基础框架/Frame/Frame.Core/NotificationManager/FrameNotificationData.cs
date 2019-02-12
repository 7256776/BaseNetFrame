using Abp.Notifications;

namespace Frame.Core
{
    /// <summary>
    /// 通知数据对象
    /// </summary>
    //[Serializable]
    public class FrameNotificationData : MessageNotificationData
    {
        /// <summary>
        /// message作为消息弹出窗体的主题内容
        /// </summary>
        /// <param name="message"></param>
        public FrameNotificationData(string message) : base(message)
        {

        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息详细信息(窗体弹出窗点击查看的详细信息,如果该值为空或默认应用message的数据)
        /// </summary>
        public string NotificationDetailed { get; set; }

        /// <summary>
        /// 消息类型 sms/其他
        /// </summary>
        public string NotificationType { get; set; }

        /// <summary>
        /// 发送人用户id
        /// </summary>
        public long SendId { get; set; }

    }



}
