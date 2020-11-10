using IdentityServer4.Models;
using IdentityServer4.Stores;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.WebApi
{
    /// <summary>
    /// 授权信息数据库存储
    /// </summary>
    public class FramePersistedGrantStore : IPersistedGrantStore
    {
        public static PersistedGrant persistedGrant;

        public Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            IEnumerable<PersistedGrant> data = new List<PersistedGrant>();
            return Task.FromResult(data);
        }

        /// <summary>
        /// 刷新token前获取原有token
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<PersistedGrant> GetAsync(string key)
        {
            if (persistedGrant.Key== key)
            {
                return Task.FromResult(persistedGrant);
            }
            return Task.FromResult<PersistedGrant>(null);
        }

        public Task RemoveAllAsync(string subjectId, string clientId)
        {
            return Task.CompletedTask;
        }

        public Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 清除原有token
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task RemoveAsync(string key)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// toekn获取完成后调用保存
        /// </summary>
        /// <param name="grant"></param>
        /// <returns></returns>
        public Task StoreAsync(PersistedGrant grant)
        {
            persistedGrant = grant;
            return Task.CompletedTask;
        }

    }
}
