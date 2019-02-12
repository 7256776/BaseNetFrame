using System;

namespace NetCoreFrame.WebApi
{
    public class AuthenticateResultModel
    {
        public string AccessToken { get; set; }

        public string EncryptedAccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public DateTime ExpireInDate { get; set; }

        public long UserId { get; set; }
    }

}
