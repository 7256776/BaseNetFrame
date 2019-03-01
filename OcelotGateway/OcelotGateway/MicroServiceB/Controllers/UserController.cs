using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityObjectModel;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceB.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class UserController : Controller
    {
        private static List<UserInfo> user = new List<UserInfo>();

        [HttpPost]
        public JsonResult GetUser([FromBody]string id)
        {
            var data = user.Where(w => w.UserCode == id).ToList();
            return Json(data);
        }

        [HttpPost]
        public JsonResult GetUserData()
        {
            return Json(user);
        }

        /// <summary>
        /// http://localhost:40382/api/User/GetUserInfo/参数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public UserInfo SetUserInfo(string id)
        {
            UserInfo data = new UserInfo(Guid.NewGuid().ToString(), "服务BBB=" + id, id);
            user.Add(data);
            //return Json(true);
            return data;
        }

    }
}
