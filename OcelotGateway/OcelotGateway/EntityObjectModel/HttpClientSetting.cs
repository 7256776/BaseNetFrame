using System;

namespace EntityObjectModel
{

    public class HttpClientSetting
    {
        public HttpClientSetting()
        {   }

        public HttpClientSetting(string url, string actionType)
        {
            Url = url;
            ActionType = actionType;
        }

        public string Url { get; set; }

        public string ActionType { get; set; }

        public string ParamData { get; set; }

        public string Token { get; set; }

    }




}
