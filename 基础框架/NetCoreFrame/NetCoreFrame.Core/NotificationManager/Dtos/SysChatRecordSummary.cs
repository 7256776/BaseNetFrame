namespace NetCoreFrame.Core
{
    /// <summary>
    /// 通知发送对象
    /// </summary>
    public class SysChatRecordSummary
    {
        /// <summary>
        /// 发送人姓名
        /// </summary>
        public virtual string UserNameCn { get; set; }

        /// <summary>
        /// 发送人Id
        /// </summary>
        public virtual long SenderUserId { get; set; }

        /// <summary>
        /// 发送的条数
        /// </summary>
        public virtual int ChatCount { get; set; }

    }




}
