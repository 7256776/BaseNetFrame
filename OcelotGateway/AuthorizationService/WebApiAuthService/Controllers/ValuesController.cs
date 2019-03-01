using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebApiAuthService.Controllers
{
    [Route("api/[controller]/[action]")]
    //[ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly AppDbContext _context;



        public ValuesController(AppDbContext context)
        {
            _context = context;
        }


        // GET http://localhost:27749/api/values/GetData
        [HttpGet]
        [Authorize]
        public JsonResult GetData()
        {
            var data = _context.SysDicts.ToList();
            return new  JsonResult(data);
        }



      

    }
}
