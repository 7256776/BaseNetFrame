using IdentityServer4.Models;
using IdentityServer4.Stores;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.WebApi
{
    /// <summary>
    /// 授权信息数据库存储
    /// </summary>
    public class FramePersistedGrantStore :   IPersistedGrantStore
    {

        public virtual  Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            return Task.FromResult<IEnumerable<PersistedGrant>>(null);
        }

        /// <summary>
        /// 刷新token前获取原有ToKen
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual Task<PersistedGrant> GetAsync(string key)
        {
            return Task.FromResult<PersistedGrant>(null);
        }

        public virtual  Task RemoveAllAsync(string subjectId, string clientId)
        {
            return Task.CompletedTask;
        }

        public virtual  Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 清除原有ToKen
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual  Task RemoveAsync(string key)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// ToKen获取完成后调用保存
        /// </summary>
        /// <param name="grant"></param>
        /// <returns></returns>
        public virtual  Task StoreAsync(PersistedGrant grant)
        {
            return Task.CompletedTask;
        }

    }





}
