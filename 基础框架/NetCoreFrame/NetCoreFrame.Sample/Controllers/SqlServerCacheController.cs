using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NetCoreFrame.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Threading.Tasks;

namespace NetCoreFrame.Sample.Controllers
{
    public class SqlServerCacheController : NetCoreFrameControllerBase
    {
        private IDistributedCache _distributedCache;

        /// <summary>
        /// dotnet sql-cache create "Server=.;User=sa;Password=sa;Database=FrameDB" dbo AspNetCoreCache
        /// </summary>
        /// <param name="cache"></param>
        public SqlServerCacheController(IDistributedCache distributedCache)
        {
            this._distributedCache = distributedCache;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult<string>> SetTime([FromBody]CacheParam param)
        {
            await _distributedCache.SetStringAsync(param.KeyName, param.Message);
            return "";
        }

        public async Task<ActionResult<string>> GetTime([FromBody]CacheParam param)
        { 
            var result = await _distributedCache.GetStringAsync(param.KeyName);
            return param.KeyName + ":" + result;
        }
    }

    


}
