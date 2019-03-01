using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityObjectModel;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceA.Controllers
{
    public class UserController : Controller
    {
        private static List<UserInfo> user = new List<UserInfo>();

        [HttpPost]
        public JsonResult GetUserData()
        {
            return Json(user);
        }

        [HttpGet]
        public JsonResult SetUserInfo(string id)
        {
            var data = new UserInfo(Guid.NewGuid().ToString(), "服务AAA=" + id);
            user.Add(data);
            return Json(data);
        }

    }
}
