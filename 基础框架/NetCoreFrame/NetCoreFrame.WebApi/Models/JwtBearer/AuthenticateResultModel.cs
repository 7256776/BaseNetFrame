using System;

namespace NetCoreFrame.WebApi
{
    public class AuthenticateResultModel
    {
        public AuthenticateResultModel()
        {
            ResultState = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultMessage"></param>
        /// <param name="resultState"></param>
        public AuthenticateResultModel(string resultMessage, bool resultState)
        {
            ResultMessage = resultMessage;
            ResultState = resultState;
        }

        public string AccessToken { get; set; }

        public string EncryptedAccessToken { get; set; }

        public string EncryptedRefreshToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public DateTime ExpireInDate { get; set; }

        public string UserId { get; set; }

        public string ResultMessage { get; set; }

        public bool ResultState { get; set; } = true;
    }

}
