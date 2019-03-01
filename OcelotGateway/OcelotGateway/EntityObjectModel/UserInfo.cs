using System;

namespace EntityObjectModel
{

    public class UserInfo
    {
        public UserInfo()
        {   }

        public UserInfo(string userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }

        public UserInfo(string userId, string userName, string userCode)
        {
            UserId = userId;
            UserName = userName;
            UserCode = userCode;
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserCode { get; set; }
    }




}
