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

        /// <summary>
        /// 写入缓存数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public  Task<ActionResult<string>> SetString([FromBody]CacheParam param)
        {
            //设置绝对有效期
            DistributedCacheEntryOptions op = new DistributedCacheEntryOptions();
            op.SetAbsoluteExpiration(new TimeSpan(0, 0, 10));
            _distributedCache.SetStringAsync(param.KeyName, param.Message, op);
            return Task.FromResult<ActionResult<string>>("");
        }

        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ActionResult<string>> GetString([FromBody]CacheParam param)
        {
            var result = await _distributedCache.GetStringAsync(param.KeyName);
            return param.KeyName + ":" + result;
        }

        /// <summary>
        /// 缓存清除
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ActionResult<string>> Remove([FromBody]CacheParam param)
        {
            await _distributedCache.RemoveAsync(param.KeyName);
            return param.KeyName + ":数据缓存清除";
        }

        /// <summary>
        /// 缓存刷新
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ActionResult<string>> Refresh([FromBody]CacheParam param)
        {
            await _distributedCache.RefreshAsync(param.KeyName);
            return param.KeyName + ":数据缓存刷新";
        }

    }

    /// <summary>
    /// 获取本地时间
    /// </summary>
    public class LocalSystemClock : Microsoft.Extensions.Internal.ISystemClock
    {
        public DateTimeOffset UtcNow => DateTime.Now;
    }


}
