using Abp.AutoMapper;
using Abp.Notifications;
using NetCoreFrame.Core;

namespace NetCoreFrame.Application
{

    /// <summary>
    /// 聊天发送对象
    /// </summary>
    [AutoMap(typeof(SysChatRecord))]
    public class SysCharSend
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public virtual string ChatDetailed { get; set; }


        /// <summary>
        /// 接收人
        /// </summary>
        public virtual long ReceiveUserId { get; set; }


        /// <summary>
        /// 消息分类
        /// </summary>
        public virtual NotificationSeverity Severity { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class SysCharPage
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public virtual string ChatDetailed { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        public virtual long SenderUserId { get; set; }

        /// <summary>
        /// 接收人id
        /// </summary>
        public virtual long ReceiveUserId { get; set; }
    }


}
