using Abp.RealTime;

namespace Frame.Core
{

    public class OnlineClientExtension : OnlineClient
    {

        public OnlineClientExtension() { }

        /// <summary>
        /// 是否在线
        /// </summary>
        public virtual bool IsOnline { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public virtual string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>		
        public virtual string UserNameCn { get; set; }

        /// <summary>
        /// 用户图像
        /// </summary>		
        public virtual string ImageUrl { get; set; }


    }
}
